using System;
using System.Data;
using System.Data.SQLite;

namespace VaultAtlas.DataModel.sqlite
{
	public class ArtistsAdapter : AdapterBase
	{
	    public ArtistsAdapter(DataTable table, SQLiteDataAdapter adapter) : base(table, adapter)
	    {
	    }

	    public Artist GetBestSuitedString(string prefix)
	    {
	        var prefixToLower = prefix.ToLower();
	        var columnIndex = Table.Columns.IndexOf("DisplayName");
	        
            Artist bestSuited = null;

	        foreach (DataRow row in Table.Rows)
	            if (row[columnIndex].ToString().ToLower().StartsWith(prefixToLower, StringComparison.OrdinalIgnoreCase)
	                && (bestSuited == null || bestSuited.DisplayName.Length > row[columnIndex].ToString().Length))
	                bestSuited = new Artist(row);

	        return bestSuited;
	    }

	    public Artist FindByAbbreviation( string abbrev ) 
		{
			abbrev = abbrev.ToLower();
            
            /* TODO QUANTUM
			foreach(Artist artist in this) 
				if (artist.Abbreviation.Equals(abbrev))
					return artist;
             */
			return null;
		}

	    public Artist Get(string sortName)
	    {
	        var row = Table.Rows.Find(sortName);
	        return row != null ? new Artist(row) : null;
	    }

		public string GetDisplayNameForSortName( string sortName )
		{
		    var row = Table.Rows.Find(sortName);
			return (row != null) ? row["DisplayName"].ToString() : "";
		}
	}
}
