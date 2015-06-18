using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using CarlosAg.ExcelXmlWriter;
using CarlosAg.ExcelXmlWriter.Schemas;


namespace VaultAtlas.FlacAtlas
{
    public class ExcelExporter
    {
        public void WriteExcelFile(DataManager manager, string fileName, bool startAfterSave)
        {
            Workbook book = new Workbook();

            // Some optional properties of the Document
            book.Properties.Author = System.Threading.Thread.CurrentPrincipal.Identity.Name;
            book.Properties.Title = "FlacAtlas Export";
            book.Properties.Created = DateTime.Now;

            WorksheetStyle style = book.Styles.Add("HeaderStyle");
            style.Font.FontName = "Verdana";
            style.Font.Size = 12;
            style.Font.Bold = true;

            style = book.Styles.Add("Default");
            style.Font.FontName = "Tahoma";
            style.Font.Size = 9;

            string[] columnNames = new string[] { "DirectoryName", "Disc", "Path" };
            int[] columnWidth = new int[] { 300,100,500 };

            Worksheet sheet = book.Worksheets.Add("List");

            WorksheetRow row = sheet.Table.Rows.Add();
            row.Height = 25;

            for(int i=0;i<columnNames.Length;i++)
            {
                WorksheetColumn targetColumn = new WorksheetColumn(columnWidth[i]);
                sheet.Table.Columns.Add(targetColumn);
                row.Cells.Add(new WorksheetCell(columnNames[i], "HeaderStyle"));
            }

            /* TODO QUANTUM
            DataView directoryView = new DataView( manager.Data.Tables["Directory"],
                "isnull( ParentUID, '' ) = ''", "DiscNumber, Name",DataViewRowState.CurrentRows);
            foreach (DataRowView rowView in directoryView)
            {
                AddDirectoryRow(sheet.Table, rowView.Row, rowView["DiscNumber"].ToString());
            }

            book.Save(fileName);
            if (startAfterSave)
                System.Diagnostics.Process.Start(fileName);
             */
        }

        private void AddDirectoryRow(WorksheetTable table, DataRow row, string parentPath)
        {
            string dirName = row["Name"].ToString();
            string displayName = row["DisplayName"].ToString();
            if (displayName.Length == 0)
                displayName = dirName;

            string thisPath = parentPath + "\\" + dirName;
            if (!string.IsNullOrEmpty(dirName))
            {
                WorksheetRow sheetrow = table.Rows.Add();
                sheetrow.Cells.Add(dirName);
                sheetrow.Cells.Add(row["DiscNumber"].ToString());
                sheetrow.Cells.Add(thisPath);
            }
            foreach (DataRow childRow in row.Table.Select("parentuid = '" + row["DirectoryUID"].ToString() + "'"))
                this.AddDirectoryRow(table, childRow, thisPath);
        }

    }
}
