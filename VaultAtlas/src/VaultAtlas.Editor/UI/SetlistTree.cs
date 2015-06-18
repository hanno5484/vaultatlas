using System;
using VaultAtlas.DataModel;

using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace VaultAtlas.UI
{
	public class SetlistTree : UserControl
	{
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		/// <summary> 
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.Container components = null;

	    public SetlistTree()
	    {
	        // Dieser Aufruf ist für den Windows Form-Designer erforderlich.
	        InitializeComponent();

	        this.LoadPlugIns();

	        base.Text = "Setlist Browser";

	        this.treeView1.AfterSelect += treeView1_AfterSelect;
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

		#region Vom Komponenten-Designer generierter Code
		/// <summary> 
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.panel1 = new System.Windows.Forms.Panel();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.ContextMenu = this.contextMenu1;
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
			this.treeView1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.treeView1.ImageIndex = -1;
			this.treeView1.Location = new System.Drawing.Point(0, 0);
			this.treeView1.Name = "treeView1";
			this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
																				  new System.Windows.Forms.TreeNode("Setlists")});
			this.treeView1.SelectedImageIndex = -1;
			this.treeView1.Size = new System.Drawing.Size(121, 149);
			this.treeView1.TabIndex = 0;
			this.treeView1.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeExpand);
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(121, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 149);
			this.splitter1.TabIndex = 1;
			this.splitter1.TabStop = false;
			// 
			// panel1
			// 
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(124, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(116, 149);
			this.panel1.TabIndex = 2;
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1});
			this.contextMenu1.Popup += new System.EventHandler(this.contextMenu1_Popup);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "Add entry ...";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// SetlistTree
			// 
			this.ClientSize = new System.Drawing.Size(240, 149);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.treeView1);
			this.Name = "SetlistTree";
			this.ResumeLayout(false);

		}
		#endregion

		private void LoadPlugIns() 
		{
			this.treeView1.BeginUpdate();
			foreach( ISetlistProvider setlistProvider in VaultAtlas.VaultAtlasApplication.Model.PlugInManager.SetlistProviders ) 
			{
				TreeNode newNode = new TreeNode( setlistProvider.Name );
				newNode.Tag = setlistProvider;
				foreach( string year in setlistProvider.GetYears()) 
				{
					TreeNode yearNode = new TreeNode( year );
					yearNode.Nodes.Add( "dummy" );
					yearNode.Tag = year;
					yearNode.NodeFont = new Font( "Tahoma", 8 );
					newNode.Nodes.Add( yearNode );
				}
				this.treeView1.Nodes[0].Nodes.Add( newNode );
			}
			this.treeView1.EndUpdate();

		}

		private void treeView1_BeforeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			if ( e.Node.Nodes.Count == 1 && e.Node.Nodes[0].Text.Equals("dummy")) 
			{
				e.Node.Nodes.Clear();
				ISetlistProvider prov = e.Node.Parent.Tag as ISetlistProvider;
				if ( prov != null ) 
				{
					this.treeView1.BeginUpdate();
					foreach( IShowAbstract resultItem in prov.GetEntriesForYear( e.Node.Tag as string )) 
					{
						TreeNode newNode = new TreeNode( resultItem.ToString());
						newNode.Tag = resultItem;
						newNode.NodeFont = new Font( "Tahoma", 8 );
						e.Node.Nodes.Add( newNode );
					}
					this.treeView1.EndUpdate();
				}

			}
		}

		private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if ( e.Node.Parent != null && e.Node.Parent.Parent != null ) 
			{
				ISetlistProvider prov = e.Node.Parent.Parent.Tag as ISetlistProvider;
				if ( prov != null ) 
				{
					Control newCtl = prov.GetControl( e.Node.Tag );
					if ( this.panel1.Controls.Count != 1 || this.panel1.Controls[0] != newCtl ) 
					{
						this.panel1.SuspendLayout();
						this.panel1.Controls.Clear();
						this.panel1.Controls.Add( newCtl );
						this.panel1.ResumeLayout();
					}
				}
			}
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			// add entry for selected setlist-tree entry
			TreeNode node = this.treeView1.SelectedNode;
			if ( node != null && node.Tag != null && node.Tag is IShowAbstract ) 
			{
				IShowAbstract show = node.Tag as IShowAbstract;
				Show newShow = VaultAtlasApplication.Model.NewShow();
				newShow.City = show.City;
				newShow.Venue = show.Venue;
				newShow.Date = show.Date;

                /* TODO QUANTUM
				if ( VaultAtlasApplication.Model.Artists[ show.Artist ] == null ) 
					VaultAtlasApplication.Model.NewArtist( show.Artist ).Enter();
                */

			    newShow.ArtistSortName = show.ArtistSortName;
				VaultAtlasApplication.MainForm.RequestEditShow( -1, newShow);
			}
		}

		private void contextMenu1_Popup(object sender, System.EventArgs e)
		{
			TreeNode node = this.treeView1.SelectedNode;
			this.menuItem1.Enabled = node != null && node.Tag != null && node.Tag is IShowAbstract;
		}
	}
}
