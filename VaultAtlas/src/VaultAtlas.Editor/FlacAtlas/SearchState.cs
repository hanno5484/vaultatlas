using System;
using System.Collections;
using System.Windows.Forms;
using System.Data;

namespace VaultAtlas.FlacAtlas
{
	public class SearchState
	{

		private int rowCursor, beginRow;

        private readonly bool _ignoreCase;

		private string searchString;

	    public bool Silent { get; set; }

	    public DataRow CurrentRow { get; private set; }


	    public bool SearchResources { get; set; }

	    public string FullPath
		{
			get 
			{
                return this.rowCursor != -1 ? this._view[this.rowCursor]["FullPath"].ToString() : null;
			}
		}

        private string _lastDirectory;

	    public bool ResultIsDirectory { get; private set; }

	    public int ColumnNumber { get; private set; }

	    public SearchState(string searchString, bool ignoreCase, int currentRow)
		{
		    ResultIsDirectory = false;
		    CurrentRow = null;
		    Silent = false;
		    this.searchString = ignoreCase ? searchString.ToUpper() : searchString;
            this._ignoreCase = ignoreCase;
            this.ColumnNumber = currentRow;
            this.beginRow = currentRow;
            /* TODO QUANTUM
            this._view = new DataView(DataManager.Get().Data.Tables["FileInfo"],
                null, "FullPath", DataViewRowState.CurrentRows);
             */
		}

        private readonly DataView _view;

        public bool Continue()
        {

            while (true)
            {
                rowCursor = _view.Count == 0 ? 0 : (rowCursor + 1) % _view.Count;
                if (rowCursor == 0)
                {
                    if (!this.Silent &&
                        MessageBox.Show(resources.SearchReachedEnd, "FlacAtlas",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1) != DialogResult.Yes)
                    {
                        return false;
                    }
                }

                if (rowCursor == beginRow)
                {
                    if (!this.Silent)
                        MessageBox.Show(resources.NoFurtherOccurrences, "FlacAtlas",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                DataRowView rowView = this._view[rowCursor];
                this.CurrentRow = rowView.Row;

                string thisRowFullPath = rowView["FullPath"] as string;
                if (thisRowFullPath != null)
                {
                    int thisRowParentDirIndex = thisRowFullPath.LastIndexOf('/');
                    int thisRowParentDirIndex2 = thisRowFullPath.LastIndexOf('/', thisRowParentDirIndex - 1);
                    string parentDirName = thisRowFullPath.Substring(thisRowParentDirIndex2, thisRowParentDirIndex - thisRowParentDirIndex2 + 1);

                    try
                    {
                        if (parentDirName != this._lastDirectory)
                        {
                            if (this.IsMatch(parentDirName, searchString))
                                return true;
                            else
                            {
                                // displayName des parentDirs könnte auch noch stimmen
                                string parentDirDisplayName = this._view.Table.DataSet.Tables["Directory"].Rows.Find(rowView["Directory"])["DisplayName"].ToString();
                                if (parentDirDisplayName != null && parentDirDisplayName.Length > 0 && IsMatch(parentDirDisplayName, searchString))
                                    return true;
                            }
                        }
                    }
                    finally
                    {
                        this._lastDirectory = parentDirName;
                    }
                }

                if (this.IsMatch(rowView["Name"] as string, searchString))
                    return true;
                if (!(rowView["DisplayName"] is DBNull) && this.IsMatch(rowView["DisplayName"].ToString(), searchString))
                    return true;
            }

        }

        private bool IsMatch(string val, string search)
        {
            string v = this._ignoreCase ? val.ToUpper() : val;
            return v.IndexOf(search) != -1;
        }
	}
}
