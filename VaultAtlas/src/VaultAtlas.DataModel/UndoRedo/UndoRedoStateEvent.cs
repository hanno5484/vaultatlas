using System;

namespace VaultAtlas.DataModel.UndoRedo
{
	public delegate void UndoRedoStateEventHandler( object sender, UndoRedoStateEventArgs args );

	public class UndoRedoStateEventArgs : EventArgs 
	{
	    public bool CanUndo { get; private set; }

	    public bool CanRedo { get; private set; }

	    public bool IsSaved { get; private set; }

	    public UndoRedoStateEventArgs( bool canUndo, bool canRedo, bool isSaved ) 
		{
			CanRedo = canRedo;
			CanUndo = canUndo;
			IsSaved = isSaved;
		}
	}
}
