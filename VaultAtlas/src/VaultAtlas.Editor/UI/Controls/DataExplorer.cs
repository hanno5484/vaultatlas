using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using VaultAtlas.DataModel;

namespace VaultAtlas.UI.Controls
{
	public class DataExplorer : UserControl
    {
        private IContainer components;
        private Panel panel1;
        private ShowGridView showList;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem filterByCityToolStripMenuItem;
        private ToolStripMenuItem filterByBadToolStripMenuItem;
        private ToolStripMenuItem filterByCategoryToolStripMenuItem;
        private ToolStrip toolStrip1;
        private ToolStripTextBox toolStripTextBox1;
        private ToolStripComboBox toolStripComboBox1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripTextBox toolStripTextBox2;
        private ToolStripLabel toolStripLabel1;
        private ToolStripMenuItem clearFiltersToolStripMenuItem;

		public DataExplorer()
		{
		    InitializeComponent();

		    Load += DataExplorer_Load;

		    // this.dataGrid1.CurrentCellChanged +=new EventHandler(DataExplorer_CurrentChanged);

		    this.cellChangedTimer.Interval = 200;
		    this.cellChangedTimer.Tick += new EventHandler(cellChangedTimer_Tick);

		    panel1.ClientSize = new Size(2000, this.panel1.ClientSize.Height);
		    panel1.Location = new Point(10, 5);

		    base.BackColor = Color.White;
		    
            showList.ItemClicked += showList_ItemClicked;
        }

