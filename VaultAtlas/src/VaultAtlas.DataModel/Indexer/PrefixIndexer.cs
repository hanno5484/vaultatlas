using System;
using System.Collections.Generic;
using System.Data;

namespace VaultAtlas.DataModel.Indexer
{
	public class PrefixIndexer
	{
		private readonly DataColumn _column;
		private readonly Dictionary<char, IList<string>> _dict = new Dictionary<char,IList<string>>();
		private readonly IList<string> _allEntries = new List<string>();

		internal PrefixIndexer( DataColumn dc )
		{
			_column = dc;

		    for (var c = 'A'; c <= 'Z'; c++)
		        _dict[c] = new List<string>();

			_dict[' '] = new List<string>(); // für alle anderen Buchstaben
			
            foreach(DataRow dr in this._column.Table.Rows) 
				AddEntry(dr[dc].ToString());
		    
            _column.Table.ColumnChanged += Table_ColumnChanged;
		}

		private void Table_ColumnChanged(object sender, DataColumnChangeEventArgs e)
		{
			if ( e.Column == this._column && e.ProposedValue != null )
				this.AddEntry( e.ProposedValue.ToString() );
		}

		private IList<string> this[string prefix] 
		{
			get 
			{
				if (string.IsNullOrEmpty(prefix) )
					prefix = " ";
				var d = prefix.Substring(0,1).ToUpper()[0];
				if (d < 'A' || d > 'Z')
					d = ' ';
				return _dict[d];
			}
		}

		private void AddEntry( string entry ) 
		{
			var list = this[ entry ];
			if (!list.Contains( entry )) 
			{
				list.Add( entry );
			}
			if (!this._allEntries.Contains( entry ))
				this._allEntries.Add( entry );
		}

		public IEnumerable<string> GetBestForPrefixes( string prefix ) 
		{
			return this[ prefix ];
		}

		public IEnumerable<string> GetAllEntries( )
		{
		    return _allEntries;
		}
	}
}
