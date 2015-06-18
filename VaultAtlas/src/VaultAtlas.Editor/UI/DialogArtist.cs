using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using VaultAtlas.DataModel;

namespace VaultAtlas.UI
{
	public class DialogArtist : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBox1;
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
			this.textBox1.DataBindings.Add("Text", artist, "DisplayName");
			this.textBox2.DataBindings.Add("Text", artist, "SortName");
			this.textBox3.DataBindings.Add("Text", artist, "Abbreviation");
			this.textBox4.DataBindings.Add("Text", artist, "ETreeArtistID");

			// fill folder combobox
			this.comboBox1.BeginUpdate();
			this.comboBox1.Items.Add( "no category selected" );
			this.comboBox1.Items.AddRange( VaultAtlasApplication.Model.Categories );
			this.comboBox1.EndUpdate();
			string folder = artist.Folder;
			if ( folder != null )
				this.comboBox1.SelectedItem = folder;
			else
				this.comboBox1.SelectedIndex = 0;

			this.comboBox1.SelectedIndexChanged +=new EventHandler(comboBox1_SelectedIndexChanged);
			this.textBox1.Leave +=new EventHandler(textBox1_Leave);

			// TODO QUANTUM this.button3.Visible = !this.artist.IsEntered;
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
			this.label5 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(146, 12);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(218, 20);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "";
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(146, 42);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(218, 20);
			this.textBox2.TabIndex = 1;
			this.textBox2.Text = "";
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(146, 72);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(218, 20);
			this.textBox3.TabIndex = 2;
			this.textBox3.Text = "";
			// 
			// textBox4
			// 
			this.textBox4.Location = new System.Drawing.Point(146, 100);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(80, 20);
			this.textBox4.TabIndex = 3;
			this.textBox4.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(10, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(128, 20);
			this.label1.TabIndex = 10;
			this.label1.Text = "Name:";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(10, 48);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(128, 20);
			this.label3.TabIndex = 7;
			this.label3.Text = "Sorted name:";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(10, 74);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(128, 20);
			this.label5.TabIndex = 9;
			this.label5.Text = "Abbreviation:";
			// 
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button1.Location = new System.Drawing.Point(204, 190);
			this.button1.Name = "button1";
			this.button1.TabIndex = 6;
			this.button1.Text = "&Save";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button3
			// 
			this.button3.CausesValidation = false;
			this.button3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button3.Location = new System.Drawing.Point(298, 190);
			this.button3.Name = "button3";
			this.button3.TabIndex = 11;
			this.button3.Text = "&Cancel";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.comboBox1);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.textBox2);
			this.groupBox1.Controls.Add(this.textBox1);
			this.groupBox1.Controls.Add(this.textBox3);
			this.groupBox1.Controls.Add(this.textBox4);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Location = new System.Drawing.Point(10, 10);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(378, 168);
			this.groupBox1.TabIndex = 12;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Artist data";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(10, 102);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(128, 20);
			this.label4.TabIndex = 8;
			this.label4.Text = "ETree Artist ID:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(10, 132);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(128, 20);
			this.label2.TabIndex = 11;
			this.label2.Text = "Category:";
			// 
			// comboBox1
			// 
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.Location = new System.Drawing.Point(146, 130);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(218, 21);
			this.comboBox1.TabIndex = 12;
			// 
			// DialogArtist
			// 
			this.AcceptButton = this.button1;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.button3;
			this.ClientSize = new System.Drawing.Size(414, 243);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DialogArtist";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Edit Artist Data";
			this.Load += new System.EventHandler(this.DialogArtist_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
		
		}

		private void DialogArtist_Load(object sender, System.EventArgs e)
		{
			this.textBox1.Focus();
		}

		private void textBox1_Leave(object sender, EventArgs e)
		{
			if (this.textBox2.Text.Equals(""))
				this.textBox2.Text = this.textBox1.Text;
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if ( this.comboBox1.SelectedIndex == 0 )
				this.artist.Folder = string.Empty;
			else
				this.artist.Folder = this.comboBox1.SelectedItem.ToString();
		}
	}
}
