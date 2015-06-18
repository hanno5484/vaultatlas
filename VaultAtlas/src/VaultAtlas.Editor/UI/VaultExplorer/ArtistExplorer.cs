using System;
using System.Linq;
using VaultAtlas.DataModel;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;


namespace VaultAtlas.UI.VaultExplorer
{
	public class ArtistExplorer : UserControl, IComparer
	{
		private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
        private CheckBox checkBox1;
        private CheckBox checkBox2;

		private System.ComponentModel.Container components = null;

		public ArtistExplorer()
		{
			InitializeComponent();

			this.Load +=new EventHandler(ArtistExplorer_Load);
		}

		/// <summary> 
		/// Clean up any resources being used.
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 16);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(238, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(250, 45);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search for an artist";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(133, 301);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 22);
            this.button1.TabIndex = 0;
            this.button1.Text = "&Add new artist ...";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listView1
            // 
            this.listView1.AutoArrange = false;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.Location = new System.Drawing.Point(3, 64);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(250, 184);
            this.listView1.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.UpdateArtistFilter);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "# shows";
            // 
            // checkBox1
            // 
            this.checkBox1.Location = new System.Drawing.Point(9, 254);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(191, 25);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "&Display entries by selected artists only";
            this.checkBox1.CheckedChanged += new System.EventHandler(this.UpdateArtistFilter);
            // 
            // checkBox2
            // 
            this.checkBox2.Location = new System.Drawing.Point(9, 276);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(191, 19);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "Sort by show count";
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // ArtistExplorer
            // 
            this.ClientSize = new System.Drawing.Size(256, 331);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.groupBox1);
            this.Name = "ArtistExplorer";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "Artists";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private void textBox1_TextChanged(object sender, System.EventArgs e)
		{
			string prefix = this.textBox1.Text.ToLower();
			foreach(ListViewItem item in this.listView1.Items)
				if (item.Text.ToLower().StartsWith(prefix)) 
				{
					item.Selected = true;
					return;
				}
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			VaultAtlasApplication.RequestEnterArtist( "" );
		}

		public event EventHandler SelectedArtistChanged;

		public Artist SelectedArtist
		{
			get 
			{
                return this.listView1.SelectedItems.Count > 0 ? this.listView1.SelectedItems[0].Tag as Artist : null;
			}
		}

		private void treeView1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if (SelectedArtistChanged != null && e.Node != null)
				this.SelectedArtistChanged(this, new EventArgs());
			this.UpdateArtistFilter(sender,e);
		}

		private void Artist_DisplayNameChanged(object sender, EventArgs e)
		{
			Artist artist = (Artist) sender;
			foreach(ListViewItem item in this.listView1.Items)
				if (item.Tag == artist)
					item.Text = artist.DisplayName;
			            
		}

        private void UpdateArtistFilter(object sender, System.EventArgs e)
        {
            if (this.SelectedArtist != null)
                VaultAtlasApplication.Model.ShowView.Filter.SelectedArtist = this.checkBox1.Checked ? this.SelectedArtist : null;
        }

		private void listView1_DoubleClick(object sender, System.EventArgs e)
		{
			if (this.SelectedArtist != null) 
			{
				DialogArtist dialog = new DialogArtist( this.SelectedArtist );
				dialog.ShowDialog();
			}
		}

		private void ArtistExplorer_Load(object sender, EventArgs e)
		{
			this.FillListView();
		}

		private void FillListView() 
		{
			this.listView1.BeginUpdate();
			this.listView1.Items.Clear();
            /* TODO QUANTUM
			foreach(Artist art in VaultAtlasApplication.Model.Artists) 
			{
				this.listView1.Items.Add( new ListViewItem( new string[]{ art.DisplayName, "0" } )).Tag = art;
				art.DisplayNameChanged +=new EventHandler(Artist_DisplayNameChanged);
			}
             */
			this.listView1.EndUpdate();
		}

		private void Model_ShowAdded(object sender, ShowEventArgs e)
		{
			this.RecalculateShowCount( this.GetItemForArtist( e.Show.Artist ));
		}

		private IComparer comparerSave;

		private void checkBox2_CheckedChanged(object sender, System.EventArgs e)
		{
			this.listView1.BeginUpdate();
			if ( this.checkBox2.Checked ) 
			{
				this.comparerSave = this.listView1.ListViewItemSorter;
				this.listView1.ListViewItemSorter = this;
			} 
			else 
			{
				this.listView1.ListViewItemSorter = this.comparerSave;
			}
			this.listView1.EndUpdate();
		}

		#region IComparer Member

		public int Compare(object x, object y)
		{
			return Int32.Parse( ((ListViewItem)y).SubItems[1].Text ).CompareTo( Int32.Parse( ((ListViewItem)x).SubItems[1].Text ) );
		}

		#endregion

		private void Model_ShowChanging(object sender, ShowEventArgs e)
		{
            /* TODO QUANTUM
			string artist = e.Show.GetOriginalValue( "Artist" ).ToString();
			string artistNew = e.Show.ArtistSortName;
			
			if ( !artist.Equals( artistNew )) 
			{
				ListViewItem lvi = this.GetItemForArtist( VaultAtlasApplication.Model.Artists[ artist ] );
				ListViewItem lviNew = this.GetItemForArtist( VaultAtlasApplication.Model.Artists[ artistNew ] );

				this.RecalculateShowCount( lvi );
				this.RecalculateShowCount( lviNew );
			}
             */
		}

		private void RecalculateShowCount( ListViewItem lvi ) 
		{
			lvi.SubItems[1].Text = ((Artist)lvi.Tag).GetShowCount().ToString();
		}

		private ListViewItem GetItemForArtist( Artist art )
		{
		    return listView1.Items.Cast<ListViewItem>().FirstOrDefault(lvi => lvi.Tag == art);
		}
	}
}
