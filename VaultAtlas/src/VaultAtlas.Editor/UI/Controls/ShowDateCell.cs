using System;
using System.Drawing;
using System.Data;
using VaultAtlas.DataModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using VaultAtlas.Properties;

namespace VaultAtlas.UI.Controls
{
    public class ShowDateCell : DataGridViewTextBoxCell
    {
        static ShowDateCell()
        {
            imageMaster = resources.StatusMaster;
            imageVideo = resources.StatusVideo;
            imageReplace = resources.StatusReplace;
            imagePrivate = resources.StatusPrivate;
        }

        private static Image imageMaster, imageVideo, imageReplace, imagePrivate;

        protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, System.ComponentModel.TypeConverter valueTypeConverter, System.ComponentModel.TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
        {
            string theDate = ShowDate.Parse(value.ToString()).ToString(VaultAtlasApplication.Model["DateFormat"], null);
            return base.GetFormattedValue(theDate, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
        }

        protected override void Paint(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds,
            System.Drawing.Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState,
            object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            if ((paintParts & DataGridViewPaintParts.ContentForeground) != 0)
            {
                DataRowView view = ((CustomDataView) this.DataGridView.DataSource)[rowIndex];
                string video = view["IsVideo"].ToString().ToLower();
                string master = view["IsMaster"].ToString().ToLower();
                string needReplacement = view["NeedReplacement"].ToString().ToLower();
                string isPublic = view["IsPublic"].ToString().ToLower();
                int x = 3;
                if (video.Equals("1"))
                {
                    graphics.DrawImage(imageVideo, x, cellBounds.Top);
                    x += 19;
                }
                if (master.Equals("1"))
                {
                    graphics.DrawImage(imageMaster, x, cellBounds.Top);
                    x += 19;
                }
                if (needReplacement.Equals("1"))
                {
                    graphics.DrawImage(imageReplace, x, cellBounds.Top);
                    x += 19;
                }
                if (!isPublic.Equals("1"))
                {
                    graphics.DrawImage(imagePrivate, x, cellBounds.Top);
                    x += 19;
                }
                base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
            }
        }
    }
}
