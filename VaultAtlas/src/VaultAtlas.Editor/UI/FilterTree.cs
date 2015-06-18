using System;
using VaultAtlas.DataModel;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace VaultAtlas.UI
{
	public class FilterTree : UserControl
	{
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.ImageList imageList1;
		private System.ComponentModel.IContainer components;

		public FilterTree()
		{
			// Dieser Aufruf ist für den Windows Form-Designer erforderlich.
			InitializeComponent();

			// TODO: Initialisierungen nach dem Aufruf von InitializeComponent hinzufügen

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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FilterTree));
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.treeView1.ImageList = this.imageList1;
			this.treeView1.Location = new System.Drawing.Point(0, 0);
			this.treeView1.Name = "treeView1";
			this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
																				  new System.Windows.Forms.TreeNode("My shows", new System.Windows.Forms.TreeNode[] {
																																										new System.Windows.Forms.TreeNode("Browse by artists", new System.Windows.Forms.TreeNode[] {
																																																																	   new System.Windows.Forms.TreeNode("dummy")}),
																																										new System.Windows.Forms.TreeNode("Browse by city", new System.Windows.Forms.TreeNode[] {
																																																																	new System.Windows.Forms.TreeNode("dummy")}),
																																										new System.Windows.Forms.TreeNode("Browse by category", new System.Windows.Forms.TreeNode[] {
																																																																		new System.Windows.Forms.TreeNode("dummy")})})});
			this.treeView1.Size = new System.Drawing.Size(168, 160);
			this.treeView1.TabIndex = 0;
			this.treeView1.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterExpand);
			this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
			this.treeView1.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeExpand);
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// FilterTree
			// 
			this.Controls.Add(this.treeView1);
			this.Name = "FilterTree";
			this.Size = new System.Drawing.Size(168, 160);
			this.ResumeLayout(false);

		}
		#endregion

		private void treeView1_AfterExpand(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
		
		}

		private void treeView1_BeforeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			if ( e.Node.Nodes.Count == 1 )
			{
				if ( e.Node.Nodes[0].Text.Equals("dummy")) 
				{
					this.treeView1.BeginUpdate();
					e.Node.Nodes.Remove( e.Node.Nodes[0] );

					if ( e.Node.Text.Equals("Browse by artists")) 
					{
                        /* TODO QUANTUM
						foreach( Artist art in VaultAtlasApplication.Model.Artists ) 
						{
							TreeNode newNode = new TreeNode( art.DisplayName+" ("+art.GetShowCount()+")" );
							newNode.Tag = art;
							newNode.Nodes.Add( "dummy" );
							e.Node.Nodes.Add( newNode );
						}
                         */
					}

					if( e.Node.Tag is Artist ) 
					{
						Artist art = (Artist) e.Node.Tag;
						
					}

					this.treeView1.EndUpdate();
				}

			}
		}

		private void treeView1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
		}
	}
}
