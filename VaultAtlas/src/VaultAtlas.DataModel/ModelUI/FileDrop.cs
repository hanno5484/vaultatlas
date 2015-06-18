using System;
using System.Windows.Forms;

namespace VaultAtlas.DataModel.ModelUI
{
	public class FileDrop
	{
	    public FileDrop(Control targetControl)
	    {
	        targetControl.AllowDrop = true;
	        targetControl.DragOver += targetControl_DragOver;
	        targetControl.DragDrop += targetControl_DragDrop;
	    }

	    public event FileDropEventHandler FileDropped;

		private static void targetControl_DragOver(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent( "FileNameW" ))
				e.Effect = DragDropEffects.Link;
		}

		private void targetControl_DragDrop(object sender, DragEventArgs e)
		{
		    if (FileDropped == null)
                return;

		    if (e.Data.GetDataPresent( DataFormats.FileDrop ) && e.Effect == DragDropEffects.Link) 
		    {
		        foreach (var fileName in (string[])e.Data.GetData(DataFormats.FileDrop))
		        {
		            FileDropped( this, new FileDropEventArgs( fileName ));
		        }
		    }
		}
	}
}
