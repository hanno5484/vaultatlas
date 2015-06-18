using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace VaultAtlas.FlacAtlas
{
    public partial class VolumeImporterDialog : FlacAtlasBaseForm
    {
        private DataManager _dataManager;
        public DataManager DataManager
        {
            get
            {
                return _dataManager;
            }
            set
            {
                if (_dataManager == null)
                    _dataManager = value;

                if (_dataManager == null)
                    return;

                var lastImportedDrive = _dataManager["LastImportedDrive", null];
                if (lastImportedDrive == null)
                    return;

                for (var i = 0; i < this.comboBox1.Items.Count; i++)
                    if (((DriveInformation)this.comboBox1.Items[i]).LogDrive == lastImportedDrive)
                        comboBox1.SelectedIndex = i;
                    else
                        comboBox1_SelectedIndexChanged(null, EventArgs.Empty);
            }
        }

        public VolumeImporterDialog()
        {
            InitializeComponent();

            var driveInfo = DriveInformation.GetDriveInformation().ToList();

            this.comboBox1.DisplayMember = "DisplayName";
            this.comboBox1.DataSource = driveInfo;

            this.comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
        }

        void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            /* TODO QUANTUM
            if (comboBox1.SelectedIndex != -1)
                _dataManager["LastImportedDrive"] = ((DriveInformation)this.comboBox1.SelectedItem).LogDrive;
            */
        }

        public int DriveCount
        {
            get
            {
                return this.comboBox1.Items.Count;
            }
        }

        public DriveInformation SelectedDrive
        {
            get
            {
                return this.comboBox1.SelectedItem as DriveInformation;
            }
        }

        public string NewDiscNumber
        {
            get
            {
                return this.textBox1.Text;
            }
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            var discs = _dataManager.Discs.Table.Select("DiscNumber = '" + textBox1.Text + "'");
            if (discs.Length > 0)
            {
                MessageBox.Show(resources.DiscNumberAlreadyAssigned);
                e.Cancel = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = textBox1.Text.Length > 0;
        }


    }
}