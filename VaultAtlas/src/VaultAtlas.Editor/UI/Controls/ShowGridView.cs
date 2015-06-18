using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace VaultAtlas.UI.Controls
{
    public partial class ShowGridView : DataGridView, VaultAtlas.DataModel.IModelNavigator
    {
        public ShowGridView()
        {
            InitializeComponent();

            this.ColumnHeadersHeight = 18;
        }


        protected override void OnDataSourceChanged(EventArgs e)
        {
            base.OnDataSourceChanged(e);
            if (this.DataSource != null && !this.DesignMode)
            {
                this.AutoGenerateColumns = false;
                this.Columns.Clear();
                
                foreach (string columnName in columnNames)
                {
                    DataGridViewCell cellTemplate = null;
                    if (columnName.Equals("Artist"))
                        cellTemplate = new ArtistDisplayNameCell();
                    else if ( columnName.Equals("Date"))
                        cellTemplate = new ShowDateCell();
                    else if (columnName.Equals("Status"))
                        cellTemplate = new StatusCell();
                    else
                        cellTemplate = new DataGridViewTextBoxCell();

                    if (columnName.Equals("DateUpdated"))
                        cellTemplate.ValueType = typeof(DateTime);

                    cellTemplate.Style.Padding = new Padding(2, 2, 2, 0);
//                    cellTemplate.Style.Font = new Font("Tahoma", 8);
                    DataGridViewColumn gridViewColumn = null;
                    gridViewColumn = new DataGridViewColumn(cellTemplate);
                    gridViewColumn.DataPropertyName = columnName;
                    gridViewColumn.SortMode = DataGridViewColumnSortMode.Programmatic;
                    gridViewColumn.HeaderText = columnName;
                    this.Columns.Add(gridViewColumn);
                }
            }

        }

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);
            Point clientPoint = this.PointToClient(MousePosition);
            int selIndex = this.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            if (selIndex != -1 && this.ItemClicked != null)
            {
                DataRow row = ((VaultAtlas.DataModel.CustomDataView)this.DataSource)[selIndex].Row;
                this.ItemClicked(this, new VaultAtlas.DataModel.ShowEventArgs(selIndex, row));
            }

        }

        public event VaultAtlas.DataModel.ShowEvent ItemClicked;

        private static string[] columnNames = new string[] { "Status", "Artist", "Date", "City", "Venue", "Quality", "Length", "Source", "DateUpdated" };

        private int FindRow(DataRow row)
        {
            VaultAtlas.DataModel.CustomDataView view = (VaultAtlas.DataModel.CustomDataView)this.DataSource;
            for (int i = 0; i < view.Count; ++i)
                if (view[i].Row == row)
                    return i;
            return -1;
        }

        public void NavigateTo(int showIndex)
        {
            if (showIndex == -1)
            {
                // TODO Flag einbauen ob diese Meldung angezeigt werden soll
                // MessageBox.Show("Changes are not seen. Blabla.", "VaultAtlas", MessageBoxButtons.OK, MessageBoxIcon.Information );
            }
            else
            {
                if (this.RowCount > showIndex)
                    this.CurrentCell = this[0, showIndex];
                // TODO this.CurrentRow = ind;
            }
        }

    }
}
