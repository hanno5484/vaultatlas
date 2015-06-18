using System;
using System.Data;

namespace VaultAtlas.DataModel
{
    public abstract class DataRowObject
    {
        protected DataRowObject(DataRow row)
        {
            if (row == null)
                throw new ArgumentNullException();

            Row = row;
        }

        public DataRow Row { get; private set; }
    }
}
