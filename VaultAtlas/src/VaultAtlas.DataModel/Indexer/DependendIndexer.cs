using System;
using System.Collections.Generic;
using System.Data;

namespace VaultAtlas.DataModel.Indexer
{
	public class DependendIndexer
	{
        private readonly IDictionary<string, IList<string>> _dict = new Dictionary<string, IList<string>>();

	    public DataColumn Column { get; private set; }

	    public DataColumn DependendColumn { get; private set; }

	    public IEnumerable<string> GetValues(string input)
	    {
	        IList<string> l;
	        if (!_dict.TryGetValue(input.ToUpper(), out l))
	            return new string[0];
	        return l;
	    }

	    public DependendIndexer( DataColumn column, DataColumn dependendColumn )
		{
			if (column.Table != dependendColumn.Table)
				throw new Exception("Column and DependendColumn must belong to the same DataTable.");

			this.Column = column;
			this.DependendColumn = dependendColumn;
		    this.Column.Table.RowChanged += ColumnChanged;

			var colIndex= column.Table.Columns.IndexOf( column );
			var colIndexDependend = dependendColumn.Table.Columns.IndexOf( dependendColumn );

			foreach(DataRow dr in column.Table.Rows) 
                this.AddEntry( dr[ colIndex ].ToString(), dr[ colIndexDependend ].ToString());

		}

	    private void AddEntry(string val, string dependendVal)
	    {
	        if (string.IsNullOrEmpty(dependendVal))
	            return;

	        IList<string> arr;

	        var key = dependendVal.ToUpper();
	        if (!_dict.TryGetValue(key, out arr))
	            _dict[key] = arr = new List<string>();

	        if (!arr.Contains(val))
	            arr.Add(val);
	    }

	    private void ColumnChanged(object sender, DataRowChangeEventArgs e)
		{
			if (e.Action == DataRowAction.Commit || e.Action == DataRowAction.Add)
				this.AddEntry( e.Row[ this.Column ] as string, e.Row[ this.DependendColumn ] as string );
		}

	}
}
