using System;
using System.Windows.Forms;

namespace VaultAtlas.DataModel
{
	public class GenericTabPage : ITabPage
	{
		public GenericTabPage( string title, Control rootControl, ImageList imageList, int imageIndex )
		{
			this.Title = title;
			this.Control = rootControl;
			this.ImageIndex = imageIndex;
			this.ImageList = imageList;
		}

		#region ITabPage Member

	    public Control Control { get; private set; }

	    public string Title { get; private set; }

	    public ImageList ImageList { get; private set; }

	    public int ImageIndex { get; private set; }

	    #endregion
	}
}
