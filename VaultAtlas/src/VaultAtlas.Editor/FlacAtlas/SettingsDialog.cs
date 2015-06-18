using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VaultAtlas.FlacAtlas
{
    public partial class SettingsDialog : FlacAtlasBaseForm
    {
        public SettingsDialog()
        {
            InitializeComponent();

            this.textBox1.Text = man["EmbedFileExtensions", "jpg gif md5 txt doc rtf ffp"];
            this.textBox2.Text = man["EmbedFilesSmallerThan", "8192"];

        }

        private DataManager man = DataManager.Get();

        private void button1_Click(object sender, EventArgs e)
        {
            /* TODO QUANTUM
            man["EmbedFileExtensions"] = this.textBox1.Text;
            man["EmbedFilesSmallerThan"] = this.textBox2.Text;
             */
        }

        private void button1_Validating(object sender, CancelEventArgs e)
        {
            int i;
            if ( !Int32.TryParse( this.textBox2.Text, out i))
                e.Cancel = true;
        }




        
    }
}