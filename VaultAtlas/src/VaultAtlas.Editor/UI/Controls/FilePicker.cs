using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace VaultAtlas.UI.Controls
{
    public partial class FilePicker : UserControl
    {
        public FilePicker()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = this.textBox1.Text;
            if (ofd.ShowDialog() == DialogResult.OK)
                this.textBox1.Text = ofd.FileName;
        }

        public string FileName
        {
            get
            {
                return this.textBox1.Text;
            }
            set
            {
                this.textBox1.Text = value;
            }
        }
    }
}
