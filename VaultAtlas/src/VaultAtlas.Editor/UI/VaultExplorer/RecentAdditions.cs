using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace VaultAtlas.UI.VaultExplorer
{
	public class RecentAdditions : System.Windows.Forms.UserControl
	{

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ComboBox comboBox2;
		private System.ComponentModel.Container components = null;
		private ArrayList ListTypes = new ArrayList();
		private System.Windows.Forms.DateTimePicker dateTimePicker1;
		private ArrayList TimeSpanTypes = new ArrayList();

		public RecentAdditions()
		{
			InitializeComponent();

			this.TimeSpanTypes.Add(new DisplayValuePair("Entries updated today", "DateUpdated >= '"+DateTime.Today.ToString("yyyy-MM-dd")+" 00:00:00.000'"));
			this.TimeSpanTypes.Add(new DisplayValuePair("Entries updated last week", "DateUpdated >= '"+DateTime.Today.AddDays(-7).ToString("yyyy-MM-dd")+" 00:00:00.000'"));
			this.TimeSpanTypes.Add(new DisplayValuePair("Entries updated last month", "DateUpdated >= '"+DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd")+" 00:00:00.000'"));
			this.TimeSpanTypes.Add(new DisplayValuePair("Entries updated after", "DateUpdated >= '{0} 00:00:00.000'"));
            this.TimeSpanTypes.Add(new DisplayValuePair("Entries updated before", "DateUpdated <= '{0} 00:00:00.000'"));
			this.TimeSpanTypes.Add(new DisplayValuePair("All entries", ""));
			this.comboBox2.DataSource = this.TimeSpanTypes;
			this.comboBox2.SelectedIndex = 5;
			this.comboBox2.DisplayMember = "Display";
			this.comboBox2.ValueMember = "Value";

			this.dateTimePicker1.Value = DateTime.Now.AddMonths(-2);

			this.comboBox2.SelectedValueChanged +=new EventHandler(comboBox2_SelectedValueChanged);

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
            this.panel1 = new System.Windows.Forms.Panel();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.comboBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(252, 74);
            this.panel1.TabIndex = 1;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "d";
            this.dateTimePicker1.Location = new System.Drawing.Point(1, 43);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(205, 20);
            this.dateTimePicker1.TabIndex = 3;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.Location = new System.Drawing.Point(1, 11);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(252, 21);
            this.comboBox2.TabIndex = 2;
            // 
            // RecentAdditions
            // 
            this.Controls.Add(this.panel1);
            this.Name = "RecentAdditions";
            this.Size = new System.Drawing.Size(252, 74);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
		{
			this.UpdateFilter();
		}

		private void UpdateFilter()
		{
			string s = comboBox2.SelectedValue as string;
			if (s != null) 
			{
				if (s.IndexOf("{0}") != -1) 
				{
					this.dateTimePicker1.Enabled = true;
					s = String.Format( s, this.dateTimePicker1.Value.ToString("yyyy-MM-dd") );
				} 
				else
					this.dateTimePicker1.Enabled = false;
				VaultAtlasApplication.Model.ShowView.Filter.WhereClause = s;
			}
		}

		private void dateTimePicker1_ValueChanged(object sender, System.EventArgs e)
		{
			this.UpdateFilter();
		}
	}

	class DisplayValuePair 
	{
		private string display, val;

		public DisplayValuePair(string Display, string Value) 
		{
			this.display = Display;
			this.val = Value;

		}

		public string Display 
		{
			get 
			{
				return this.display;
			}
		}

		public string Value
		{
			get 
			{
				return this.val;
			}
		}
	}
}
