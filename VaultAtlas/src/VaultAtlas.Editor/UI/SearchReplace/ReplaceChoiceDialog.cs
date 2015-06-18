using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace VaultAtlas.UI.SearchReplace
{
	public class ReplaceChoiceDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		private System.ComponentModel.Container components = null;

		public ReplaceChoiceDialog()
		{
			InitializeComponent();
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.Ignore;
			this.button1.Location = new System.Drawing.Point(92, 12);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(80, 22);
			this.button1.TabIndex = 4;
			this.button1.Text = "&Skip";
			// 
			// button2
			// 
			this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button2.Location = new System.Drawing.Point(8, 12);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(80, 22);
			this.button2.TabIndex = 3;
			this.button2.Text = "&Replace";
			// 
			// button3
			// 
			this.button3.DialogResult = System.Windows.Forms.DialogResult.Yes;
			this.button3.Location = new System.Drawing.Point(260, 12);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(80, 22);
			this.button3.TabIndex = 6;
			this.button3.Text = "Replace &all";
			// 
			// button4
			// 
			this.button4.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button4.Location = new System.Drawing.Point(176, 12);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(80, 22);
			this.button4.TabIndex = 5;
			this.button4.Text = "&Cancel";
			// 
			// ReplaceChoiceDialog
			// 
			this.AcceptButton = this.button2;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(352, 47);
			this.ControlBox = false;
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "ReplaceChoiceDialog";
			this.ShowInTaskbar = false;
			this.Text = "Replace";
			this.ResumeLayout(false);

		}
		#endregion

	}
}
