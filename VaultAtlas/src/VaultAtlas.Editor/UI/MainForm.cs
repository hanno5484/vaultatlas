using System;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using VaultAtlas.DataModel.FlacAtlas;
using VaultAtlas.FlacAtlas;
using VaultAtlas.Properties;
using VaultAtlas.UI.SearchReplace;
using VaultAtlas.DataModel.ModelUI;
using VaultAtlas.DataModel;
using VaultAtlas.UI;
using VaultAtlas.UI.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;

namespace VaultAtlas
{
	public class MainForm : System.Windows.Forms.Form, IPlugInEnvironment
	{

		private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.StatusBarPanel statusBarPanel1;
		private System.Windows.Forms.StatusBarPanel statusBarPanelLineNumber;
        private System.Windows.Forms.StatusBarPanel statusBarPanelContent;

        private IContainer components;

		private static char[] digits = "1234567890".ToCharArray();

        private System.Windows.Forms.MenuItem menuItem41;
        private DataExplorer dataExplorer1;
        private TabControl tabSearch;
        private TabPage tabPage1;
        private ImageList imageList1;
        private MenuItem menuMatchFlacAtlas;

		private readonly IList<Show> _recentlyEdited = new List<Show>( 5 );
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuExit;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem21;
        private System.Windows.Forms.MenuItem menuItem22;
        private System.Windows.Forms.MenuItem menuItem24;
		private ShowListMenuItem MenuItem_RecentlyEdited;
		private System.Windows.Forms.MenuItem menuItem28;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuItem23;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem menuItem19;
        private System.Windows.Forms.MenuItem menuItem15;
        private TabPage tabPageDiscs;
        private FlacAtlasControl flacAtlasControl1;
        private TabPage tabPage2;
        private SearchControl searchControl1;
        private MenuItem menuImportDisc;
        private MenuItem menuItemImportHardDrive;
        private MenuItem menuItem2;
        private System.Windows.Forms.MainMenu mainMenu1;
	
		public IList<Show> RecentlyEdited 
		{
			get 
			{
				return this._recentlyEdited;
			}
		}

		public MainForm()
		{
			InitializeComponent();
			VaultAtlasApplication.MainForm = this;
			VaultAtlasApplication.Model.PlugInManager.LoadPlugIn( new BuiltInPlugIn());
			this.Load += MainForm_Load;
			this.Focus();
		}

