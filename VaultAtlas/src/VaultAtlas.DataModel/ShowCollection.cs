using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace VaultAtlas.DataModel
{
	public class ShowCollection
	{
		private readonly DataView _view;

		internal ShowCollection(  )
		{
		}

		internal ShowCollection( DataView view ) 
		{
			_view = view;
		}

		public int Count 
		{
			get 
			{
				return this._view.Count;
			}
		}

		public Show this[ string uid ] 
		{
			get 
			{
				var row = this._view.Table.Rows.Find( uid );
				return row == null ? null : Show.GetShow( row );
			}
		}


		public Show this[int i] 
		{
			get 
			{
				return Show.GetShow( _view[i].Row );
			}
		}

	    public IEnumerable<Show> Shows
	    {
	        get { return _view.Cast<DataRowView>().Select(v => Show.GetShow(v.Row)); }
	    }
	}
}
