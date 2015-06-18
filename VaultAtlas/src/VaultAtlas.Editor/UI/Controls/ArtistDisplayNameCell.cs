using VaultAtlas.DataModel;
using System.Windows.Forms;

namespace VaultAtlas.UI.Controls
{
    public class ArtistDisplayNameCell : DataGridViewTextBoxCell
    {
        public ArtistDisplayNameCell()
        {
            Style.Font = new System.Drawing.Font("Tahoma", 8, System.Drawing.FontStyle.Bold);
        }

        protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, System.ComponentModel.TypeConverter valueTypeConverter, System.ComponentModel.TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
        {
            var artist = value != null ? value.ToString() : null;
            if (artist != null)
            {
                artist = Model.SingleModel.Artists.GetDisplayNameForSortName(artist);
            }
            return base.GetFormattedValue(artist, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
        }
    }
}
