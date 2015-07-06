using System;
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
                
                foreach (var columnName in ColumnNames)
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

                    var gridViewColumn = new DataGridViewColumn(cellTemplate)
                    {
                        DataPropertyName = columnName,
                        SortMode = DataGridViewColumnSortMode.Programmatic,
                        HeaderText = columnName
                    };
                    Columns.Add(gridViewColumn);
                }
            }

        }

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);
            var clientPoint = PointToClient(MousePosition);
            var selIndex = HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            if (selIndex != -1 && this.ItemClicked != null)
            {
                var row = ((DataModel.CustomDataView)DataSource)[selIndex].Row;
                this.ItemClicked(this, new DataModel.ShowEventArgs(selIndex, row));
            }

        }

        public event VaultAtlas.DataModel.ShowEvent ItemClicked;

        private static readonly string[] ColumnNames = { "Status", "Artist", "Date", "City", "Venue", "Quality", "Length", "Source", "DateUpdated" };

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
