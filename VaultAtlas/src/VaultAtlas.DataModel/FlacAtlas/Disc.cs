using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using VaultAtlas.DataModel.sqlite;
using VaultAtlas.FlacAtlas;

namespace VaultAtlas.DataModel.FlacAtlas
{
    public class Disc : DataRowObject, IFileSystemProvider
    {
        public Disc(DataRow row) : base(row)
        {
        }

        #region IFileSystemProvider Members

        private AdapterBase _directories;

        public AdapterBase GetDirectoriesAdapter(string whereClause = null)
        {
            if (_directories != null)
                return _directories;

            var data = new DataSet("FlacAtlas");

            var dt = data.Tables.Add("DirectoryInfo");

            var queryText = "select * from Directory where DiscNumber = @DiscNumber" + (whereClause != null ? " and (" + whereClause + ")" : "");
            var da = new SQLiteDataAdapter(queryText, Model.ApplicationModel.Conn);
            da.SelectCommand.Parameters.Add("DiscNumber", DbType.String).Value = DiscNumber;

            da.FillSchema(dt, SchemaType.Source);
            da.Fill(dt);
            
            _directories = new AdapterBase(dt, da);
            return _directories;
        }

        string IFileSystemProvider.RootName
        {
            get { return "/"; }
        }

        public DiscDirectoryInfo GetRootDir()
        {
            return new DiscDirectoryInfo(GetDirectoriesAdapter("parentuid = ''").Table.Rows.Cast<DataRow>().First());
        }

        public const string RootName = "/";

        #endregion

        public DataRow CreateRootDirOnDisc()
        {
            var newRow = GetDirectoriesAdapter().Table.NewRow();
            newRow["DirectoryUID"] = Guid.NewGuid().ToString();
            newRow["DiscNumber"] = DiscNumber;
            newRow.Table.Rows.Add(newRow);
            return newRow;
        }

        IFileSystemDirectory IFileSystemProvider.GetRootDirectory()
        {
            return GetRootDir();
        }

        public string DiscNumber
        {
            get { return Row.Field<string>("DiscNumber"); }
        }

        public bool IsWritable
        {
            get { return 1 == Row.Field<long?>("IsWritable"); }
            set { Row["IsWritable"] = value ? 1 : 0; }
        }
    }
}
