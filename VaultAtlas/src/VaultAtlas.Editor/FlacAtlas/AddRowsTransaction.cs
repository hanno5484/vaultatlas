using System;
using System.Data;
using System.Collections.Generic;

namespace VaultAtlas.FlacAtlas
{
    public class AddRowsTransaction : IDisposable
    {
        public AddRowsTransaction(DataManager dataManager)
        {
            /* TODO QUANTUM
            _dataSet = dataManager.Data;

            foreach (DataTable tbl in _dataSet.Tables)
                tbl.BeginLoadData();
             */
        }

        public DataRow AddRow(string tablename)
        {
            var row = _dataSet.Tables[tablename].NewRow();
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
            foreach (DataTable tbl in this._dataSet.Tables)
                    tbl.EndLoadData();
        }

        #endregion
    }
}
