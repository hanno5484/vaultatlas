using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using VaultAtlas.DataModel;
using VaultAtlas.DataModel.FlacAtlas;
using VaultAtlas.DataModel.sqlite;
using VaultAtlas.Properties;

namespace VaultAtlas.FlacAtlas
{
	public class FlacAtlasControl : UserControl
	{
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ContextMenu contextMenu2;
		private System.Windows.Forms.MenuItem menuItem12;

        private Panel panel1;
        private Label label1;
        private MenuItem menuItem20;
        private Panel panel2;
        private ImageList imageList1;
        private Panel panel5;
        private Splitter splitter3;
        private FileContentDialog fileContentDialog1;

		public FlacAtlasControl()
		{
			InitializeComponent();

         
			this.listView1.SmallImageList = _fileTypeImages.SmallImageList;
			this.listView1.LargeImageList = _fileTypeImages.LargeImageList;
			this.treeView1.ImageList = _fileTypeImages.SmallImageList;
			this.treeView1.ImageIndex = 0;

            this.Load += FlacAtlasControl_Load;
        }

        void FlacAtlasControl_Load(object sender, EventArgs e)
        {
            if (DesignMode)
                return;

            this.RefreshView();
        }

        public void RefreshView()
        {
            const int rootimageIndex = 0;
            treeView1.Nodes.Clear();
            var rootNode = new TreeNode(resources.FlacAtlasRootName, rootimageIndex, rootimageIndex) {Tag = Disc.RootName};
            treeView1.Nodes.Add(rootNode);

            FillDiscs();
        }

		private void FillFiles( DiscDirectoryInfo dirInfo ) 
		{
		    try
		    {
		        listView1.BeginUpdate();
		        listView1.Items.Clear();
		        if (!dirInfo.IsNotRead)
		        {
		            this.listView1.Enabled = true;
		            this.panel1.Visible = false;
		            foreach (var file in dirInfo.GetFilesAdapter().Table.Rows.Cast<DataRow>().Select(r => new DiscFileInfo(r)))
		            {
		                int imageIndex = _fileTypeImages.GetFileTypeIndex(file.Name);
		                string sizeString = Formatters.GetFileSizeString(file.Size);

		                long lengthSeconds = file.Length;
		                
                        string lengthString;
		                if (lengthSeconds == 0)
		                    lengthString = string.Empty;
		                else
		                {
		                    lengthString = Formatters.GetTimeString(lengthSeconds);
		                }
		                string[] items = {file.Name, sizeString, file.LastModifiedDate.ToString("g"), lengthString};
		                var lvi = new ListViewItem(items, imageIndex) {Tag = file};
		                listView1.Items.Add(lvi);
		            }
		        }
		        else
		        {
		            listView1.Enabled = false;
		            panel1.Visible = true;
		        }
		    }
		    finally
		    {
		        listView1.EndUpdate();
		    }

		}

	    private void FillDiscs()
	    {
	        // return list of discs
	        var discs = DataManager.Discs.Table.Select().Select(r => new Disc(r)).ToArray();
	        treeView1.BeginUpdate();
	        try
	        {
	            foreach (var d in discs)
	            {
	                var d1 = d;

	                var tn = CreateTreeNode(treeView1.Nodes, Disc.RootName, d.DiscNumber,
	                    sn => FillSubTreeNodes(d1.GetRootDir().GetSubDirAdapter(), sn));

	                tn.Tag = d;
	            }
	        }
	        finally
	        {
	            treeView1.EndUpdate();
	        }
	    }

	    private void FillSubTreeNodes(AdapterBase adapter, TreeNode tn)
	    {
            treeView1.BeginUpdate();
	        try
	        {
	            var t = adapter.Table.Rows.Cast<DataRow>();
	            foreach (var dirRow in t)
	            {
	                var discDirInfo = new DiscDirectoryInfo(dirRow);
	                var subTreeNode = CreateTreeNode(tn.Nodes, discDirInfo.Name, discDirInfo.DisplayName,
	                    sn => FillSubTreeNodes(discDirInfo.GetSubDirAdapter(), sn));
	                subTreeNode.Tag = discDirInfo;
	            }
	        }
	        finally
	        {
	            treeView1.EndUpdate();
	        }
	    }

