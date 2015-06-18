using System;
using System.Collections.Generic;

namespace VaultAtlas.DataModel
{
    public class Filter
    {
        public event EventHandler FilterChanged;

        private string _whereClauseDate = "";
        private string _whereClauseArtist = "";

        private string _customFilterExpression = "";
        public string CustomFilterExpression
        {
            get
            {
                return _customFilterExpression;
            }
            set
            {
                _customFilterExpression = value;
                RebuildWhereClause();
            }
        }

        public void RebuildWhereClause()
        {

            var filters = new List<string>();

            if (_whereClauseDate != string.Empty)
            {
                if (_whereClauseArtist != string.Empty)
                    filters.Add(_whereClauseDate + " and " + _whereClauseArtist);
                else
                    filters.Add(_whereClauseDate);
            }
            else if (_whereClauseArtist != "")
                filters.Add(_whereClauseArtist);

            // add category filter
            if (this._categoryFilter != null && !this._categoryFilter.Equals(string.Empty))
            {
                filters.Add("Parent.Folder = '" + Util.MakeSelectSafe(_categoryFilter) + "'");
            }

            // add custom filter
            if (!string.IsNullOrEmpty(_customFilterExpression))
            {
                filters.Add(_customFilterExpression);
            }

            // add show need replacement
            if (_showNeedReplacement.HasValue )
            {
                filters.Add("needreplacement = " + (_showNeedReplacement.Value ? "true" : "false"));
            }

            // add text filter
            if (!string.IsNullOrEmpty(textFilter))
            {
                string pattern = "like '%" + Util.MakeSelectSafe(this.textFilter) + "%'";
                filters.Add("( artist " + pattern + " or venue " + pattern + " or city " + pattern + " )");
            }

            var filterExpression = string.Join(" and ", filters);
            if (EvaluatedExpression != filterExpression)
            {
                EvaluatedExpression = filterExpression;
                if (FilterChanged != null)
                    FilterChanged(this, EventArgs.Empty);
            }
        }

        private bool? _showNeedReplacement;
        public bool? ShowNeedReplacement
        {
            get
            {
                return _showNeedReplacement;
            }
            set
            {
                if (_showNeedReplacement != value)
                {
                    _showNeedReplacement = value;
                    RebuildWhereClause();
                }
            }
        }

        public string EvaluatedExpression { get; private set; }

        private string textFilter;
        public string TextFilter
        {
            get
            {
                return textFilter;
            }
            set
            {
                textFilter = value;
                RebuildWhereClause();
            }
        }

        public string WhereClause
        {
            get
            {
                return _whereClauseDate;
            }
            set
            {
                _whereClauseDate = value;
                RebuildWhereClause();
            }
        }


        private string _categoryFilter;
        public string CategoryFilter
        {
            get
            {
                return _categoryFilter;
            }
            set
            {
                _categoryFilter = value;
                RebuildWhereClause();
            }
        }

        public Artist SelectedArtist
        {
            set
            {
                if (value == null)
                    _whereClauseArtist = "";
                else
                    _whereClauseArtist = "Artist = '" + value.SortName + "'";
                RebuildWhereClause();
            }
        }

        public void ClearFilters()
        {
            _whereClauseArtist = string.Empty;
            _whereClauseDate = string.Empty;
            _customFilterExpression = string.Empty;
            _categoryFilter = string.Empty;
            RebuildWhereClause();
        }

    }

}
