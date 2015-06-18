using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace VaultAtlas.UI.SearchReplace
{
	/// <summary>
	/// Summary description for SearchReplaceDialog.
	/// </summary>
	public class ReplaceDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Button button2;

		private System.ComponentModel.Container components = null;

		public ReplaceDialog()
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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.button1 = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.button2 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Location = new System.Drawing.Point(84, 16);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(118, 20);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(10, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(61, 16);
			this.label1.TabIndex = 4;
			this.label1.Text = "Search for:";
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(84, 76);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(110, 14);
			this.checkBox1.TabIndex = 2;
			this.checkBox1.Text = "case sensitive";
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button1.Location = new System.Drawing.Point(222, 16);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(100, 22);
			this.button1.TabIndex = 3;
			this.button1.Text = "&Replace";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(10, 46);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(66, 16);
			this.label2.TabIndex = 5;
			this.label2.Text = "Replace by:";
			// 
			// textBox2
			// 
			this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox2.Location = new System.Drawing.Point(84, 44);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(118, 20);
			this.textBox2.TabIndex = 1;
			this.textBox2.Text = "";
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.Location = new System.Drawing.Point(222, 60);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(100, 22);
			this.button2.TabIndex = 6;
			this.button2.Text = "&Close";
			// 
			// ReplaceDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.button2;
			this.ClientSize = new System.Drawing.Size(334, 125);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ReplaceDialog";
			this.ShowInTaskbar = false;
			this.Text = "Search and Replace";
			this.ResumeLayout(false);

		}
		#endregion

		public string SearchString 
		{
			get 
			{
				return this.textBox1.Text;
			}
		}

		public string ReplaceString 
		{
			get 
			{
				return this.textBox2.Text;
			}
		}

		public bool IgnoreCase 
		{
			get 
			{
				return (!this.checkBox1.Checked);
			}
		}
	}
}
