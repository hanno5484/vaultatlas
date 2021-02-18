using System;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace VaultAtlas.DataModel.sqlite
{
    public class AdapterBase
    {
        public AdapterBase(DataTable table, SQLiteDataAdapter adapter)
        {
            Table = table;
            Adapter = adapter;

            var builder = new SQLiteCommandBuilder(adapter) {ConflictOption = ConflictOption.OverwriteChanges};
            Adapter.UpdateCommand = builder.GetUpdateCommand();
            Adapter.InsertCommand = builder.GetInsertCommand();
            Adapter.DeleteCommand = builder.GetDeleteCommand();
        }

        public DataTable Table { get; private set; }

        internal SQLiteDataAdapter Adapter { get; private set; }

        public void UpdateTable(DataTable table)
        {
            Adapter.Update(table);
        }

        public void Update(DataRow row)
        {
            if (row.Table != Table)
                throw new InvalidOperationException();
            
            if (row.RowState == DataRowState.Detached)
                row.Table.Rows.Add(row);

            Adapter.Update(new[] {row});
        }

        public void Update()
        {
            foreach (var row in Table.Rows.Cast<DataRow>().Where(r => r.RowState == DataRowState.Detached).ToArray())
            {
                row.Table.Rows.Add(row);
            }

            Adapter.Update(Table);
        }

    }
}
