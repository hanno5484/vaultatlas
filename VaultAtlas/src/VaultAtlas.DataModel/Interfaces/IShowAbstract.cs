using System;

namespace VaultAtlas.DataModel
{
	public interface IShowAbstract
	{
		string ArtistSortName
		{
			get;
		}

		string Key 
		{
			get;
		}

		string Venue
		{
			get;
		}

		ShowDate Date 
		{
			get;
		}

		string City 
		{
			get;
		}
	}
}
