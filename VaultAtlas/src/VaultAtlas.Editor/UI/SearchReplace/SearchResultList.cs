using System;
using VaultAtlas.UI.Controls;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using VaultAtlas.DataModel;


namespace VaultAtlas.UI.SearchReplace
{
	public class SearchResultList : UserControl
	{
		private System.ComponentModel.IContainer components;

		public SearchResultList()
		{
			InitializeComponent();
		}

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;

		private string searchTerm;
		public string SearchTerm 
		{
			get 
			{
				return this.searchTerm;
			}
			set 
			{
				this.searchTerm = value;
				this.Text = "Search results: "+searchTerm;
			}
		}

		private void AddNewItem( Show show )
		{
			ListViewItem lvi = new ListViewItem( new string[]{ show.Artist.DisplayName, show.Date.ToString(), show.City, show.Venue }, 0 );
			lvi.Tag = show;
			lvi.ImageIndex = 0;
			this.listView1.Items.Add( lvi );
		}

		public void DoSearch( bool ignoreCase, bool searchInResources )
		{
			this.listView1.Items.Clear();
			SearchState state = new SearchState( this.searchTerm, ignoreCase, 0 );
			state.Silent = true;
			state.SearchResources = searchInResources;
            /* TODO QUANTUM
			while ( state.Continue() ) 
			{
				Show show = VaultAtlasApplication.Model.Shows[ state.Row["UID"].ToString() ];
				this.AddNewItem( show );
			}
            */
			if ( this.listView1.Items.Count == 0) 
			{
				ListViewItem lvi = new ListViewItem( new string[]{ "No results found.", string.Empty, string.Empty, string.Empty }, 0 );
				this.listView1.Items.Add( lvi );
			}
		}
		
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SearchResultList));
			this.panel1 = new System.Windows.Forms.Panel();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.listView1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(296, 301);
			this.panel1.TabIndex = 0;
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1,
																						this.columnHeader2,
																						this.columnHeader3,
																						this.columnHeader4});
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.listView1.FullRowSelect = true;
			this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listView1.HoverSelection = true;
			this.listView1.Location = new System.Drawing.Point(0, 0);
			this.listView1.MultiSelect = false;
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(296, 301);
			this.listView1.SmallImageList = this.imageList1;
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Artist";
			this.columnHeader1.Width = 130;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Date";
			this.columnHeader2.Width = 110;
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "City";
			this.columnHeader3.Width = 100;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Venue";
			this.columnHeader4.Width = 200;
			// 
			// SearchResultList
			// 
			this.ClientSize = new System.Drawing.Size(296, 301);
			this.Controls.Add(this.panel1);
			this.Name = "SearchResultList";
			this.Text = "SearchResultList";
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void listView1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		}

		private void listView1_DoubleClick(object sender, System.EventArgs e)
		{
			if ( this.listView1.SelectedItems.Count > 0 ) 
			{
				Show show = this.listView1.SelectedItems[0].Tag as Show;
				if ( show != null ) 
				{
                    DataExplorer exp = null; // VaultAtlasApplication.MainForm.FindContent(typeof(DataExplorer)) as DataExplorer;
					int rowNumber = VaultAtlasApplication.Model.ShowView.GetShowRowIndex( show );
					if ( exp != null && rowNumber != -1 )
						exp.RowNumber = rowNumber;
					this.Focus();
				}
			}		
		}


	}
}
