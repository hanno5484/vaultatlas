using System.Linq;
using VaultAtlas.UI.AutoCompleter;
using System;
using VaultAtlas.DataModel;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace VaultAtlas.UI
{
	public class ShowEditData : System.Windows.Forms.UserControl, VaultAtlas.DataModel.IShowEditPlugIn
	{

        private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ComboBox comboBox2;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.TextBox textBox5;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.TextBox textBox6;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.ComboBox comboBox4;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox baseTextBox1;
        private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Splitter splitter2;
		
		private ShowResourceEditor showResourceEditor;
        private CheckBox checkBox2;
        private CheckBox checkBox3;
        private CheckBox checkBox4;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton toolStripButton2;
        private ToolStripButton toolStripButton3;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton toolStripButton4;
        private ToolStripButton toolStripButton5;
        private Panel panel2;
        private Panel panel4;
        private Panel panel5;
        private Splitter splitter1;
        private VaultAtlas.UI.Controls.AssociatedShows associatedShows1;
		private TextBox textBox7;
		private Label label13;
		private CheckBox checkBoxIsObsolete;
		private TextBox textBox8;
		private Label label14;

		private System.ComponentModel.Container components = null;

		public ShowEditData()
		{
			InitializeComponent();

		    this.comboBox4.Items.AddRange(Quality.QualityNames);

			this.showResourceEditor = new ShowResourceEditor();
			this.panel3.Controls.Add( this.showResourceEditor );
			this.showResourceEditor.Dock = DockStyle.Fill;
            this.Load += new EventHandler(ShowEditData_Load);
		}

        void ShowEditData_Load(object sender, EventArgs e)
        {
            this.show_ValueChanged(null, new ShowEventArgs(this.DataSourceIndex, this.EditedShow));
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowEditData));
			this.panel3 = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.checkBoxIsObsolete = new System.Windows.Forms.CheckBox();
			this.textBox7 = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.checkBox4 = new System.Windows.Forms.CheckBox();
			this.checkBox3 = new System.Windows.Forms.CheckBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.baseTextBox1 = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textBox6 = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.comboBox4 = new System.Windows.Forms.ComboBox();
			this.label10 = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.comboBox2 = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.button3 = new System.Windows.Forms.Button();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.button4 = new System.Windows.Forms.Button();
			this.splitter2 = new System.Windows.Forms.Splitter();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel4 = new System.Windows.Forms.Panel();
			this.panel5 = new System.Windows.Forms.Panel();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.associatedShows1 = new VaultAtlas.UI.Controls.AssociatedShows();
			this.textBox8 = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel5.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel3
			// 
			this.panel3.BackColor = System.Drawing.SystemColors.Window;
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(648, 186);
			this.panel3.TabIndex = 26;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.textBox8);
			this.panel1.Controls.Add(this.label14);
			this.panel1.Controls.Add(this.checkBoxIsObsolete);
			this.panel1.Controls.Add(this.textBox7);
			this.panel1.Controls.Add(this.label13);
			this.panel1.Controls.Add(this.checkBox4);
			this.panel1.Controls.Add(this.checkBox3);
			this.panel1.Controls.Add(this.checkBox2);
			this.panel1.Controls.Add(this.label12);
			this.panel1.Controls.Add(this.label8);
			this.panel1.Controls.Add(this.checkBox1);
			this.panel1.Controls.Add(this.baseTextBox1);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.textBox6);
			this.panel1.Controls.Add(this.label11);
			this.panel1.Controls.Add(this.comboBox4);
			this.panel1.Controls.Add(this.label10);
			this.panel1.Controls.Add(this.comboBox1);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.comboBox2);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.button3);
			this.panel1.Controls.Add(this.textBox5);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.label7);
			this.panel1.Controls.Add(this.textBox1);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.textBox2);
			this.panel1.Controls.Add(this.textBox3);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.textBox4);
			this.panel1.Controls.Add(this.label9);
			this.panel1.Controls.Add(this.button4);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 24);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(850, 191);
			this.panel1.TabIndex = 24;
			// 
			// checkBoxIsObsolete
			// 
			this.checkBoxIsObsolete.Location = new System.Drawing.Point(610, 82);
			this.checkBoxIsObsolete.Name = "checkBoxIsObsolete";
			this.checkBoxIsObsolete.Size = new System.Drawing.Size(128, 24);
			this.checkBoxIsObsolete.TabIndex = 32;
			this.checkBoxIsObsolete.Text = "Obsolete source";
			// 
			// textBox7
			// 
			this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox7.Location = new System.Drawing.Point(537, 83);
			this.textBox7.Name = "textBox7";
			this.textBox7.Size = new System.Drawing.Size(62, 21);
			this.textBox7.TabIndex = 30;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(481, 83);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(50, 16);
			this.label13.TabIndex = 31;
			this.label13.Text = "Etree:";
			// 
			// checkBox4
			// 
			this.checkBox4.Location = new System.Drawing.Point(610, 61);
			this.checkBox4.Name = "checkBox4";
			this.checkBox4.Size = new System.Drawing.Size(128, 24);
			this.checkBox4.TabIndex = 29;
			this.checkBox4.Text = "Video";
			// 
			// checkBox3
			// 
			this.checkBox3.Location = new System.Drawing.Point(610, 20);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(104, 24);
			this.checkBox3.TabIndex = 27;
			this.checkBox3.Text = "Master";
			// 
			// checkBox2
			// 
			this.checkBox2.Location = new System.Drawing.Point(610, 42);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(128, 24);
			this.checkBox2.TabIndex = 28;
			this.checkBox2.Text = "Need replacement";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(4, 131);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(50, 23);
			this.label12.TabIndex = 26;
			this.label12.Text = "Update:";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(312, 110);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(50, 16);
			this.label8.TabIndex = 25;
			this.label8.Text = "Notes:";
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(610, 1);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(104, 24);
			this.checkBox1.TabIndex = 24;
			this.checkBox1.Text = "Public";
			// 
			// baseTextBox1
			// 
			this.baseTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.baseTextBox1.Location = new System.Drawing.Point(368, 34);
			this.baseTextBox1.Name = "baseTextBox1";
			this.baseTextBox1.Size = new System.Drawing.Size(231, 21);
			this.baseTextBox1.TabIndex = 9;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(312, 34);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(50, 16);
			this.label5.TabIndex = 20;
			this.label5.Text = "From:";
			// 
			// textBox6
			// 
			this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox6.Location = new System.Drawing.Point(368, 83);
			this.textBox6.Name = "textBox6";
			this.textBox6.Size = new System.Drawing.Size(98, 21);
			this.textBox6.TabIndex = 10;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(312, 83);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(50, 16);
			this.label11.TabIndex = 22;
			this.label11.Text = "Loc:";
			// 
			// comboBox4
			// 
			this.comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox4.Location = new System.Drawing.Point(368, 7);
			this.comboBox4.Name = "comboBox4";
			this.comboBox4.Size = new System.Drawing.Size(62, 21);
			this.comboBox4.TabIndex = 7;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(312, 9);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(50, 23);
			this.label10.TabIndex = 19;
			this.label10.Text = "Quality:";
			// 
			// comboBox1
			// 
			this.comboBox1.Location = new System.Drawing.Point(66, 52);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(230, 21);
			this.comboBox1.Sorted = true;
			this.comboBox1.TabIndex = 3;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(6, 80);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(50, 23);
			this.label4.TabIndex = 17;
			this.label4.Text = "Venue:";
			// 
			// comboBox2
			// 
			this.comboBox2.Location = new System.Drawing.Point(66, 80);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new System.Drawing.Size(230, 21);
			this.comboBox2.Sorted = true;
			this.comboBox2.TabIndex = 4;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(6, 52);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(40, 23);
			this.label3.TabIndex = 16;
			this.label3.Text = "City:";
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(264, 7);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(32, 20);
			this.button3.TabIndex = 1;
			this.button3.Text = "...";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// textBox5
			// 
			this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox5.Location = new System.Drawing.Point(66, 106);
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(230, 21);
			this.textBox5.TabIndex = 5;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 23);
			this.label1.TabIndex = 14;
			this.label1.Text = "Artist:";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(4, 106);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(50, 23);
			this.label7.TabIndex = 18;
			this.label7.Text = "Source:";
			// 
			// textBox1
			// 
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox1.Location = new System.Drawing.Point(66, 5);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(182, 21);
			this.textBox1.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(6, 31);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(45, 23);
			this.label2.TabIndex = 15;
			this.label2.Text = "Date:";
			// 
			// textBox2
			// 
			this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox2.Location = new System.Drawing.Point(66, 29);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(86, 21);
			this.textBox2.TabIndex = 2;
			// 
			// textBox3
			// 
			this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox3.Location = new System.Drawing.Point(484, 6);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(64, 21);
			this.textBox3.TabIndex = 8;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(436, 10);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(50, 18);
			this.label6.TabIndex = 21;
			this.label6.Text = "Length:";
			// 
			// textBox4
			// 
			this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox4.Location = new System.Drawing.Point(368, 110);
			this.textBox4.Multiline = true;
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(231, 71);
			this.textBox4.TabIndex = 11;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(72, 131);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(80, 20);
			this.label9.TabIndex = 19;
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(168, 131);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(66, 23);
			this.button4.TabIndex = 6;
			this.button4.Text = "&Set";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// splitter2
			// 
			this.splitter2.Location = new System.Drawing.Point(0, 0);
			this.splitter2.Name = "splitter2";
			this.splitter2.Size = new System.Drawing.Size(3, 3);
			this.splitter2.TabIndex = 0;
			this.splitter2.TabStop = false;
			// 
			// toolStrip1
			// 
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripSeparator2,
            this.toolStripButton4,
            this.toolStripButton5});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(850, 25);
			this.toolStrip1.TabIndex = 18;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton1.Text = "Close";
			this.toolStripButton1.Click += new System.EventHandler(this.BtnSaveChanges_Clicked);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripButton2
			// 
			this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
			this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton2.Name = "toolStripButton2";
			this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton2.Text = "Undo";
			this.toolStripButton2.Click += new System.EventHandler(this.button6_Click_1);
			// 
			// toolStripButton3
			// 
			this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
			this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton3.Name = "toolStripButton3";
			this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton3.Text = "Delete";
			this.toolStripButton3.Click += new System.EventHandler(this.button5_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripButton4
			// 
			this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
			this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton4.Name = "toolStripButton4";
			this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton4.Text = "Previous";
			this.toolStripButton4.Click += new System.EventHandler(this.button8_Click);
			// 
			// toolStripButton5
			// 
			this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
			this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton5.Name = "toolStripButton5";
			this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton5.Text = "toolStripButton5";
			this.toolStripButton5.Click += new System.EventHandler(this.button7_Click);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.toolStrip1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(850, 24);
			this.panel2.TabIndex = 28;
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.panel3);
			this.panel4.Controls.Add(this.panel5);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel4.Location = new System.Drawing.Point(0, 215);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(850, 186);
			this.panel4.TabIndex = 29;
			// 
			// panel5
			// 
			this.panel5.BackColor = System.Drawing.SystemColors.MenuBar;
			this.panel5.Controls.Add(this.associatedShows1);
			this.panel5.Controls.Add(this.splitter1);
			this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
			this.panel5.Location = new System.Drawing.Point(648, 0);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(202, 186);
			this.panel5.TabIndex = 27;
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(0, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 186);
			this.splitter1.TabIndex = 0;
			this.splitter1.TabStop = false;
			// 
			// associatedShows1
			// 
			this.associatedShows1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.associatedShows1.Location = new System.Drawing.Point(3, 0);
			this.associatedShows1.Name = "associatedShows1";
			this.associatedShows1.Size = new System.Drawing.Size(199, 186);
			this.associatedShows1.TabIndex = 1;
			// 
			// textBox8
			// 
			this.textBox8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox8.Location = new System.Drawing.Point(368, 59);
			this.textBox8.Name = "textBox8";
			this.textBox8.Size = new System.Drawing.Size(231, 21);
			this.textBox8.TabIndex = 33;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(312, 59);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(50, 16);
			this.label14.TabIndex = 34;
			this.label14.Text = "Folder:";
			// 
			// ShowEditData
			// 
			this.Controls.Add(this.panel4);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.panel2);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "ShowEditData";
			this.Size = new System.Drawing.Size(850, 401);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel4.ResumeLayout(false);
			this.panel5.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region IShowEditPlugIn Member

		public VaultAtlas.DataModel.ITabPage[] CreateTabPages(ImageList defaultImageList)
		{
            this.Dock = DockStyle.Fill;
			return new ITabPage[]{ new GenericTabPage( "Edit", this, defaultImageList, 0 ) };
		}

		private IPlugInFunctionProvider provider;

		private void ReBind() 
		{
			this.checkBox1.Checked = this.EditedShow.IsPublic;
            this.checkBox2.Checked = this.EditedShow.NeedReplacement;
            this.checkBox3.Checked = this.EditedShow.IsMaster;
            this.checkBox4.Checked = this.EditedShow.IsVideo;
			this.checkBoxIsObsolete.Checked = this.EditedShow.IsObsolete;

            var artist = EditedShow.Artist;
			this.textBox1.Text = artist != null ? artist.DisplayName : string.Empty;
			this.textBox2.Text = this.EditedShow.Date.ToString();
			this.textBox3.Text = this.EditedShow.Length;
			this.textBox4.Text = this.EditedShow.Comments;
			this.textBox5.Text = this.EditedShow.Source;
			this.textBox6.Text = this.EditedShow.Loc;
			this.textBox7.Text = this.EditedShow.SHN;
			this.textBox8.Text = this.EditedShow.FolderName;
			this.baseTextBox1.Text = this.EditedShow.TradingSource;

			this.comboBox1.Text = this.EditedShow.City;
			this.comboBox2.Text = this.EditedShow.Venue;
			this.comboBox4.SelectedIndex = this.EditedShow.Quality;
			this.label9.Text = this.EditedShow.DateUpdated.ToShortDateString();

		}

        private bool eventsRegistered = false;

        public void OnBind(IPlugInFunctionProvider provider, int showIndex, Show show)
        {
            if (this.eventsRegistered)
            {

                this.checkBox1.Validating -= new CancelEventHandler(this.checkBox1_Validating);
                this.checkBox2.Validating -= new CancelEventHandler(this.checkBox2_Validating);
                this.checkBox3.Validating -= new CancelEventHandler(this.checkBox3_Validating);
                this.checkBox4.Validating -= new CancelEventHandler(this.checkBox4_Validating);
				this.checkBoxIsObsolete.Validating -= this.checkBoxIsObsolete_Validating;
                this.textBox1.Validating -= new CancelEventHandler(textBox1_Leave);
                this.textBox2.Validating -= new CancelEventHandler(textBox2_Validating);
                this.textBox3.Validating -= new CancelEventHandler(textBox3_Validating);
                this.textBox4.Validating -= new CancelEventHandler(textBox4_Validating);
                this.textBox6.Validating -= new CancelEventHandler(textBox6_Validating);
				this.textBox7.Validating -= new CancelEventHandler(textBox7_Validating);
				this.textBox8.Validating -= new CancelEventHandler(textBox8_Validating);
				this.baseTextBox1.Validating -= new CancelEventHandler(baseTextBox1_Validating);
                this.comboBox1.Validating -= new CancelEventHandler(comboBox1_Validating);
                this.comboBox2.Validating -= new CancelEventHandler(comboBox2_Validating);
                this.comboBox4.Validating -= new CancelEventHandler(comboBox4_Validating);
                this.textBox5.Validated -= new EventHandler(textBox5_Validated);
                this.EditedShow.ValueChanged -= new ShowEvent(this.show_ValueChanged);
            }

            this.DataSourceIndex = showIndex;
            this.EditedShow = show;

            this.EditedShow.ValueChanged += new ShowEvent(show_ValueChanged);
            this.show_ValueChanged(null, new ShowEventArgs(this.DataSourceIndex, this.EditedShow));
            this.provider = provider;

            this.showResourceEditor.EditedShow = this.EditedShow;

            this.ReBind();

            if (textCompleter == null)
                textCompleter = new TextBoxAutoCompleter(textBox1,
                    s =>
                    {
                        var artist = VaultAtlasApplication.Model.Artists.GetBestSuitedString(s);
                        return artist != null ? artist.DisplayName : s;
                    });

            if (this.comboBox1.Items.Count == 0)
            {
                this.comboBox1.BeginUpdate();
                this.comboBox1.Items.Clear();
                this.comboBox1.Items.AddRange(VaultAtlasApplication.Model.GetCities().ToArray());
                this.comboBox1.EndUpdate();
            }

            if( this.comboCompleter == null )
                this.comboCompleter = new ComboBoxAutoCompleter(this.comboBox2, this.comboBox1, VaultAtlasApplication.Model.GetBestForVenuePrefix);

            this.checkBox1.Validating += new CancelEventHandler(this.checkBox1_Validating);
            this.checkBox2.Validating += new CancelEventHandler(this.checkBox2_Validating);
            this.checkBox3.Validating += new CancelEventHandler(this.checkBox3_Validating);
            this.checkBox4.Validating += new CancelEventHandler(this.checkBox4_Validating);
			this.checkBoxIsObsolete.Validating += this.checkBoxIsObsolete_Validating;
            this.textBox1.Validating += new CancelEventHandler(textBox1_Leave);
            this.textBox2.Validating += new CancelEventHandler(textBox2_Validating);
            this.textBox3.Validating += new CancelEventHandler(textBox3_Validating);
            this.textBox4.Validating += new CancelEventHandler(textBox4_Validating);
            this.textBox6.Validating += new CancelEventHandler(textBox6_Validating);
			this.textBox7.Validating += new CancelEventHandler(textBox7_Validating);
			this.textBox8.Validating += new CancelEventHandler(textBox8_Validating);
			this.baseTextBox1.Validating += new CancelEventHandler(baseTextBox1_Validating);
            this.comboBox1.Validating += new CancelEventHandler(comboBox1_Validating);
            this.comboBox2.Validating += new CancelEventHandler(comboBox2_Validating);
            this.comboBox4.Validating += new CancelEventHandler(comboBox4_Validating);
            this.textBox5.Validated += new EventHandler(textBox5_Validated);

            this.eventsRegistered = true;
        }

        void show_ValueChanged(object sender, ShowEventArgs e)
        {
            TabPage p = this.Parent.Parent.Parent.Parent as TabPage; // TODO sehr unsauber! parent vom EditShow-Control
            if (p != null)
                p.Text = this.EditedShow.Display;
        }

        private TextBoxAutoCompleter textCompleter;
        private ComboBoxAutoCompleter comboCompleter;


	    public Show EditedShow { get; private set; }

	    public int DataSourceIndex { get; private set; }

	    void IShowEditPlugIn.OnShowPropertyChanged(string propertyName)
		{
		}

	    void IShowEditPlugIn.OnShowAcceptChanges()
		{
		}

	    void IShowEditPlugIn.OnFormClosing(CancelEventArgs args)
		{
		}

		public void OnShowRejectChanges()
		{
			this.ReBind();
			this.showResourceEditor.OnShowRejectChanges();
		}

		#endregion

		private void comboBox4_Validating(object sender, CancelEventArgs e)
		{
			this.EditedShow.Quality = (byte) this.comboBox4.SelectedIndex;
		}

		private void textBox6_Validating(object sender, CancelEventArgs e)
		{
			this.EditedShow.Loc = textBox6.Text;
		}

		private void baseTextBox1_Validating(object sender, CancelEventArgs e)
		{
			this.EditedShow.TradingSource = baseTextBox1.Text;
		}

		private void textBox7_Validating(object sender, CancelEventArgs e)
		{
			this.EditedShow.SHN = textBox7.Text;
		}

		private void textBox8_Validating(object sender, CancelEventArgs e)
		{
			this.EditedShow.FolderName = textBox8.Text;
		}

		private void textBox3_Validating(object sender, CancelEventArgs e)
		{
			this.EditedShow.Length = textBox3.Text;
			this.textBox3.Text = EditedShow.Length;
		}

		private void textBox5_Validated(object sender, EventArgs e)
		{
			this.EditedShow.Source = this.textBox5.Text;
		}

		private void textBox1_Leave(object sender, CancelEventArgs e)
		{
		    if (EditedShow.Row.RowState == DataRowState.Deleted)
		        return;

            if ( textBox1.Text.Equals( string.Empty ))
				return;

			// erzwingen, dass ein gueltiger Artist eingegeben wurde. sonst AddNew-Fenster öffnen.
		    var art = VaultAtlasApplication.Model.Artists.GetBestSuitedString(textBox1.Text);

			if (art != null) 
			{
				EditedShow.Artist = art;
			}
			else
			{
			    var newArtist = VaultAtlasApplication.RequestEnterArtist(textBox1.Text);
                if (newArtist != null)
			        EditedShow.Artist = newArtist;

			    if (!textBox1.Text.Equals(EditedShow.ArtistSortName))
			    {
			        if (EditedShow.Artist == null)
			            textBox1.Text = string.Empty;
			        else if (!textBox1.Text.Equals(EditedShow.Artist.DisplayName))
			            textBox1.Text = EditedShow.ArtistSortName;
			        e.Cancel = true;
			    }
			}
		}

		private void comboBox1_Validating(object sender, CancelEventArgs e)
		{
			this.EditedShow.City = this.comboBox1.Text;
		}

        private void comboBox2_Validating(object sender, CancelEventArgs e)
        {
            this.EditedShow.Venue = this.comboBox2.Text;
        }

		private void textBox2_Validating(object sender, CancelEventArgs e)
		{
			try 
			{
				var showDate = ShowDate.Parse( textBox2.Text );
				EditedShow.Date = showDate;
			} 
			catch
			{
			}
			this.textBox2.Text = this.EditedShow.Date.ToString();
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			if (this.EditedShow.Artist == null)
				return; // TODO
			var art = new DialogArtist( this.EditedShow.Artist );
			art.ShowDialog();
			this.textBox1.Text = this.EditedShow.ArtistSortName;
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			this.EditedShow.DateUpdated = DateTime.Today;
			this.label9.Text = this.EditedShow.DateUpdated.ToShortDateString();
		}

		private void button6_Click_1(object sender, System.EventArgs e)
		{
			this.provider.UndoChanges( this.EditedShow );
		}

		private void button5_Click(object sender, System.EventArgs e)
		{
			this.provider.DeleteShow( this.EditedShow );
		}

		private void textBox4_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			this.EditedShow.Comments = this.textBox4.Text;
		}

		private void BtnSaveChanges_Clicked(object sender, EventArgs e)
		{
		    SaveChanges();
		    provider.RequestClose();
		}

	    private void SaveChanges()
	    {
	        VaultAtlasApplication.Model.Shows.Update(EditedShow.Row);
	        showResourceEditor.Adapter.Update();
	    }

	    private void checkBox1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            EditedShow.IsPublic = checkBox1.Checked;
        }
    
        private void checkBox2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            EditedShow.NeedReplacement = checkBox2.Checked;
        }

        private void checkBox3_Validating(object sender, CancelEventArgs e)
        {
            EditedShow.IsMaster = checkBox3.Checked;
        }

        private void checkBox4_Validating(object sender, CancelEventArgs e)
        {
            EditedShow.IsVideo = checkBox4.Checked;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            BindNewShow(DataSourceIndex + 1, resources.NoNextShow);
        }

	    private void button8_Click(object sender, EventArgs e)
	    {
	        BindNewShow(DataSourceIndex - 1, resources.NoPrevShow);
	    }

	    private void BindNewShow(int newIndex, string errString)
	    {
            if (!ValidateChildren())
                return;
            
            if (newIndex < 0 || VaultAtlasApplication.Model.ShowView.Count <= newIndex)
	        {
	            MessageBox.Show(errString);
            }

            SaveChanges();
	        
            OnBind(provider, newIndex, VaultAtlasApplication.Model.ShowView.GetShow(newIndex));
	    }

	    private void checkBoxIsObsolete_Validating(object sender, EventArgs e)
		{
			EditedShow.IsObsolete = checkBoxIsObsolete.Checked;
		}
    }
}
