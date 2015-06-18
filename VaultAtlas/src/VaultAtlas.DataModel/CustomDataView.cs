using System;
using System.Data;

namespace VaultAtlas.DataModel
{
	public class CustomDataView : DataView
	{
		internal CustomDataView(DataTable dt) : base(dt)
		{
			Sort = "Artist, Date";
			Filter = new Filter();
		}

	    public CustomDataView ParentView { get; set; }

	    private Filter _filter;
		public Filter Filter 
		{
			get 
			{
				return this._filter;
			}
			set 
			{
				if ( _filter != null )
					_filter.FilterChanged -= filter_FilterChanged;
				_filter = value;
				if ( _filter != null )
					_filter.FilterChanged += filter_FilterChanged;
				RebuildWhereClause();
			}
		}

		private ShowCollection _shows;
		public ShowCollection Shows 
		{
			get 
			{
				if ( _shows == null )
					_shows = new ShowCollection( this );
				return _shows;
			}
		}

		public void RebuildWhereClause() 
		{
			_filter.RebuildWhereClause();

			string filterExpression = _filter.EvaluatedExpression;
			// add parent filter
			if ( ParentView != null && ParentView.RowFilter != null && !ParentView.RowFilter.Equals(string.Empty )) 
			{
				if ( filterExpression.Length > 0)
					filterExpression += " and ";
				filterExpression += ParentView.RowFilter;
			}

			RowFilter = filterExpression;
		}

		public CustomDataView CreateChildView()
		{
		    var child = new CustomDataView(Table) {ParentView = this};
		    child.RebuildWhereClause();
			return child;
		}

	    public int GetFirstRowForArtist(string artist)
	    {
	        for (int rowIndex = 0; rowIndex < this.Count; rowIndex++)
	            if (this[rowIndex]["Artist"].ToString() == artist)
	                return rowIndex;
	        return 0;
	    }

	    public int GetShowRowIndex(Show show)
        {
            var showUid = show.UID;
            for (var rowIndex = 0; rowIndex < this.Count; rowIndex++)
                if (this[rowIndex].Row["UID"].ToString() == showUid)
                    return rowIndex;
            return -1;
        }

		public Show GetShow( int index ) 
		{
			return Shows[ this[ index ]["UID"].ToString() ];
		}

		private void filter_FilterChanged(object sender, EventArgs e)
		{
			string filterExpression = _filter.EvaluatedExpression;
			// add parent filter
			if ( ParentView != null && ParentView.RowFilter != null && !ParentView.RowFilter.Equals(string.Empty )) 
			{
				if ( filterExpression.Length > 0)
					filterExpression += " and ";
				filterExpression += ParentView.RowFilter;
			}

			RowFilter = filterExpression;
		}
	}
}
