using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace VaultAtlas.FlacAtlas
{
	/// <summary>
	/// Zusammenfassung für FolderPropertyDialog.
	/// </summary>
	public class FolderPropertyDialog : UserControl
	{
        private System.Windows.Forms.Label label1;
        private ListView listView1;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private FileContentDialog fileContentDialog1;
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FolderPropertyDialog()
		{
			InitializeComponent();
		}

        private bool isMultiSelect;

	    public void ShowDirectoryInfo(IFileSystemDirectory folder)
	    {
            /*
            int index = firstFile.Name.IndexOf('/', 1);
            if (index != -1)
            {
                var item = new ListViewItem(new[] { "Disc", firstFile.Name.Substring(1, index) });
                this.listView1.Items.Add(item);
            }
            else
            {
                var item = new ListViewItem(new[] { "Volume name", _provider.GetVolumeName(firstFile.Name.Substring(1)) });
                this.listView1.Items.Add(item);

                item = new ListViewItem(new[] { "Serial number", _provider.GetSerialNumber(firstFile.Name.Substring(1)) });
                this.listView1.Items.Add(item);
            }
            */
	        // FlacAtlasControl._fileTypeImages.GetDirectoryNormalImageIndex(folder.Name), FlacAtlasControl._fileTypeImages.LargeImageList

	        var files = folder.GetFiles().ToArray();

            AddFilesProperties(files);

            tabControl1.TabPages.Remove(tabPage2);
	    }

	    public void ReadFileInfo(IEnumerable<IFileSystemFile> fileNames)
	    {
	        var files = fileNames.ToArray();
	        isMultiSelect = files.Count() > 1;
	        label1.ImageList = FlacAtlasControl._fileTypeImages.LargeImageList;
	        label1.ImageIndex = 0;
	        var firstFile = files.First();
	        label1.Text = isMultiSelect
	            ? files.Count() + " files"
	            : Path.GetFileName(firstFile.Name);

	        if (isMultiSelect)
	            tabControl1.TabPages.Remove(tabPage2);

	        AddFilesProperties(files);

	        if (!isMultiSelect)
	        {
	            fileContentDialog1.Content = firstFile.FileContent;
	        }
	    }

	    private void AddFilesProperties(IFileSystemFile[] files)
	    {
	        var item = new ListViewItem(new[] {"Size", Formatters.GetFileSizeString(files.Sum(file => file.Size))});
	        listView1.Items.Add(item);

	        var seconds = files.Sum(file => file.GetLengthSeconds());

	        if (seconds > 0)
	        {
	            var lengthItem = new ListViewItem(new[] {"Length", Formatters.GetTimeString(seconds)});
	            listView1.Items.Add(lengthItem);
	        }
	    }

	    /// <summary>
		/// Die verwendeten Ressourcen bereinigen.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Vom Windows Form-Designer generierter Code
		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FolderPropertyDialog));
            this.label1 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.fileContentDialog1 = new VaultAtlas.FlacAtlas.FileContentDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            resources.ApplyResources(this.listView1, "listView1");
            this.listView1.Name = "listView1";
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listView1);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.fileContentDialog1);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // fileContentDialog1
            // 
            this.fileContentDialog1.Content = null;
            resources.ApplyResources(this.fileContentDialog1, "fileContentDialog1");
            this.fileContentDialog1.Name = "fileContentDialog1";
            // 
            // FolderPropertyDialog
            // 
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.Name = "FolderPropertyDialog";
            resources.ApplyResources(this, "$this");
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion



	}
}