	    public event EventHandler CurrentCellChanged;

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

		
		public int RowNumber 
		{
			get 
			{
                return this.showList != null && this.showList.CurrentRow != null ? this.showList.CurrentRow.Index : -1;
			}
            set
            {
                foreach (DataGridViewRow viewRow in this.showList.SelectedRows)
                    viewRow.Selected = false;

                if (value != -1)
                {
                    this.showList.Rows[value].Selected = true;
                    this.showList.CurrentCell = this.showList[0, value];
                    this.showList.FirstDisplayedScrollingRowIndex = value;
                }
            }
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataExplorer));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterByCityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterByBadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterByCategoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearFiltersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripTextBox2 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.showList = new VaultAtlas.UI.Controls.ShowGridView();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.showList)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.ForeColor = System.Drawing.Color.Gray;
            this.panel1.Location = new System.Drawing.Point(0, 178);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(469, 35);
            this.panel1.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.filterByCityToolStripMenuItem,
            this.filterByBadToolStripMenuItem,
            this.filterByCategoryToolStripMenuItem,
            this.clearFiltersToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(171, 114);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // filterByCityToolStripMenuItem
            // 
            this.filterByCityToolStripMenuItem.Name = "filterByCityToolStripMenuItem";
            this.filterByCityToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.filterByCityToolStripMenuItem.Text = "Filter by city";
            this.filterByCityToolStripMenuItem.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // filterByBadToolStripMenuItem
            // 
            this.filterByBadToolStripMenuItem.Name = "filterByBadToolStripMenuItem";
            this.filterByBadToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.filterByBadToolStripMenuItem.Text = "Filter by band";
            this.filterByBadToolStripMenuItem.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // filterByCategoryToolStripMenuItem
            // 
            this.filterByCategoryToolStripMenuItem.Name = "filterByCategoryToolStripMenuItem";
            this.filterByCategoryToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.filterByCategoryToolStripMenuItem.Text = "Filter by category";
            // 
            // clearFiltersToolStripMenuItem
            // 
            this.clearFiltersToolStripMenuItem.Name = "clearFiltersToolStripMenuItem";
            this.clearFiltersToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.clearFiltersToolStripMenuItem.Text = "Clear filters";
            this.clearFiltersToolStripMenuItem.Click += new System.EventHandler(this.menuItem4_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AllowMerge = false;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripComboBox1,
            this.toolStripTextBox2,
            this.toolStripSeparator1,
            this.toolStripTextBox1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(469, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripLabel1.Image")));
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Padding = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.toolStripLabel1.Size = new System.Drawing.Size(53, 22);
            this.toolStripLabel1.Text = "Filter";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox1.Items.AddRange(new object[] {
            "alle Einträge",
            "geändert nach",
            "geändert vor"});
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 25);
            this.toolStripComboBox1.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
            // 
            // toolStripTextBox2
            // 
            this.toolStripTextBox2.AutoSize = false;
            this.toolStripTextBox2.Enabled = false;
            this.toolStripTextBox2.Name = "toolStripTextBox2";
            this.toolStripTextBox2.Size = new System.Drawing.Size(70, 25);
            this.toolStripTextBox2.Text = "01.01.2007 ";
            this.toolStripTextBox2.Validating += new System.ComponentModel.CancelEventHandler(this.toolStripTextBox2_Validating);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(14, 25);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 25);
            this.toolStripTextBox1.ToolTipText = "Filterbegriff";
            this.toolStripTextBox1.TextChanged += new System.EventHandler(this.toolStripTextBox1_TextChanged);
            // 
            // showList
            // 
            this.showList.AllowUserToAddRows = false;
            this.showList.AllowUserToDeleteRows = false;
            this.showList.AllowUserToOrderColumns = true;
            this.showList.AllowUserToResizeRows = false;
            this.showList.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.showList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.showList.ColumnHeadersHeight = 18;
            this.showList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.showList.ContextMenuStrip = this.contextMenuStrip1;
            this.showList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.showList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.showList.Font = new System.Drawing.Font("Tahoma", 9F);
            this.showList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.showList.Location = new System.Drawing.Point(0, 25);
            this.showList.Margin = new System.Windows.Forms.Padding(0);
            this.showList.Name = "showList";
            this.showList.ReadOnly = true;
            this.showList.RowHeadersVisible = false;
            this.showList.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.showList.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.showList.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Maroon;
            this.showList.RowTemplate.Height = 20;
            this.showList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.showList.ShowCellErrors = false;
		    this.showList.ShowCellToolTips = false;
            this.showList.ShowEditingIcon = false;
            this.showList.ShowRowErrors = false;
            this.showList.Size = new System.Drawing.Size(469, 153);
            this.showList.TabIndex = 1;
            // 
            // DataExplorer
            // 
            this.Controls.Add(this.showList);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "DataExplorer";
            this.Size = new System.Drawing.Size(469, 213);
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.showList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        void contextMenu1_Collapse(object sender, EventArgs e)
        {
            this.Invalidate();
        }
		#endregion

		private bool modelevtHandlerRegistered = false;

		/// <summary>
		/// Timer used to defer CurrentCellChanged events, so only one event is fired at most every 200 ms.
		/// </summary>
		private Timer cellChangedTimer = new Timer();

		public CustomDataView DataSource 
		{
			get 
			{
				return this.showList.DataSource as CustomDataView;
			}
			set 
			{
				this.showList.DataSource = value;
			}
		}

		public Show CurrentShow 
		{
			get 
			{
				int currentRow = this.showList.CurrentRow.Index;
				if ( currentRow == -1 )
					return null;
				return DataModel.Show.GetShow( ((CustomDataView)this.showList.DataSource)[ currentRow ].Row );
			}
		}

	    private void DataExplorer_Load(object sender, EventArgs e)
	    {
	        if (DesignMode)
	            return;

	        DataSource = VaultAtlasApplication.Model.ShowView;
            VaultAtlasApplication.Model.ShowView.ListChanged += ShowView_ListChanged;
            VaultAtlasApplication.Model.ModelNavigator = showList;

	    }

	    private void cellChangedTimer_Tick(object sender, EventArgs e)
		{
			if (this.CurrentCellChanged != null)
				this.CurrentCellChanged(this,e);
			this.cellChangedTimer.Stop();
		}

		private void ShowView_ListChanged(object sender, ListChangedEventArgs e)
		{
            VaultAtlasApplication.MainForm.UpdateStatusBarPanelLineNumber();
		}

        private void contextMenu1_Popup(object sender, System.EventArgs e)
        {
            /* TODO
            int showAtPoint = this.showList.GetItemAtPosition(MousePosition);
            Show show = null;
            if (showAtPoint != -1)
            {
                this.showList.CurrentRow = showAtPoint;
                show = this.CurrentShow;
            }
            */

            Show show = this.CurrentShow;
            if (show == null)
            {
                /*
                this.menuItem1.Enabled = this.menuItem2.Enabled = this.menuItem3.Enabled
                    = this.menuItem8.Enabled = this.menuItem9.Enabled = this.menuItem10.Enabled = this.menuItem11.Enabled = false;
                 * */
                return;
            }
            else
            {
                /*
                foreach (MenuItem mi in this.contextMenu1.MenuItems)
                    mi.Enabled = true;

                string city = show.City;
                this.menuItem1.Enabled = city != null && !city.Equals(string.Empty);
                string artist = show.Artist != null ? show.ArtistSortName : string.Empty;
                this.menuItem2.Enabled = artist != null && !artist.Equals(string.Empty);
                 * */

            }

            // fill categories
            /*
            this.menuItem6.MenuItems.Clear();
            string currentCategory = VaultAtlasApplication.Model.ShowView.Filter.CategoryFilter;
            foreach (string category in VaultAtlasApplication.Model.Categories)
            {
                MenuItem mi = new MenuItem(category, new EventHandler(this.SelectCategory));

                if (currentCategory == category)
                    mi.Checked = true;

                this.menuItem6.MenuItems.Add(mi);

            }

            this.menuItem6.Enabled = this.menuItem6.MenuItems.Count > 0;
             * */
        }

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			// filter by city
			Show show = this.CurrentShow;
			if ( show != null )
				this.DataSource.Filter.CustomFilterExpression = "City = '"+Util.MakeSelectSafe( show.City )+"'";
			this.VerifyViewNotEmpty();
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			// filter by band
			Show show = this.CurrentShow;
			if ( show != null && show.Artist != null )
				this.DataSource.Filter.CustomFilterExpression = "Artist = '"+Util.MakeSelectSafe( show.Artist.SortName )+"'";
			this.VerifyViewNotEmpty();
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			// open for editing
			Show show = this.CurrentShow;
			if ( show != null )
				VaultAtlasApplication.MainForm.RequestEditShow( this.DataSource.GetShowRowIndex( show ), show );
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			// clear filters
			this.DataSource.Filter.ClearFilters();
		}

		private void showList_ItemClicked(object sender, ShowEventArgs e)
		{
			VaultAtlas.VaultAtlasApplication.MainForm.RequestEditShow( e.ShowIndex, e.Show );
		}

		public void SelectCategory( object sender, System.EventArgs e )
		{
			MenuItem mi = sender as MenuItem;
			if ( mi != null ) 
			{
				string category = mi.Text;
				VaultAtlasApplication.Model.ShowView.Filter.CategoryFilter = category;
			}
			this.VerifyViewNotEmpty();
		}

		private void VerifyViewNotEmpty() 
		{
			if ( this.DataSource.Count == 0 ) 
			{
                MessageBox.Show(resources.NoElementsForFilter, resources.Information, MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DesignMode)
                return;

            switch (this.toolStripComboBox1.SelectedIndex)
            {
                case -1:
                case 0:
                    this.toolStripTextBox2.Enabled = false;
                    VaultAtlasApplication.Model.ShowView.Filter.WhereClause = string.Empty;
                    break;
                case 1:
                case 2:
                    this.toolStripTextBox2.Enabled = true;
                    this.toolStripTextBox2.TextBox.Focus();
                    break;
            }
        }

        private void toolStripTextBox2_Validating(object sender, CancelEventArgs e)
        {
            if (this.toolStripTextBox2.Text.Equals(string.Empty))
            {
                this.toolStripComboBox1.SelectedIndex = 0;
            }
            else
            {
                DateTime dt;
                if (!DateTime.TryParse(this.toolStripTextBox2.Text, out dt))
                {
                    e.Cancel = true;
                }
                else
                {
                    string s = "DateUpdated {1} '{0:yyyy-MM-dd} 00:00:00.000'";
                    s = string.Format(s, dt, this.toolStripComboBox1.SelectedIndex == 2 ? "<= " : ">=");
                    VaultAtlasApplication.Model.ShowView.Filter.WhereClause = s;
                }
            }
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            VaultAtlasApplication.Model.ShowView.Filter.TextFilter = this.toolStripTextBox1.Text;
        }

	}

}