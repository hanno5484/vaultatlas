using System;
using System.Data;
using CarlosAg.ExcelXmlWriter;
using CarlosAg.ExcelXmlWriter.Schemas;
using VaultAtlas.DataModel;

namespace VaultAtlas.UI.Export
{
	public class ExcelExporter
	{
		public ExcelExporter()
		{
		}

		public void WriteExcelFile( string fileName, bool startAfterSave ) 
		{
			Workbook book = new Workbook();

			// Some optional properties of the Document
			book.Properties.Author = System.Threading.Thread.CurrentPrincipal.Identity.Name;
			book.Properties.Title = "VaultAtlas List Export";
			book.Properties.Created = DateTime.Now;

			// Add some styles to the Workbook
			WorksheetStyle style = book.Styles.Add( "HeaderStyle" );
			style.Font.FontName = "Verdana";
			style.Font.Size = 12;
			style.Font.Bold = true;

			// Create the Default Style to use for everyone
			style = book.Styles.Add("Default");
			style.Font.FontName = "Tahoma";
			style.Font.Size = 9;

			CustomDataView view = VaultAtlas.VaultAtlasApplication.Model.ShowView;

			string[] columnNames = new string[]{ "Artist", "Date", "City", "Venue", "Quality", "Length", "Source" };

			// Add a Worksheet with some data
			Worksheet sheet = book.Worksheets.Add("List");

			WorksheetRow row = sheet.Table.Rows.Add();
			row.Height = 25;

			foreach( string columnName in columnNames ) 
			{
				DataColumn dc = view.Table.Columns[ columnName ];
				int dcWidth = dc.ExtendedProperties["Width"] != null ? Int32.Parse( dc.ExtendedProperties["Width"].ToString()) : 100;
				WorksheetColumn targetColumn = new WorksheetColumn( dcWidth );
				sheet.Table.Columns.Add( targetColumn );
				row.Cells.Add(new WorksheetCell( columnName, "HeaderStyle"));
			}

			foreach( DataRowView rowView in view ) 
			{
				row = sheet.Table.Rows.Add();
				foreach( string columnName in columnNames ) 
				{
					object cellValue = rowView[ columnName ];
						
					switch( columnName ) 
					{
						case "Artist":
							cellValue = VaultAtlasApplication.Model.Artists.GetDisplayNameForSortName( cellValue.ToString());
							break;
						case "Quality":
							cellValue = cellValue is DBNull ? string.Empty :
								Quality.QualityNames[ (byte) cellValue ];
							break;
					}

					row.Cells.Add( cellValue.ToString());
				}
			}

			if ( startAfterSave )
				System.Diagnostics.Process.Start(fileName);
		}
	}
}
