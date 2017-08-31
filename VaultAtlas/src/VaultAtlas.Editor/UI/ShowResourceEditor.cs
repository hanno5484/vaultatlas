using System;
using System.Data;
using System.Linq;
using VaultAtlas.DataModel.ModelUI;
using VaultAtlas.DataModel;
using System.Windows.Forms;
using VaultAtlas.DataModel.sqlite;

namespace VaultAtlas.UI
{
	public class ShowResourceEditor : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ImageList imagesLarge;
		private System.Windows.Forms.ImageList imagesSmall;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.MenuItem menuItem7;
		private System.ComponentModel.IContainer components;

		public ShowResourceEditor()
		{
			// Dieser Aufruf ist für den Windows Form-Designer erforderlich.
			InitializeComponent();

			this.Load +=ShowResourceEditor_Load;
		}

		/// <summary> 
		/// Die verwendeten Resourcen bereinigen.
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

		#region Vom Komponenten-Designer generierter Code
		/// <summary> 
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowResourceEditor));
            this.listView1 = new System.Windows.Forms.ListView();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.imagesLarge = new System.Windows.Forms.ImageList(this.components);
            this.imagesSmall = new System.Windows.Forms.ImageList(this.components);
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.ContextMenu = this.contextMenu1;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.listView1.LabelEdit = true;
            this.listView1.LargeImageList = this.imagesLarge;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(128, 256);
            this.listView1.SmallImageList = this.imagesSmall;
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listView1_AfterLabelEdit);
            this.listView1.BeforeLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listView1_BeforeLabelEdit);
            // 
            // contextMenu1
            // 
            this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem6,
            this.menuItem7,
            this.menuItem1,
            this.menuItem2,
            this.menuItem3,
            this.menuItem4});
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 0;
            this.menuItem6.Text = "Add &text ...";
            this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 1;
            this.menuItem7.Text = "Add &link ...";
            this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 2;
            this.menuItem1.Text = "&Delete ...";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 3;
            this.menuItem2.Text = "-";
            // 
            // menuItem3
            // 
            this.menuItem3.Checked = true;
            this.menuItem3.Index = 4;
            this.menuItem3.Text = "Large Icons";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 5;
            this.menuItem4.Text = "Small Icons";
            this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
            // 
            // imagesLarge
            // 
            this.imagesLarge.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imagesLarge.ImageStream")));
            this.imagesLarge.TransparentColor = System.Drawing.Color.Transparent;
            this.imagesLarge.Images.SetKeyName(0, "");
            this.imagesLarge.Images.SetKeyName(1, "");
            this.imagesLarge.Images.SetKeyName(2, "");
            // 
            // imagesSmall
            // 
            this.imagesSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imagesSmall.ImageStream")));
            this.imagesSmall.TransparentColor = System.Drawing.Color.Transparent;
            this.imagesSmall.Images.SetKeyName(0, "");
            this.imagesSmall.Images.SetKeyName(1, "");
            this.imagesSmall.Images.SetKeyName(2, "");
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(128, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 256);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(131, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(341, 256);
            this.panel1.TabIndex = 2;
            // 
            // ShowResourceEditor
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.listView1);
            this.Name = "ShowResourceEditor";
            this.Size = new System.Drawing.Size(472, 256);
            this.ResumeLayout(false);

		}
		#endregion

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			if ( this.listView1.SelectedItems.Count < 1 )
				return;

			var lviSelected = this.listView1.SelectedItems[0];
		    var res = (Resource) lviSelected.Tag;
			string resName = lviSelected.Text;
			if ( MessageBox.Show( "VaultAtlas", "Are you sure you want to delete the resource "+resName+" ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2 ) != DialogResult.Yes )
				return;

			lviSelected.Selected = false;
			lviSelected.Remove();
			res.Row.Delete();
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			this.menuItem1.Checked = true;
			this.listView1.View = View.SmallIcon;
			this.menuItem3.Checked = false;
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			this.menuItem3.Checked = true;
			this.listView1.View = View.LargeIcon;
			this.menuItem1.Checked = false;
		}

	    private void ShowResourceEditor_Load(object sender, EventArgs e)
	    {
	        var fileDrop = new FileDrop(this.listView1);
	        fileDrop.FileDropped += fileDrop_FileDropped;
	    }

	    private Show _show;
		public Show EditedShow 
		{
			get 
			{
				return _show;
			}
			set 
			{
			    if (_show != value && value != null)
			    {
			        _show = value;

                    ListResources();
			    }
			}
		}

        public AdapterBase Adapter { get; private set; }

	    private void ListResources()
	    {
	        panel1.SuspendLayout();
	        listView1.Items.Clear();
	        listView1.BeginUpdate();

	        Adapter = _show.GetResourcesAdapter();

            foreach (var res in Adapter.Table.Rows.Cast<DataRow>().Select(r => new Resource(r)))
            {
                var lvi = BuildListViewItem(res);
                listView1.Items.Add(lvi);
            }

	        if (listView1.Items.Count > 0)
	            listView1.Items[0].Selected = true;
	        else
	            panel1.Controls.Clear();

	        listView1.EndUpdate();
	        panel1.ResumeLayout();
	    }

	    private static ListViewItem BuildListViewItem(Resource res)
	    {
	        var lvi = new ListViewItem(res.Key);
	        var imageIndex = 0;
	        if (res.ResourceType.Equals(string.Empty) || res.ResourceType.Equals("Text"))
	            imageIndex = 1;
	        else if (res.ResourceType.Equals("URL"))
	            imageIndex = 2;

	        lvi.StateImageIndex = lvi.ImageIndex = imageIndex;
	        lvi.Tag = res;
	        return lvi;
	    }

	    private void menuItem6_Click(object sender, EventArgs e)
	    {
	        EnterNewResource(string.Empty, "Text");
	    }

	    private void EnterNewResource( string resourceValue, string resourceType )
		{
	        var res = new Resource(Adapter.Table.NewRow())
	        {
	            Key = "new resource " + (listView1.Items.Count),
                ResourceType = resourceType,
                UidShow = _show.UID,
                Value = resourceValue
	        };
	        res.Row.Table.Rows.Add(res.Row);

            listView1.BeginUpdate();
	        try
	        {
	            var lvi = BuildListViewItem(res);
	            listView1.Items.Add(lvi);
                lvi.Selected = true;
                lvi.BeginEdit();
            }
	        finally
	        {
	            listView1.EndUpdate();
	        }
		}

		private void listView1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			var lvi = listView1.SelectedItems.Count == 0 ? null : this.listView1.SelectedItems[0];
			if ( lvi != null ) 
			{
                var oldControl = this.panel1.Controls.Count == 1 ? this.panel1.Controls[0] : null;
                if( oldControl == null )
				    panel1.Controls.Clear();

			    var res = (Resource) lvi.Tag;

				switch( res.ResourceType )
				{
					case "URL":
					{
                        var browser = oldControl as WebBrowser;
                        if (browser == null)
                        {
                            browser = new WebBrowser {Dock = DockStyle.Fill};
                            panel1.Controls.Clear();
                            panel1.Controls.Add(browser);
                        }
                        browser.Navigate(res.Value, null);
                        break;
					}

				    default: 
					{
                        var textEd = oldControl as ShowResourceTextEditor;
                        if (textEd == null)
                        {
                            textEd = new ShowResourceTextEditor {Resource = res};
                            panel1.Controls.Clear();
                            panel1.Controls.Add(textEd);
                            textEd.Dock = DockStyle.Fill;
                        }

					    textEd.Resource = res;
                        textEd.Value = res.Value;
						
						break;
					}
				}

				panel1.ResumeLayout();
			}
		}

		private void listView1_BeforeLabelEdit(object sender, System.Windows.Forms.LabelEditEventArgs e)
		{
			foreach( ListViewItem lvi in this.listView1.Items ) 
			{
				if ( lvi != this.listView1.Items[ e.Item ] && lvi.Text.Equals( e.Label )) 
				{
                    MessageBox.Show( resources.NameAlreadyAssignedToResource, "VaultAtlas", MessageBoxButtons.OK, MessageBoxIcon.Error);
					e.CancelEdit = true;
					return;
				}
			}
		}

		private void listView1_AfterLabelEdit(object sender, LabelEditEventArgs e)
		{
			string newName = e.Label;
			ListViewItem lvi = this.listView1.Items[ e.Item ];
			var resource = (Resource) lvi.Tag;
			if ( newName == null ) 
			{
				e.CancelEdit = true;
				return;
			}
			if ( !newName.Equals( resource.Key )) 
			{
				resource.Key = newName;
			}
		}

	    public void OnShowRejectChanges()
	    {
            foreach (Control oldControl in panel1.Controls)
                oldControl.Dispose();
            
            foreach (var res in listView1.Items.Cast<ListViewItem>().Select(i => (Resource)i.Tag))
	        {
	            res.Row.RejectChanges();
	        }

	        panel1.Controls.Clear();

	        ListResources();
	    }

		private void menuItem7_Click(object sender, EventArgs e)
		{
			// add link
			var input = new InputDialog {Caption = "URL", Text = "Please enter a URL."};
		    if ( input.ShowDialog() == DialogResult.OK )
			{
				EnterNewResource( input.InputText, "URL" );
			}
		}

	    private void fileDrop_FileDropped(object sender, FileDropEventArgs e)
	    {
	        var resValue = FileTool.TolerantInfoFileRead(e.FileName);
	        if (!string.IsNullOrEmpty(resValue))
	            EnterNewResource(resValue, "Text");
	    }
	}
}
