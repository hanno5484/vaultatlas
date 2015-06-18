using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;

namespace VaultAtlas.DataModel.sqlite
{
    public class Connection
    {
        private void Create()
        {
            // auskommentieren wenn importiert werden soll
            return;

            // SQLiteConnection.CreateFile(DatabaseFileName);

            // RecreateSizeAndLength();

            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();

                // importschemavaultatlas();

                CreateSchema(conn);

                var ds = new LegacyVaultAtlasModel().GetDataSet();

                var dm = VaultAtlas.FlacAtlas.DataManager.Get();
                /* TODO QUANTUM dm.Init(null);

                dm.Data.Tables["Directory"].Columns["DirectoryUID"].ColumnName = "UID";
                dm.Data.Tables["FileInfo"].Columns["FileUID"].ColumnName = "UID";
                // importiere die Vaultatlas Daten
                ImportDataSet(conn, ds.Tables.Cast<DataTable>());

                // importiere die Flacatlas Daten
                ImportDataSet(conn, dm.Data.Tables.Cast<DataTable>());
                */
            }
        }

        private static void RecreateSizeAndLength()
        {
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();

                var ds = new DataSet();
                ds.ReadXml(@"C:\Users\hannob\Dropbox\Vault.xml", XmlReadMode.InferSchema);

                foreach (DataRow fileRow in ds.Tables["FileInfo"].Rows)
                {
                    try
                    {
                        var size = fileRow["Size"];
                        var length = fileRow["Length"];
                        var uid = fileRow["FileUID"];

                        var updateCmd = new SQLiteCommand("update fileinfo set Size = @size, Length = @length where UID = @uidfile and length = 0", conn);
                        updateCmd.Parameters.Add("uidfile", DbType.String).Value = uid;
                        updateCmd.Parameters.Add("size", DbType.Int64).Value = size;
                        updateCmd.Parameters.Add("length", DbType.Int32).Value = length;
                        var chk = updateCmd.ExecuteScalar();
                    }
                    catch (Exception exc)
                    {
                        
                    }
                }

                conn.Close();
            }
        }

        private const string ConnectionString = "Data Source=vaultatlas.sqlite;Version=3;";

        internal const string DatabaseFileName = "vaultatlas.sqlite";

        public SQLiteConnection GetConnection()
        {
            Create();

            return new SQLiteConnection(ConnectionString);
        }

        private void CreateSchema(SQLiteConnection conn)
        {
            var s = Assembly.GetExecutingAssembly().GetManifestResourceStream("VaultAtlas.DataModel.sqlite.sql0001.sqlite");
            using (var sr = new StreamReader(s))
            {
                var queryText = sr.ReadToEnd();

                try
                {
                    var c = new SQLiteCommand(conn) {CommandText = queryText};
                    c.ExecuteNonQuery();
                }
                catch (Exception exc)
                {
                }
            }
        }

        private void ImportDataSet(SQLiteConnection conn, IEnumerable<DataTable> ds)
        {
            foreach (var table in ds)
            {
                if (table.TableName == "UpdatePoints")
                    continue;

                const string pattern = "insert into {0} ({1}) values ({2})";

                var columns = table.Columns.Cast<DataColumn>().ToArray();
                var columnNames = string.Join(", ", columns.Select(c => c.ColumnName));


                foreach (var dataRow in table.Rows.Cast<DataRow>())
                {
                    var row = dataRow;
                    var c = new SQLiteCommand(conn);
                    var values = columns.Select(dc => GetDataValueString(row, dc, c));
                    var valueStrings = string.Join(", ", values);
                    var stmt = string.Format(pattern, table.TableName, columnNames, valueStrings);
                    c.CommandText = stmt;
                    try
                    {
                        c.ExecuteNonQuery();
                    }
                    catch (Exception exc)
                    {
                    }
                }
            }
        }

        private static string GetDataValueString(DataRow dataRow, DataColumn dc, SQLiteCommand cmd)
        {
            var val = dataRow[dc];
            string strVal;
            if (val is string)
                strVal = "'" + ((string) val).Replace("'", "''").Replace("\0", "") + "'";
            else if (val is DateTime)
                strVal = "'" + ((DateTime) val).ToString("s") + "'";
            else if (val is byte)
                strVal = ((int)(byte)val).ToString();
            else if (val is int)
                strVal = ((int)val).ToString();
            else if (val is byte[])
            {
                strVal = "@" + dc.ColumnName;
                cmd.Parameters.Add(new SQLiteParameter(dc.ColumnName, val));
            }
            else if (val is bool)
            {
                strVal = ((bool)val) ? "1" : "0";
            }
            else
            {
                strVal = "''";
            }

            return strVal;
        }

        private static void importschemavaultatlas(DataSet data)
        {
            foreach (DataTable table in data.Tables)
            {
                var cmd = "CREATE TABLE IF NOT EXISTS " + table.TableName + "(";

                var table1 = table;

                cmd += string.Join(", ", table.Columns.Cast<DataColumn>().Select(column => GetColumnDeclaration(column, table1)));

                cmd += ");";

                Console.WriteLine(cmd);
            }
        }

        private static string GetColumnDeclaration(DataColumn column, DataTable table1)
        {
            var s = column.ColumnName;

            if (table1.PrimaryKey.Contains(column))
                s += " PRIMARY KEY";

            string type;
            if (column.DataType == typeof(bool))
                type = "INTEGER";
            else if (column.DataType == typeof(int))
                type = "TEXT";
            else if (column.DataType == typeof(string))
                type = "TEXT";
            else if (column.DataType == typeof(byte))
                type = "TEXT";
            else if (column.DataType == typeof(object) || column.DataType == typeof(DateTime))
            {
                type = "TEXT";
            }
            else
            {
                type = "TEXT";
            }
            s += " " + type;
            return s;
        }
    }
}
