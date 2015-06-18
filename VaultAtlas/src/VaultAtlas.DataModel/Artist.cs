using System;
using System.Data;

namespace VaultAtlas.DataModel
{
	public class Artist : DataRowObject
	{
	    public Artist(DataRow row) : base(row)
	    {
	    }

	    public string Folder
	    {
	        get { return Row["Folder"] as string; }
	        set { Row["Folder"] = value; }
	    }

	    public string SortName
	    {
	        get { return Row["SortName"] as string; }
	        set { Row["SortName"] = value; }
	    }

	    public string Abbreviation
	    {
	        get { return Row["Abbreviation"] as string; }
	        set { Row["Abbreviation"] = value; }
	    }

	    public string DisplayName
	    {
	        get { return Row["DisplayName"] as string; }
	        set { Row["DisplayName"] = value; }
	    }

	    public int ETreeArtistID
	    {
	        get { return Row.Field<int?>("ETreeArtistID").GetValueOrDefault(); }
	        set { Row["ETreeArtistID"] = value; }
	    }

	    public int GetShowCount()
		{
		    return 0; // TODO QUANTUM this.row.Table.DataSet.Tables["Shows"].Select( "Artist = '"+Util.MakeSelectSafe( this.SortName )+"'" ).Length;
		}
	}
}
