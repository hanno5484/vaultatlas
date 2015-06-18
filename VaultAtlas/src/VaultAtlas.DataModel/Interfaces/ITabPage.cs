using System;


namespace VaultAtlas.DataModel
{
	public interface ITabPage
	{
		System.Windows.Forms.Control Control 
		{
			get;
		}

		string Title 
		{
			get;
		}

		System.Windows.Forms.ImageList ImageList 
		{
			get;
		}

		int ImageIndex 
		{
			get;
		}
	}
}
