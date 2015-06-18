using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace VaultAtlas.UI.VaultExplorer
{
	public class Sorting : System.Windows.Forms.UserControl
	{
		private VaultAtlas.UI.VaultExplorer.SortControl sortControl1;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Sorting()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

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
			this.sortControl1 = new VaultAtlas.UI.VaultExplorer.SortControl();
			this.SuspendLayout();
			// 
			// sortControl1
			// 
			this.sortControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.sortControl1.Location = new System.Drawing.Point(0, 0);
			this.sortControl1.Name = "sortControl1";
			this.sortControl1.Size = new System.Drawing.Size(264, 56);
			this.sortControl1.TabIndex = 0;
			// 
			// Sorting
			// 
			this.Controls.Add(this.sortControl1);
			this.Name = "Sorting";
			this.Size = new System.Drawing.Size(264, 56);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
