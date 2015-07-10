using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VaultAtlas.DataModel;

namespace VaultAtlas.FlacAtlas
{
    public partial class MatchingResultViewer : UserControl
    {
        public MatchingResultViewer()
        {
            InitializeComponent();
        }

        public void AddItems(IDictionary<string, Show> items)
        {
            try
            {
                listView1.BeginUpdate();

                foreach (var item in items)
                {
                    var lvi = new ListViewItem();
                    lvi.Text = item.Key;
                    lvi.SubItems.Add(item.Value.Display);
                    lvi.Checked = true;
                    listView1.Items.Add(lvi);
                }
            }
            finally
            {
                listView1.EndUpdate();
            }
        }
    }
}
