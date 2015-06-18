using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace VaultAtlas.DataModel.ModelUI
{
	public class SearchDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.CheckBox checkBox2;

		private System.ComponentModel.Container components = null;

		public SearchDialog()
		{
			InitializeComponent();
		}
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchDialog));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.AccessibleDescription = null;
            this.textBox1.AccessibleName = null;
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.BackgroundImage = null;
            this.textBox1.Font = null;
            this.textBox1.Name = "textBox1";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = null;
            this.label1.AccessibleName = null;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Font = null;
            this.label1.Name = "label1";
            // 
            // checkBox1
            // 
            this.checkBox1.AccessibleDescription = null;
            this.checkBox1.AccessibleName = null;
            resources.ApplyResources(this.checkBox1, "checkBox1");
            this.checkBox1.BackgroundImage = null;
            this.checkBox1.Font = null;
            this.checkBox1.Name = "checkBox1";
            // 
            // button1
            // 
            this.button1.AccessibleDescription = null;
            this.button1.AccessibleName = null;
            resources.ApplyResources(this.button1, "button1");
            this.button1.BackgroundImage = null;
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Font = null;
            this.button1.Name = "button1";
            // 
            // button2
            // 
            this.button2.AccessibleDescription = null;
            this.button2.AccessibleName = null;
            resources.ApplyResources(this.button2, "button2");
            this.button2.BackgroundImage = null;
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Font = null;
            this.button2.Name = "button2";
            // 
            // button3
            // 
            this.button3.AccessibleDescription = null;
            this.button3.AccessibleName = null;
            resources.ApplyResources(this.button3, "button3");
            this.button3.BackgroundImage = null;
            this.button3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button3.Font = null;
            this.button3.Name = "button3";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // checkBox2
            // 
            this.checkBox2.AccessibleDescription = null;
            this.checkBox2.AccessibleName = null;
            resources.ApplyResources(this.checkBox2, "checkBox2");
            this.checkBox2.BackgroundImage = null;
            this.checkBox2.Font = null;
            this.checkBox2.Name = "checkBox2";
            // 
            // SearchDialog
            // 
            this.AcceptButton = this.button1;
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.BackgroundImage = null;
            this.CancelButton = this.button2;
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = null;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchDialog";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.SearchDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        public event EventHandler SearchAllRequested;

		private void button3_Click(object sender, System.EventArgs e)
		{
            if (this.SearchAllRequested != null)
                this.SearchAllRequested(this, EventArgs.Empty);
		}

		public string SearchString 
		{
			get 
			{
				return this.textBox1.Text;
			}
		}

		public bool IgnoreCase 
		{
			get 
			{
				return (!this.checkBox1.Checked);
			}
		}

		public bool SearchInResources 
		{
			get 
			{
				return this.checkBox2.Checked;
			}
		}

        private void SearchDialog_Load(object sender, EventArgs e)
        {

        }
	}
}
