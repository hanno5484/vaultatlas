using System;
using System.IO;
using System.Windows.Forms;

namespace VaultAtlas.FlacAtlas
{
    public partial class FileContentDialog : UserControl
    {
        public FileContentDialog()
        {
            InitializeComponent();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.textBox1.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            if (sfd.ShowDialog() != DialogResult.OK)
                return;
            using (var fs = new FileStream(sfd.FileName, FileMode.OpenOrCreate, FileAccess.Write))
                fs.Write(this.content, 0, this.content.Length);
        }

        private byte[] content;
        public byte[] Content
        {
            get
            {
                return content;
            }
            set
            {
                var hasContent = (content = value) != null && value.Length > 0;

                if (hasContent)
                {
                    textBox1.Text = System.Text.UTF8Encoding.UTF8.GetString(this.content);
                }


                textBox1.Visible = button3.Visible = button4.Visible = hasContent;
            }
        }
    }
}
