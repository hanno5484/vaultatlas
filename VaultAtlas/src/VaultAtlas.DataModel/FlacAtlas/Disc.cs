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

        DateTime IFileSystemProvider.GetLastModifiedDate(string fileName)
        {
            DataRow row = this.FindFile(fileName);
            if (row == null)
                throw new Exception();
            return (DateTime)row["DateLastModified"];

        }

        #endregion

        public DataRow CreateRootDirOnDisc()
        {
            var newRow = GetDirectoriesAdapter().Table.NewRow();
            newRow["DirectoryUID"] = Guid.NewGuid().ToString();
            newRow["DiscNumber"] = DiscNumber;
            newRow.Table.Rows.Add(newRow);
            return newRow;
        }

        public string GetVolumeName(string volume)
        {
            return Row["VolumeID"] as string;
        }

        IFileSystemDirectory IFileSystemProvider.GetRootDirectory()
        {
            return GetRootDir();
        }

        public string GetSerialNumber(string volume)
        {
            return Row["SerialNumber"] as string;
        }

        public byte[] GetFileContent(string fileName)
        {
            DataRow fileRow = this.FindFile(fileName);
            if (fileRow != null)
                return fileRow["Content"] as byte[];
            return null;
        }

        public string DiscNumber
        {
            get { return Row.Field<string>("DiscNumber"); }
        }

        private DiscDirectoryInfo FindDir(string dir)
        {
            var split = dir.Replace('\\', '/').Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (split.Length < 1)
                throw new Exception("Illegal directory name.");

            string discNumber = split[0];
            if (!string.Equals(discNumber, DiscNumber, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException();

            var rootDirOnDisc = GetDirectoriesAdapter().Table.Select("isnull( parentuid, '')=''");
            var parentDir = (rootDirOnDisc.Length < 1) ? CreateRootDirOnDisc() : rootDirOnDisc[0];

            for (int i = 1; i < split.Length; i++)
            {
                var nextDir = GetDirectoriesAdapter().Table.Select(
                    "ParentUID = '" + parentDir["DirectoryUID"] + "' and name = '" + Util.MakeSelectSafe(split[i]) + "'");
                if (nextDir.Length < 1)
                    throw new Exception("Directory not found.");
                parentDir = nextDir[0];
            }

            return parentDir != null ? new DiscDirectoryInfo(parentDir) : null;
        }

        private DataRow FindFile(string file)
        {
            /* TODO QUANTUM
            int lastIndex = file.Replace('\\','/').LastIndexOf('/');
            if ( lastIndex == -1 )
                throw new Exception("Illegal file name: "+file);
            string fileName = file.Substring(lastIndex+1);
            string dir = file.Substring(0, lastIndex );
            DataRow dirRow = FindDir(dir);
            if (dirRow == null)
                throw new Exception("Directory not found: "+dir);
            DataRow[] rows = this.Data.Tables["FileInfo"].Select(
                "Name = '" + SafeSelect(fileName) + "' and directory = '" + dirRow["DirectoryUID"].ToString() + "'");
            if (rows == null || rows.Length == 0)
                throw new Exception("File not found: "+fileName );
            return rows[0];
             */
            return null;
        }
    }
}
