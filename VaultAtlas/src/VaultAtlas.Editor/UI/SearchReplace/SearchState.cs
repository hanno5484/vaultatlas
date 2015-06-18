using System;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using VaultAtlas.DataModel;

namespace VaultAtlas.UI.SearchReplace
{
	public class SearchState
	{

		private int rowCursor, columnCursor = 0, beginRow, beginColumn;
		private bool ignoreCase, replaceMode;
		private string searchString, replaceString;

		private bool silent = false;
		public bool Silent
		{
			get 
			{
				return this.silent;
			}
			set 
			{
				this.silent = value;
			}
		}

		
		private bool searchResources;
		public bool SearchResources 
		{
			get 
			{
				return this.searchResources;
			}
			set 
			{
				this.searchResources = value;
			}
		}

		public int RowNumber
		{
			get 
			{
				return this.rowCursor;
			}
		}

		public int ColumnNumber
		{
			get 
			{
				return this.columnCursor;
			}
		}

		public DataRow Row 
		{
			get 
			{
				return VaultAtlasApplication.Model.ShowView[this.rowCursor].Row;
			}
		}

		public SearchState(string searchString, bool ignoreCase, int currentRow)
            : this(searchString, "", ignoreCase, currentRow)
		{
			this.replaceMode = false;
		}

		public SearchState(string searchString, string replaceString, bool ignoreCase, int currentRow)
		{
			this.beginColumn = this.columnCursor = 0;
			this.beginRow = this.rowCursor = currentRow;
			this.ignoreCase = ignoreCase;
			this.searchString = ignoreCase ? searchString.ToUpper() : searchString;
			this.replaceString = ignoreCase ? replaceString.ToUpper() : replaceString;
		}


		public bool Continue() 
		{
			CustomDataView view = VaultAtlasApplication.Model.ShowView;
			int columnCount = view.Table.Columns.Count;
			while (true)
			{
                columnCursor = (columnCursor+1) % columnCount;
				if (columnCursor == 0) 
				{
					rowCursor = (rowCursor+1) % view.Count;
					if (rowCursor == 0) 
					{
						if ( !this.silent &&
                            MessageBox.Show( resources.SearchReachedEnd, Constants.ApplicationName,
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button1) != DialogResult.Yes) 
						{
							return false;
						}
					}

					// search resources for this show if needed
					if ( this.SearchResources )
						foreach( Resource res in view.GetShow( rowCursor ).GetResources())
							if ( res.Value is string )
							{
								if ( (ignoreCase ? res.Value.ToString().ToUpper() : res.Value.ToString()).IndexOf( searchString ) != -1)
									return true;
							}
				}
				
				if (columnCursor == beginColumn && rowCursor == beginRow) 
				{
					if ( !this.silent )
                        MessageBox.Show(resources.NoFurtherOccurrences, Constants.ApplicationName,
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
					return false;
				}

				string val = view[rowCursor][columnCursor] as string;
				if (val != null) 
				{
					if (ignoreCase)
						val = val.ToUpper();
					if (val.IndexOf(searchString) != -1)
						return true;
				}
			}
		}
	}
}
