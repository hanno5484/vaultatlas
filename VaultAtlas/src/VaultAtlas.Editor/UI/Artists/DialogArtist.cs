using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Grape.DataModel.Artists;

namespace Grape.UI.Artists
{
	/// <summary>
	/// Summary description for DialogArtist.
	/// </summary>
	public class DialogArtist : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DialogArtist()
		{
			InitializeComponent();
		}

		public DialogArtist(Artist artist)
		{
			InitializeComponent();
			this.artist = artist;
			this.checkBox1.DataBindings.Add("Checked", artist, "IsPublicByDefault");
			this.textBox1.DataBindings.Add("Text", artist, "DisplayName");
			this.textBox2.DataBindings.Add("Text", artist, "SortName");
			this.textBox3.DataBindings.Add("Text", artist, "Abbreviation");
			this.textBox4.DataBindings.Add("Text", artist, "ETreeArtistID");

			this.button3.Visible = !this.artist.IsEntered;
		}

		private Artist artist;
		public Artist Artist 
		{
			get 
			{
				return this.artist;
			}
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
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(138, 14);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(178, 20);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "";
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(138, 42);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(178, 20);
			this.textBox2.TabIndex = 1;
			this.textBox2.Text = "";
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(138, 70);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(178, 20);
			this.textBox3.TabIndex = 2;
			this.textBox3.Text = "";
			// 
			// textBox4
			// 
			this.textBox4.Location = new System.Drawing.Point(138, 98);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(80, 20);
			this.textBox4.TabIndex = 3;
			this.textBox4.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(4, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(128, 20);
			this.label1.TabIndex = 10;
			this.label1.Text = "Name:";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(4, 44);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(128, 20);
			this.label3.TabIndex = 7;
			this.label3.Text = "Sorted name:";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(4, 98);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(128, 20);
			this.label4.TabIndex = 8;
			this.label4.Text = "ETree Artist ID:";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(4, 71);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(128, 20);
			this.label5.TabIndex = 9;
			this.label5.Text = "Abbreviation:";
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(6, 128);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(286, 20);
			this.checkBox1.TabIndex = 5;
			this.checkBox1.Text = "Shows by this artist are &publicly visible by default";
			// 
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button1.Location = new System.Drawing.Point(218, 156);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(98, 24);
			this.button1.TabIndex = 6;
			this.button1.Text = "&Close";
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(229, 98);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(86, 22);
			this.button2.TabIndex = 4;
			this.button2.Text = "&Browse ...";
			// 
			// button3
			// 
			this.button3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button3.Location = new System.Drawing.Point(110, 156);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(98, 24);
			this.button3.TabIndex = 11;
			this.button3.Text = "&Cancel";
			// 
			// DialogArtist
			// 
			this.AcceptButton = this.button1;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.button3;
			this.ClientSize = new System.Drawing.Size(336, 191);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox4);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "DialogArtist";
			this.Text = "DialogArtist";
			this.ResumeLayout(false);

		}
		#endregion
	}
}
