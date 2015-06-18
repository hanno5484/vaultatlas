using System;
using System.Collections.Generic;
using System.Data;

namespace VaultAtlas.DataModel.UndoRedo
{

	public class UndoRedoManager
	{
	    public List<PerformedAction> InternalList { get; private set; }

	    private int _minIndex;

	    public Model Model { get; private set; }

	    private bool _captureActions = true;
		private int _savedIndex;

		public UndoRedoManager( Model model )
		{
		    InternalList = new List<PerformedAction>();
		    Model = model;
		}

	    internal event UndoRedoStateEventHandler UndoRedoStateChanged;

		public void UndoAction() 
		{
			if ( this._minIndex == 0 )
				throw new Exception("No action to undo.");

			_captureActions = false;
			_minIndex--;
            InternalList[_minIndex].Undo();
			this.OnUndoRedoStateChanged();
			_captureActions = true;
		}

		public void RedoAction()
		{
			if ( _minIndex == UndoableActionCount )
				throw new Exception("No action to redo.");

			_captureActions = false;
			InternalList[_minIndex].Do();
			_minIndex++;
			this.OnUndoRedoStateChanged();
			_captureActions = true;
		}

	    public int UndoableActionCount { get; private set; }

	    public int RedoableActionCount
		{
			get 
			{
				return UndoableActionCount-_minIndex;
			}
		}

		private readonly string[] _watchTables = { "Shows", "Resources" };

		public void RegisterEventHandlers(DataSet data) 
		{
			foreach( var tableName in _watchTables ) 
			{
				data.Tables[ tableName ].ColumnChanged += ColumnChangedHandler;
				data.Tables[ tableName ].RowDeleting += RowDeletedHandler;
				data.Tables[ tableName ].RowChanged += RowChangedHandler;
			}
		}

		public void UnregisterEventHandlers(DataSet data)
		{
			foreach( var tableName in _watchTables ) 
			{
				data.Tables[ tableName ].ColumnChanged -= ColumnChangedHandler;
				data.Tables[ tableName ].RowDeleting -= RowDeletedHandler;
				data.Tables[ tableName ].RowChanged -= RowChangedHandler;
			}
		}

		public void ColumnChangedHandler(object sender, DataColumnChangeEventArgs args) 
		{
			if ( !_captureActions )
				return;

			if ( args.Column.DataType == typeof(System.DateTime) )
				args.ProposedValue = args.ProposedValue.ToString().Split(' ')[0];
				
			// Detached means that this row is yet to be inserted, so no undo value can be
			// given. This is not registered as an undo action because the RowChangedHandler
			// already handles insertions.
			if ( args.Row.RowState == DataRowState.Detached )
				return;

			object originalValue = args.Row[ args.Column, DataRowVersion.Current ];

			// If no actual change to the data was made, do not register this as an undo action
			if (originalValue == null || originalValue.Equals(args.ProposedValue)) 
				return;
			this.doAction( new UpdateAction( args.Row, args.Column.ColumnName, args.ProposedValue, originalValue ));
		}

		public void RowChangedHandler(object sender, DataRowChangeEventArgs args) 
		{
			if (!_captureActions)
				return;
			if (args.Action == DataRowAction.Add)
				this.doAction( new RowAction( true, args.Row));
		}

		public void RowDeletedHandler(object sender, DataRowChangeEventArgs args) 
		{
			if (!_captureActions)
				return;
			if (args.Action == DataRowAction.Delete)
				this.doAction( new RowAction( false, args.Row ));
		}

		internal void doAction(PerformedAction performed ) 
		{
			while ( InternalList.Count < _minIndex+1 )
				InternalList.Add( null );
			InternalList[_minIndex] = performed;
			_minIndex++;
			UndoableActionCount = _minIndex;
			for(int i=_minIndex;i<InternalList.Count;i++) // flush action cache
				InternalList[i] = null;
			this.OnUndoRedoStateChanged();
		}

		public void SetSavedIndex() 
		{
			this._savedIndex = this._minIndex;
			this.OnUndoRedoStateChanged( );
		}

		private void OnUndoRedoStateChanged() 
		{
			if (this.UndoRedoStateChanged != null)
				this.UndoRedoStateChanged( this, new UndoRedoStateEventArgs( this.UndoableActionCount > 0, this.RedoableActionCount > 0, this._savedIndex == this._minIndex) );
		}
	}
}
