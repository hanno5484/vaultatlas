using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using VaultAtlas.DataModel.FlacAtlas;
using VaultAtlas.DataModel.sqlite;

namespace VaultAtlas.FlacAtlas
{
	public class DataManager
	{
	    private DataManager()
	    {
            var data = new DataSet("FlacAtlas");

            var dt = data.Tables.Add("Disc");

            var da = new SQLiteDataAdapter("select * from Disc", DataModel.Model.ApplicationModel.Conn);
            da.FillSchema(dt, SchemaType.Source);
            da.Fill(dt);

            Discs = new AdapterBase(dt, da);
	    }

        public static DataManager Get()
        {
            return SingleDataManager;
        }

        private static readonly DataManager SingleDataManager = new DataManager();

        public AdapterBase Discs { get; private set; }

        public string this[string settingsKey, string defaultValue]
        {
            get
            {
                return defaultValue;
            }
        }

        public void DeleteRecursive(string dir, bool leaveStub)
        {
            /* TODO QUANTUM
            foreach (string directory in this.GetSubDirectories(dir))
            {
                DeleteRecursive( Path.Combine( dir, directory), false );
            }

            var dirRow = FindDir(dir);

            foreach (DataRow row in this.Data.Tables["FileInfo"].Select(
                "directory = '" + dirRow["DirectoryUID"] + "'"))
            {
                row.Delete();
            }

            if (leaveStub)
            {
                dirRow["IsNotRead"] = true;
            }
            else
            {
                if (dirRow["ParentUID"] is DBNull)
                {
                    this.DeleteDisc(dir);
                }
                dirRow.Delete();
            }
                        */

        }

        private void DeleteDisc(string discPath)
        {
            string[] split = discPath.Replace('\\', '/').Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (split.Length < 1)
                throw new Exception("Illegal directory name.");
            string discNumber = split[0];
            DataRow[] dr = Discs.Table.Select("DiscNumber = '" + SafeSelect(discNumber) + "'");
            if (dr.Length != 1)
                throw new Exception("Could not find disc " + discNumber);
            dr[0].Delete();

        }

        public static string SafeSelect( string where)
        {
            return where.Replace("'", "''");
        }
    }
}