	    private TreeNode CreateTreeNode(TreeNodeCollection parentNode, string directory, string text, Action<TreeNode> expandAction)
	    {
	        int imageIndex = _fileTypeImages.GetDirectoryNormalImageIndex(directory);
	        int openimageIndex = _fileTypeImages.GetDirectoryOpenImageIndex(directory);
	        var node = new TreeNode(text, imageIndex, openimageIndex);
	        parentNode.Add(node);
	        node.Nodes.Add(DummyNodeIdentifier).Tag = expandAction;
	        return node;
	    }

	    internal static readonly FileTypeImageList _fileTypeImages = new FileTypeImageList();

	    private DataManager _dataManagerInstance;

	    private DataManager DataManager
	    {
	        get
	        {
                if (_dataManagerInstance == null)
	                _dataManagerInstance = DataManager.Get();
                return _dataManagerInstance;
	        }
	    }

		/// <summary>
		/// Die verwendeten Ressourcen bereinigen.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlacAtlasControl));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenu2 = new System.Windows.Forms.ContextMenu();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.menuItem20 = new System.Windows.Forms.MenuItem();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel5 = new System.Windows.Forms.Panel();
            this.fileContentDialog1 = new VaultAtlas.FlacAtlas.FileContentDialog();
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.ContextMenu = this.contextMenu2;
            resources.ApplyResources(this.treeView1, "treeView1");
            this.treeView1.FullRowSelect = true;
            this.treeView1.HideSelection = false;
            this.treeView1.LabelEdit = true;
            this.treeView1.Name = "treeView1";
            this.treeView1.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView1_BeforeLabelEdit);
            this.treeView1.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView1_AfterLabelEdit);
            this.treeView1.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeExpand);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // contextMenu2
            // 
            this.contextMenu2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem12,
            this.menuItem20});
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 0;
            resources.ApplyResources(this.menuItem12, "menuItem12");
            this.menuItem12.Click += new System.EventHandler(this.menuItem12_Click);
            // 
            // menuItem20
            // 
            this.menuItem20.Index = 1;
            resources.ApplyResources(this.menuItem20, "menuItem20");
            this.menuItem20.Click += new System.EventHandler(this.menuItem20_Click);
            // 
            // splitter1
            // 
            resources.ApplyResources(this.splitter1, "splitter1");
            this.splitter1.Name = "splitter1";
            this.splitter1.TabStop = false;
            // 
            // listView1
            // 
            this.listView1.AllowColumnReorder = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            resources.ApplyResources(this.listView1, "listView1");
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.LabelEdit = true;
            this.listView1.Name = "listView1";
            this.listView1.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listView1_AfterLabelEdit);
            this.listView1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView1_ItemSelectionChanged);
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // columnHeader3
            // 
            resources.ApplyResources(this.columnHeader3, "columnHeader3");
            // 
            // columnHeader4
            // 
            resources.ApplyResources(this.columnHeader4, "columnHeader4");
            // 
            // menuItem7
            // 
            this.menuItem7.Index = -1;
            resources.ApplyResources(this.menuItem7, "menuItem7");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.treeView1);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            resources.ApplyResources(this.imageList1, "imageList1");
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.fileContentDialog1);
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Name = "panel5";
            // 
            // fileContentDialog1
            // 
            this.fileContentDialog1.Content = null;
            resources.ApplyResources(this.fileContentDialog1, "fileContentDialog1");
            this.fileContentDialog1.Name = "fileContentDialog1";
            // 
            // splitter3
            // 
            resources.ApplyResources(this.splitter3, "splitter3");
            this.splitter3.Name = "splitter3";
            this.splitter3.TabStop = false;
            // 
            // FlacAtlasControl
            // 
            this.Controls.Add(this.splitter3);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel2);
            resources.ApplyResources(this, "$this");
            this.Name = "FlacAtlasControl";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            var hasFileContent = false;

            // file Eigenschaften

            try
            {
                if (listView1.SelectedItems.Count != 1)
                    return;
                var item = listView1.SelectedItems[0];
                var file = item != null ? item.Tag as IFileSystemFile : null;

                if (file != null)
                {
                    fileContentDialog1.Content = file.GetFileContent();
                    hasFileContent = true;
                }
            }
            finally
            {
                this.fileContentDialog1.Visible = hasFileContent;
            }
        }

		private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
            EnsureHasNoDummyNode(e.Node);
		}

	    private void EnsureHasNoDummyNode(TreeNode node)
	    {
	        if (node.Nodes.Count == 1 && node.Nodes[0].Text == DummyNodeIdentifier)
	        {
	            var dummyNode = node.Nodes[0];
	            node.Nodes.Remove(dummyNode);
	            var dir = dummyNode.Tag as Action<TreeNode>;
	            if (dir != null)
	            {
	                Cursor.Current = Cursors.WaitCursor;
	                dir(node);
	                Cursor.Current = Cursors.Default;
	            }
	        }
	    }

	    private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
	        var directoryInfo = e.Node.Tag as DiscDirectoryInfo;
	        if (directoryInfo != null)
	        {
	            FillFiles(directoryInfo);
	        }
		}

		private void menuItem12_Click(object sender, System.EventArgs e)
		{
			// folder eigenschaften
			var node = this.treeView1.SelectedNode;
			var directory = node != null ? node.Tag as IFileSystemDirectory : null;
		    if (directory == null)
                return;

		    var dict = new Dictionary<string, string>();

		    var files = directory.GetFiles().ToArray();
		    dict["Size"] = Formatters.GetFileSizeString(files.Sum(file => file.Size));
            
            var seconds = files.Sum(file => file.GetLengthSeconds());

            if (seconds > 0)
            {
                var lengthString = Formatters.GetTimeString(seconds);
                dict["Length"] = lengthString;
            }

		    var str = string.Join(", ", dict.Select(v => v.Key + ": " + v.Value));
		}

        public void ImportDisc()
        {
            var vid = new VolumeImporterDialog();
            vid.DataManager = this.DataManager;
            if (vid.DriveCount == 0)
            {
                MessageBox.Show(resources.NoDrivesAvailable, Constants.ApplicationName,MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if (vid.ShowDialog() != DialogResult.OK)
                return;

            var di = vid.SelectedDrive;

            var importer = new RecursiveImporter(
                new LocalFileSystemProvider(di.VolumeName), DataManager,
                di.SerialNumber, di.VolumeName, vid.NewDiscNumber);

            var newDiscNumber = vid.NewDiscNumber;

            var p = new VaultAtlas.DataModel.ModelUI.ProgressWindow();
            p.StartPosition = FormStartPosition.CenterParent;
            Form.ActiveForm.AddOwnedForm(p);
            p.ProgressVisible = false;
            p.Show();
            p.FormClosed += (o, args) =>
            {
                RefreshView();
                SelectDisc(newDiscNumber);
            };
            System.Threading.ThreadPool.QueueUserWorkItem(
                                      importer.DoImport,
                                      p );
        }

        private void SelectDisc(string discNumber)
        {
            foreach (TreeNode node in this.treeView1.Nodes[0].Nodes)
            {
                if (node.Text == discNumber)
                {
                    node.EnsureVisible();
                    node.Expand();
                    this.treeView1.SelectedNode = node;
                }
            }
        }

        private void menuItem15_Click(object sender, EventArgs e)
        {
            // TODO QUANTUM
            TreeNode node = this.treeView1.SelectedNode;
            if (node == null)
                return;

            if (node.Parent == null)
            {
                MessageBox.Show("You cannot delete the root node.");
                return;
            }

            string dir = node.Tag as string;
            string text = System.IO.Path.GetFileName(dir);
                        
            if (MessageBox.Show("Are you sure that you want to remove the directory " + text + " from the database?"
                , "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            bool isDisc = node.Parent.Parent == null;
            this.DataManager.DeleteRecursive(dir, !isDisc);
            this.RefreshView();

        }

	    private const string DummyNodeIdentifier = "(dummy)";

        private void treeView1_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            var dir = e.Node.Tag as DiscDirectoryInfo;
            if (dir == null)
                e.CancelEdit = true;
            else
                e.Node.Text = dir.GetNodeCaption();
        }

        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            var dir = e.Node.Tag as DiscDirectoryInfo;
            if (dir == null)
                e.CancelEdit = true;
            else
            {
                dir.DisplayName = e.Label;
                e.Node.Text = dir.GetNodeCaption();
            }
        }

        private void listView1_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            var item = ((ListView)sender).Items[e.Item];
            var file = (DiscFileInfo)item.Tag;
            file.DisplayName = e.Label;
            item.Text = file.DisplayName;
        }

        private void menuItem20_Click(object sender, EventArgs e)
        {
            if (this.treeView1.SelectedNode != null)
                this.treeView1.SelectedNode.BeginEdit();
        }
	}
}

