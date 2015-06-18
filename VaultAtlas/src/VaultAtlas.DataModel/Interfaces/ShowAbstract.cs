using System;

namespace VaultAtlas.DataModel
{
	public class ShowAbstract : IShowAbstract
	{
		public ShowAbstract( string display, string key, ShowDate date, string venue, string city, string artistSortName ) 
		{
			Display = display;
			Key = key;
			Venue = venue;
			City = city;
			ArtistSortName = artistSortName;
			Date = date;
		}

	    public string Display { get; private set; }

	    public string Key { get; private set; }

	    public override string ToString()
		{
			return this.Display;
		}

		#region IShowAbstract Member

	    public ShowDate Date { get; private set; }

	    public string City { get; private set; }

	    public string ArtistSortName { get; private set; }

	    public string Venue { get; private set; }

	    #endregion
	}
}
