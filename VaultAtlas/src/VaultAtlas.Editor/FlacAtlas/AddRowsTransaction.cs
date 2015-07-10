using System;
using System.Data;
using System.Collections.Generic;

namespace VaultAtlas.FlacAtlas
{
    public class AddRowsTransaction : IDisposable
    {
        public AddRowsTransaction(DataSet dataSet)
        {
            foreach (DataTable t in dataSet.Tables)
                t.BeginLoadData();
            _dataSet = dataSet;
        }
        
        public DataRow AddRow(string tableName)
        {
            var row = _dataSet.Tables[tableName].NewRow();
            _rowsAdded.Add(row);
            return row;
        }

        private readonly DataSet _dataSet;
        private readonly IList<DataRow> _rowsAdded = new List<DataRow>();

        public void Abort()
        {
            foreach (var row in _rowsAdded)
            {
                try
                {
                    row.Delete();
                }
                catch
                {
                }
            }
        }

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            foreach (DataTable tbl in _dataSet.Tables)
                tbl.EndLoadData();
        }

        #endregion
    }
}
