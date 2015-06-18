using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Grape.UI
{
    public partial class ListControlTester : Form
    {
        public ListControlTester()
        {
            InitializeComponent();
        }

        [STAThread]
        static void Main()
        {
            try
            {
                Application.DoEvents();
                ListControlTester mf = new ListControlTester();
                Application.Run(mf);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message + " " + exc.StackTrace);
                MessageBox.Show(exc.Message + " " + exc.StackTrace);
            }
        }

    }
}