		/// <summary>
		/// Clean up any resources being used.
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.statusBarPanel1 = new System.Windows.Forms.StatusBarPanel();
            this.statusBarPanelContent = new System.Windows.Forms.StatusBarPanel();
            this.statusBarPanelLineNumber = new System.Windows.Forms.StatusBarPanel();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuExit = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuItem21 = new System.Windows.Forms.MenuItem();
            this.menuItem22 = new System.Windows.Forms.MenuItem();
            this.menuItem24 = new System.Windows.Forms.MenuItem();
            this.MenuItem_RecentlyEdited = new VaultAtlas.UI.Controls.ShowListMenuItem();
            this.menuItem28 = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem23 = new System.Windows.Forms.MenuItem();
            this.menuImportDisc = new System.Windows.Forms.MenuItem();
            this.menuItemImportHardDrive = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.menuItem19 = new System.Windows.Forms.MenuItem();
            this.menuItem41 = new System.Windows.Forms.MenuItem();
            this.menuItem15 = new System.Windows.Forms.MenuItem();
            this.menuMatchFlacAtlas = new System.Windows.Forms.MenuItem();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.tabSearch = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataExplorer1 = new VaultAtlas.UI.Controls.DataExplorer();
            this.tabPageDiscs = new System.Windows.Forms.TabPage();
            this.flacAtlasControl1 = new VaultAtlas.FlacAtlas.FlacAtlasControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.searchControl1 = new VaultAtlas.UI.SearchControl();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanelContent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanelLineNumber)).BeginInit();
            this.tabSearch.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPageDiscs.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusBar1
            // 
            resources.ApplyResources(this.statusBar1, "statusBar1");
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.statusBarPanel1,
            this.statusBarPanelContent,
            this.statusBarPanelLineNumber});
            this.statusBar1.ShowPanels = true;
            this.statusBar1.SizingGrip = false;
            // 
            // statusBarPanel1
            // 
            resources.ApplyResources(this.statusBarPanel1, "statusBarPanel1");
            // 
            // statusBarPanelContent
            // 
            resources.ApplyResources(this.statusBarPanelContent, "statusBarPanelContent");
            // 
            // statusBarPanelLineNumber
            // 
            resources.ApplyResources(this.statusBarPanelLineNumber, "statusBarPanelLineNumber");
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuExit});
            resources.ApplyResources(this.menuItem1, "menuItem1");
            // 
            // menuExit
            // 
            this.menuExit.Index = 0;
            resources.ApplyResources(this.menuExit, "menuExit");
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 1;
            this.menuItem8.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem21,
            this.menuItem22});
            resources.ApplyResources(this.menuItem8, "menuItem8");
            // 
            // menuItem21
            // 
            resources.ApplyResources(this.menuItem21, "menuItem21");
            this.menuItem21.Index = 0;
            this.menuItem21.Click += new System.EventHandler(this.menuItem21_Click);
            // 
            // menuItem22
            // 
            resources.ApplyResources(this.menuItem22, "menuItem22");
            this.menuItem22.Index = 1;
            this.menuItem22.Click += new System.EventHandler(this.menuItem22_Click);
            // 
            // menuItem24
            // 
            this.menuItem24.Index = 2;
            this.menuItem24.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItem_RecentlyEdited,
            this.menuItem28,
            this.menuItem9,
            this.menuItem23,
            this.menuImportDisc,
            this.menuItemImportHardDrive,
            this.menuItem2});
            resources.ApplyResources(this.menuItem24, "menuItem24");
            // 
            // MenuItem_RecentlyEdited
            // 
            this.MenuItem_RecentlyEdited.Index = 0;
            this.MenuItem_RecentlyEdited.ShowList = null;
            resources.ApplyResources(this.MenuItem_RecentlyEdited, "MenuItem_RecentlyEdited");
            // 
            // menuItem28
            // 
            this.menuItem28.Index = 1;
            resources.ApplyResources(this.menuItem28, "menuItem28");
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 2;
            resources.ApplyResources(this.menuItem9, "menuItem9");
            this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click);
            // 
            // menuItem23
            // 
            this.menuItem23.Index = 3;
            resources.ApplyResources(this.menuItem23, "menuItem23");
            this.menuItem23.Click += new System.EventHandler(this.menuItem23_Click);
            // 
            // menuImportDisc
            // 
            this.menuImportDisc.Index = 4;
            resources.ApplyResources(this.menuImportDisc, "menuImportDisc");
            this.menuImportDisc.Click += new System.EventHandler(this.menuImportDisc_Click);
            // 
            // menuItemImportHardDrive
            // 
            this.menuItemImportHardDrive.Index = 5;
            resources.ApplyResources(this.menuItemImportHardDrive, "menuItemImportHardDrive");
            this.menuItemImportHardDrive.Click += new System.EventHandler(this.menuItemImportHardDrive_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 6;
            resources.ApplyResources(this.menuItem2, "menuItem2");
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 3;
            this.menuItem10.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem19,
            this.menuItem41,
            this.menuItem15,
            this.menuMatchFlacAtlas});
            resources.ApplyResources(this.menuItem10, "menuItem10");
            this.menuItem10.Click += new System.EventHandler(this.menuItem10_Click);
            // 
            // menuItem19
            // 
            this.menuItem19.Index = 0;
            resources.ApplyResources(this.menuItem19, "menuItem19");
            this.menuItem19.Click += new System.EventHandler(this.menuItem19_Click);
            // 
            // menuItem41
            // 
            this.menuItem41.Index = 1;
            resources.ApplyResources(this.menuItem41, "menuItem41");
            this.menuItem41.Click += new System.EventHandler(this.menuItem41_Click);
            // 
            // menuItem15
            // 
            this.menuItem15.Index = 2;
            resources.ApplyResources(this.menuItem15, "menuItem15");
            this.menuItem15.Click += new System.EventHandler(this.menuItem15_Click);
            // 
            // menuMatchFlacAtlas
            // 
            this.menuMatchFlacAtlas.Index = 3;
            resources.ApplyResources(this.menuMatchFlacAtlas, "menuMatchFlacAtlas");
            this.menuMatchFlacAtlas.Click += new System.EventHandler(this.menuMatchFlacAtlas_Click);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem8,
            this.menuItem24,
            this.menuItem10});
            // 
            // tabSearch
            // 
            resources.ApplyResources(this.tabSearch, "tabSearch");
            this.tabSearch.Controls.Add(this.tabPage1);
            this.tabSearch.Controls.Add(this.tabPageDiscs);
            this.tabSearch.Controls.Add(this.tabPage2);
            this.tabSearch.ImageList = this.imageList1;
            this.tabSearch.Name = "tabSearch";
            this.tabSearch.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.dataExplorer1);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataExplorer1
            // 
            this.dataExplorer1.BackColor = System.Drawing.Color.White;
            this.dataExplorer1.DataSource = null;
            resources.ApplyResources(this.dataExplorer1, "dataExplorer1");
            this.dataExplorer1.Name = "dataExplorer1";
            this.dataExplorer1.RowNumber = -1;
            // 
            // tabPageDiscs
            // 
            this.tabPageDiscs.Controls.Add(this.flacAtlasControl1);
            resources.ApplyResources(this.tabPageDiscs, "tabPageDiscs");
            this.tabPageDiscs.Name = "tabPageDiscs";
            this.tabPageDiscs.UseVisualStyleBackColor = true;
            // 
            // flacAtlasControl1
            // 
            resources.ApplyResources(this.flacAtlasControl1, "flacAtlasControl1");
            this.flacAtlasControl1.Name = "flacAtlasControl1";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.searchControl1);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // searchControl1
            // 
            resources.ApplyResources(this.searchControl1, "searchControl1");
            this.searchControl1.Name = "searchControl1";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Movie Document 16 h p.png");
            this.imageList1.Images.SetKeyName(1, "Music Document 16 h p.png");
            this.imageList1.Images.SetKeyName(2, "Organizer 16 h p.png");
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.tabSearch);
            this.Controls.Add(this.statusBar1);
            this.Menu = this.mainMenu1;
            this.Name = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanelContent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanelLineNumber)).EndInit();
            this.tabSearch.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPageDiscs.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			try 
			{
				Application.DoEvents();
				MainForm mf = new MainForm();
				Application.Run( mf );
			} 
			catch (Exception exc) 
			{
				Console.WriteLine( exc.Message+" "+exc.StackTrace );
				MessageBox.Show( exc.Message+" "+exc.StackTrace );
			}
		}

		private void menuItem9_Click(object sender, EventArgs e)
		{
			try 
			{
				VaultAtlasApplication.RequestEnterShow("", new ShowDate("????-??-??"), "");
			} 
			catch (Exception exc)
			{
				MessageBox.Show(exc.Message +" - " +exc.StackTrace);
			}
		}

        public void UpdateStatusBarPanelLineNumber()
        {
            BeginInvoke(new Action(delegate {
                this.statusBarPanelLineNumber.Text = this.dataExplorer1.RowNumber + " : " + VaultAtlasApplication.Model.ShowView.Count;

            }));
        }

        public void RequestCloseTab(Control ctl)
        {
            Control p = ctl;
            while (p != null && !(p is TabPage))
                p = p.Parent;
            if (p is TabPage)
            {
                this.tabSearch.TabPages.Remove((TabPage)p);
            }
        }

	    private void menuExit_Click(object sender, EventArgs e)
	    {
	        Application.Exit();
	    }

		private void menuItem19_Click(object sender, EventArgs e)
		{
			var exp = new UI.Export.XMLExporter();
            this.AddOwnedForm(exp);
            exp.StartPosition = FormStartPosition.CenterParent;
			exp.ShowDialog();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			var fileDrop = new FileDrop( this );
			fileDrop.FileDropped += FileDropped;

		    VaultAtlasApplication.Model.UndoRedoStatusChanged += Model_UndoRedoStatusChanged;

			this.MenuItem_RecentlyEdited.ShowList = _recentlyEdited;
		}

		private void FileDropped( object sender, FileDropEventArgs e ) 
		{
			ShowDate date = ShowDate.Now;
			Artist probableArtist = null;

			try 
			{
				string strippedFileName = Path.GetFileNameWithoutExtension( e.FileName );
				int firstDot = strippedFileName.IndexOf(".");
				if ( firstDot != -1 )
					strippedFileName = strippedFileName.Substring( 0, firstDot );
				int firstDigit = strippedFileName.IndexOfAny( digits );
				// int firstSeparator = strippedFileName.IndexOfAny( separators );
				string artistAbbrev = strippedFileName.Substring(0, firstDigit);
				string probableDate = strippedFileName.Substring(firstDigit);
				if (probableDate.Length > 2 && !probableDate.StartsWith("19") && !probableDate.StartsWith("20")) 
				{
					if (Int32.Parse( probableDate.Substring(0,2) ) < 50)
						probableDate = "20"+probableDate;
					else
						probableDate = "19"+probableDate;
				}
                if (probableDate.Length > 10)
                    probableDate = probableDate.Substring(0, 10);
				date = ShowDate.Parse( probableDate );
				probableArtist = VaultAtlasApplication.Model.Artists.FindByAbbreviation( artistAbbrev );
			} 
			catch {}

			var attr = File.GetAttributes(e.FileName);

			var directory = ((attr & FileAttributes.Directory) == FileAttributes.Directory) ? e.FileName : Path.GetDirectoryName(e.FileName);
			var mediaFileInfoProvider = new MediaFileInfoProvider(directory);

			//detect whether its a directory or file
			var resourceFileName = ((attr & FileAttributes.Directory) == FileAttributes.Directory) ? null : e.FileName;

            //avoid loading very long files
            if (resourceFileName != null)
            {
                if (new FileInfo(resourceFileName).Length > 30000)
                    resourceFileName = null;
            }

		    VaultAtlasApplication.RequestEnterShow(probableArtist != null ? probableArtist.SortName : null, date, resourceFileName, mediaFileInfoProvider.ApplyToShow);

		}


        private IEnumerable<TabPage> FindContentAll(Type contentType)
        {
            return tabSearch.TabPages.Cast<TabPage>()
                .Where(tabPage => tabPage.Tag != null && tabPage.Tag.GetType() == contentType);
        }

	    public void RequestEditShow( int showIndex, Show show ) 
		{
			TabPage tabPage = null;
            
            foreach (var tabP in this.FindContentAll(typeof(EditShow)))
            {
                if (((EditShow)tabP.Tag).EditedShow.UID.Equals(show.UID))
                    tabPage = tabP;
            }

			if ( tabPage == null ) 
			{
                tabPage = new TabPage();
                var es = new EditShow();
				es.Bind( showIndex, show);
                tabPage.Controls.Add(es);
                tabPage.Text = es.Text;
                es.Dock = DockStyle.Fill;
                tabPage.Tag = es;
                tabPage.ImageIndex = 1;
                this.tabSearch.TabPages.Add(tabPage);
			}

            this.tabSearch.SelectedTab = tabPage;

		}
		private void menuItem15_Click(object sender, EventArgs e)
		{
			var length = 0;
		    var countItems = 0;
			foreach( var show in VaultAtlasApplication.Model.ShowView.Shows.Shows )
			{
			    countItems++;

				try 
				{
					var showLength = show.Length;
					if (showLength.EndsWith("'"))
						length += Int32.Parse( showLength.Substring(0, showLength.Length-1 ));
					else if (showLength.EndsWith(" DVD"))
						length += 90*Int32.Parse( showLength.Substring(0, showLength.Length-4 ));
					else if (showLength.EndsWith(" CD"))
					    length += 60*Int32.Parse(showLength.Substring(0, showLength.Length - 3));
					else
					{
                        int lengthInt;
                        if (Int32.TryParse(showLength, out lengthInt))
                            length += lengthInt;
					}
				}
				catch 
				{
				}
			}
            MessageBox.Show(string.Format(resources.ApproximatelyXHours, (length / 60)+"", countItems.ToString()));
		}

		private void Model_UndoRedoStatusChanged(object sender, DataModel.UndoRedo.UndoRedoStateEventArgs args)
		{
		    this.Text = Constants.ApplicationName + (args.IsSaved ? "" : " *");

			this.menuItem21.Enabled = args.CanUndo;
			this.menuItem22.Enabled = args.CanRedo;
		}

		private void menuItem10_Click(object sender, System.EventArgs e)
		{
		
		}
		private void menuItem21_Click(object sender, EventArgs e)
		{
			VaultAtlasApplication.Model.UndoRedoManager.UndoAction();
		}

		private void menuItem22_Click(object sender, EventArgs e)
		{
			VaultAtlasApplication.Model.UndoRedoManager.RedoAction();
		}

		private void menuItem41_Click(object sender, System.EventArgs e)
		{
			var export = new VaultAtlas.UI.Export.ExcelExporter();
			var sfd = new SaveFileDialog();
			sfd.Title = Resources.SaveFile;
			if (sfd.ShowDialog() != DialogResult.OK)
				return;
			export.WriteExcelFile( sfd.FileName, false );
		}

        private void menuItem23_Click(object sender, EventArgs e)
        {
            VaultAtlasApplication.RequestEnterArtist("");
        }

        private void menuMatchFlacAtlas_Click(object sender, EventArgs e)
        {
            IProgressCallback progressCallback = null;

            new ShowDirectoryMatcher().Match(async (path, show) =>
            {
                if (progressCallback == null)
                    progressCallback = flacAtlasControl1.GetProgressDialog();

                var ddi = await flacAtlasControl1.ImportLocalFolderStructure(path, progressCallback).ConfigureAwait(false);
                show.UidDirectory = ddi.UID;
            });

            Model.SingleModel.Shows.Adapter.Update(Model.SingleModel.Shows.Table);
        }

	    public Model Model
	    {
	        get { return Model.SingleModel; }
	    }

        private void menuImportDisc_Click(object sender, EventArgs e)
        {
            flacAtlasControl1.ImportDisc().ConfigureAwait(false);
        }

        private void menuItemImportHardDrive_Click(object sender, EventArgs e)
        {
            flacAtlasControl1.ImportHardDrive();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            flacAtlasControl1.ImportLocalFolderStructure().ConfigureAwait(false);
        }
	}
}
