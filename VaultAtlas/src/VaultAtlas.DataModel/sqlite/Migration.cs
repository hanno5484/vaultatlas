using System;
using System.Data.SQLite;
using System.IO;
using System.Reflection;
using System.Resources;

namespace VaultAtlas.DataModel.sqlite
{
    internal class Migration
    {
        internal Migration(SQLiteConnection conn)
        {
            if (conn == null)
                throw new ArgumentNullException();
            _conn = conn;
        }

        private readonly SQLiteConnection _conn;
        private const int MigVersion = 4;

        internal void Migrate()
        {
            _conn.Open();

            try
            {
                var objVersion = new SQLiteCommand("select value from settings where key = 'migversion'", _conn).ExecuteScalar();

                int version;
                if (objVersion == null)
                {
                    new SQLiteCommand("insert into settings values('migversion', 1)", _conn).ExecuteNonQuery();
                    version = 1;
                }
                else
                {
                    if (!Int32.TryParse(objVersion.ToString(), out version))
                        version = 2;
                }

                for (var step = version + 1; step <= MigVersion; step++)
                    MigrateStep(step);

                new SQLiteCommand("update settings set value = '" + MigVersion + "' where key = 'migversion'", _conn).ExecuteNonQuery();
            }
            finally
            {
                _conn.Close();
            }
        }

        private void MigrateStep(int step)
        {
            var name = string.Format("VaultAtlas.DataModel.sqlite.sql{0:0000}.sqlite", step);
            var s = Assembly.GetExecutingAssembly().GetManifestResourceStream(name);
            if (s == null)
                throw new MissingManifestResourceException();
            using (var str = new StreamReader(s))
            {
                var cmdText = str.ReadToEnd();
                new SQLiteCommand(cmdText, _conn).ExecuteNonQuery();
            }
        }
    }
}
