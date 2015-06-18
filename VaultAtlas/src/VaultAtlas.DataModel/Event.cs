using System;
using System.Data;

namespace VaultAtlas.DataModel
{

	public delegate void ShowEvent(object sender, ShowEventArgs e);

	public class ShowEventArgs : EventArgs 
	{
		public ShowEventArgs( int showIndex, Show show )
		{
			this.Show = show;
            this.ShowIndex = showIndex;
		}

		public ShowEventArgs( int showIndex, DataRow row )
		{
			this.Show = Show.GetShow( row );
            this.ShowIndex = showIndex;
		}

	    public int ShowIndex { get; private set; }

	    public Show Show { get; private set; }
	}

    public delegate void FileDropEventHandler(object sender, FileDropEventArgs e);

	public class FileDropEventArgs : EventArgs 
	{
	    public FileDropEventArgs(string fileName)
	    {
			FileName = fileName;
		}

	    public string FileName { get; private set; }
	}

}
