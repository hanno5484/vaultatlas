using System.Collections;
using System.Data;
using System;

namespace VaultAtlas.DataModel.UndoRedo
{
    public abstract class PerformedAction
    {
        protected PerformedAction(DataRow dr)
        {
            IsDone = true;
            DataRow = dr;
        }

        public abstract string Display
        {
            get;
        }

        public DataRow DataRow { get; protected set; }

        public bool IsDone { get; protected set; }

        public void Undo()
        {
            if (!this.IsDone)
                throw new Exception("Attempted to undo already undone action.");

            this.InternalUndo();

            this.IsDone = false;
        }

        public void Do()
        {
            if (IsDone)
                throw new Exception("Attempted to redo already performed action.");

            InternalDo();
            IsDone = true;
        }

        protected abstract void InternalUndo();

        protected abstract void InternalDo();        
    }

    public class RowAction : PerformedAction
    {
        public RowAction(bool isInsert, DataRow row) : base( row)
        {
            IsInsert = isInsert;
            TargetTable = row.Table;
        }

        public DataTable TargetTable { get; private set; }

        public override string Display
        {
            get
            {
                return (this.IsInsert ? "Insert" : "Delete") + " on table " + TargetTable.TableName;            
            }
        }

        protected override void InternalDo()
        {
            if (!this.IsInsert)
            {
                this.DataRow.Table.Rows.Remove(this.DataRow);
            }
            else
            {
                if (this.DataRow.Table == null)
                    this.TargetTable.Rows.Add(this.DataRow);
            }
        }

        public bool IsInsert { get; private set; }

        protected override void InternalUndo()
        {
            if (this.IsInsert)
            {
                this.DataRow.Table.Rows.Remove(this.DataRow);
            }
            else
            {
                if ( this.DataRow.Table == null )
                    this.TargetTable.Rows.Add(this.DataRow);

                DataRow.RejectChanges();
                TargetTable.AcceptChanges();
            }
        }
    }

    public class UpdateAction : PerformedAction
    {
        public UpdateAction(DataRow row, string columnName, object newValue, object oldValue)
            : base(row)
        {
            this.NewValue = newValue;
            this.OldValue = oldValue;

            this.ColumnName = columnName;
        }

        public override string Display
        {
            get
            {
                return "Update on table " + this.DataRow.Table.TableName + ", oldValue = "+this.OldValue.ToString()+", newValue = "+this.NewValue.ToString();
            }
        }

        protected override void InternalUndo()
        {
            this.DataRow.BeginEdit();
            this.DataRow[ this.ColumnName ] = this.OldValue;
            this.DataRow.EndEdit();
            this.DataRow.AcceptChanges();
        }

        protected override void InternalDo()
        {
            this.DataRow.BeginEdit();
            this.DataRow[this.ColumnName] = this.NewValue;
            this.DataRow.EndEdit();
            this.DataRow.AcceptChanges();
        }

        public string ColumnName { get; private set; }

        public object OldValue { get; private set; }

        public object NewValue { get; private set; }
    }
}
