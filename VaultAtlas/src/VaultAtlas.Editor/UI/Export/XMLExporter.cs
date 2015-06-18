using System;
using System.Threading;
using System.Reflection;
using VaultAtlas.DataModel;
using VaultAtlas.DataModel.Export;
using System.Drawing;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Xml.Xsl;
using System.Xml;

namespace VaultAtlas.UI.Export
{
	/// <summary>
	/// Summary description for XMLExporter.
	/// </summary>
	public class XMLExporter : System.Windows.Forms.Form
	{

		private string BrowserTempFile = System.IO.Path.GetTempFileName();
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.RadioButton radioButton2;

		private Exporter exporter;

		private System.ComponentModel.Container components = null;

		private System.Windows.Forms.CheckBox checkBox1;

        private string generatedDocument;
        private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox checkBox3;
		private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
        private VaultAtlas.UI.Controls.FilePicker filePicker2;
        private VaultAtlas.UI.Controls.FilePicker filePicker1;
		
		private ExportSettings settings = new ExportSettings();
		public ExportSettings Settings
		{
			get 
			{
				return this.settings;
			}
		}

		public XMLExporter()
		{
			InitializeComponent();

			this.exporter = new Exporter( this.settings );

			this.checkBox1.Checked = this.settings.UseArtistFriendlyNames;
			this.checkBox1.CheckedChanged +=new EventHandler(checkBox1_CheckedChanged);
			this.Load +=new EventHandler(XMLExporter_Load);
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
			if (File.Exists(this.BrowserTempFile))
				File.Delete(this.BrowserTempFile);
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XMLExporter));
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.filePicker1 = new VaultAtlas.UI.Controls.FilePicker();
            this.filePicker2 = new VaultAtlas.UI.Controls.FilePicker();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            resources.ApplyResources(this.checkBox1, "checkBox1");
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Name = "checkBox1";
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.filePicker2);
            this.groupBox3.Controls.Add(this.filePicker1);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.radioButton2);
            this.groupBox3.Controls.Add(this.radioButton1);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // radioButton2
            // 
            resources.ApplyResources(this.radioButton2, "radioButton2");
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.Checked = true;
            resources.ApplyResources(this.radioButton1, "radioButton1");
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.TabStop = true;
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.checkBox3);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // checkBox3
            // 
            resources.ApplyResources(this.checkBox3, "checkBox3");
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Name = "checkBox3";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button4
            // 
            this.button4.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.button4, "button4");
            this.button4.Name = "button4";
            // 
            // filePicker1
            // 
            resources.ApplyResources(this.filePicker1, "filePicker1");
            this.filePicker1.Name = "filePicker1";
            // 
            // filePicker2
            // 
            resources.ApplyResources(this.filePicker2, "filePicker2");
            this.filePicker2.Name = "filePicker2";
            // 
            // XMLExporter
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Name = "XMLExporter";
            this.ShowInTaskbar = false;
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void radioButton2_CheckedChanged(object sender, System.EventArgs e)
		{
			this.settings.UseXSLT = 
				this.filePicker1.Enabled = 
				this.filePicker2.Enabled =
				!this.radioButton2.Checked;
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
            this.LoadFiles();

			SaveFileDialog sfd = new SaveFileDialog();
			if (sfd.ShowDialog() != DialogResult.OK)
				return;
	        
			this.SaveDocument( sfd.FileName );
		}

        private void ExportFinished()
        {
            MessageBox.Show("Saved successfully.");
        }

        private void SaveDocument( string fileName ) 
		{
            XslThread xslThread = new XslThread(this, fileName, new Action(this.ExportFinished));

            VaultAtlas.DataModel.ModelUI.ProgressWindow p = new VaultAtlas.DataModel.ModelUI.ProgressWindow();
            this.AddOwnedForm(p);
            p.StartPosition = FormStartPosition.CenterParent;
            p.ProgressVisible = false;
            p.Show();
            p.Begin();
            p.SetText("Writing " + fileName + " ...");
            System.Threading.ThreadPool.QueueUserWorkItem(
                          new System.Threading.WaitCallback(xslThread.Do),p);
        }

        internal class XslThread
        {
            private string fileName;
            private XMLExporter export;
            internal XslThread(XMLExporter export, string fileName, Delegate finishedDelegate)
            {
                this.fileName = fileName;
                this.export = export;
                this.finishedDelegate = finishedDelegate;
            }

            private Delegate finishedDelegate;

            internal void Do( object callback)
            {
                VaultAtlas.DataModel.ModelUI.IProgressCallback pw = callback as VaultAtlas.DataModel.ModelUI.IProgressCallback;

                try
                {
                    this.export.GenerateDocument();

                    using (StreamWriter sw = new StreamWriter(fileName, false, System.Text.Encoding.UTF8))
                    {
                        sw.WriteLine(this.export.generatedDocument);
                        sw.Close();
                    }

                    if (this.finishedDelegate != null)
                        this.finishedDelegate.DynamicInvoke(new object[0]);

                }
                finally
                {
                    pw.End();
                }

            }
        }

		private void GenerateDocument() 
		{
            CustomDataView exporterView = VaultAtlasApplication.Model.ShowView.CreateChildView();
            settings.IncludeNonPublic = !this.checkBox3.Checked;
            if( !settings.IncludeNonPublic )
                exporterView.Filter.CustomFilterExpression = "IsPublic = true";

			this.generatedDocument = exporter.GenerateDocument( exporterView, VaultAtlasApplication.Model.Artists );
			StreamWriter sw = new StreamWriter( this.BrowserTempFile );
			sw.Write( this.generatedDocument );
			sw.Close();
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			this.settings.UseArtistFriendlyNames = this.checkBox1.Checked;
		}

        private void XMLExporter_Load(object sender, EventArgs e)
        {
            this.filePicker1.FileName = VaultAtlasApplication.Model["XsltFile"];
            this.filePicker2.FileName = VaultAtlasApplication.Model["CssFile"];
        }

        private void LoadFiles()
        {
            VaultAtlasApplication.Model["XsltFile"] = this.filePicker1.FileName;
            VaultAtlasApplication.Model["CssFile"] = this.filePicker2.FileName;
            this.settings.LoadTransform(this.filePicker1.FileName);
            this.settings.LoadStylesheet(this.filePicker2.FileName);
        }

        private void button2_Click(object sender, System.EventArgs e)
		{
			string tempFileName = Path.GetTempFileName()+".html";

            this.LoadFiles();

			this.SaveDocument( tempFileName );
			System.Diagnostics.Process.Start( tempFileName );
		}
	}
}
