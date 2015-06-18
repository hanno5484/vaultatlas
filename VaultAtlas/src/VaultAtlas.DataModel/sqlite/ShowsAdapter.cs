using System.Data;
using System.Data.SQLite;

namespace VaultAtlas.DataModel.sqlite
{
    public class ShowsAdapter : AdapterBase
    {
        public ShowsAdapter(DataTable table, SQLiteDataAdapter adapter) : base(table, adapter)
        {
        }
    }
}
