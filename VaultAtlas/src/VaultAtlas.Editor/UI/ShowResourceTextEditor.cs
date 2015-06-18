using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using VaultAtlas.DataModel;

namespace VaultAtlas.UI
{
    public partial class ShowResourceTextEditor : UserControl
    {
        public ShowResourceTextEditor()
        {
            InitializeComponent();
        }

        public Resource Resource { get; set; }

        public string Value
        {
            get
            {
                return this.textBox1.Text;
            }
            set
            {
                this.textBox1.Text = value;
                int f = value.IndexOf('\n');
                if (f > 0)
                {
                    if (value[f - 1] != '\r')
                    {
                        var sb = new StringBuilder(value);
                        sb.Replace("\n", Environment.NewLine);
                        this.textBox1.Text = sb.ToString();
                    }
                }
            }
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
            if (IsDisposed)
                return;

            Resource.Value = textBox1.Text;
        }
    }
}
