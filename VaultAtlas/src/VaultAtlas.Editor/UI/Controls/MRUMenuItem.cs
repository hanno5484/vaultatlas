using System;
using VaultAtlas.DataModel;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VaultAtlas.UI.Controls
{
	public class ShowListMenuItem : MenuItem
	{
		public ShowListMenuItem()
		{
		    if (!this.DesignMode)
		    {
		        this.Select += ShowListMenuItem_Select;
		    }
		}

	    public IEnumerable<Show> ShowList { get; set; }

	    private IDictionary<MenuItem, Show> _menuItemsToShows;

		private void ShowListMenuItem_Select(object sender, EventArgs e)
		{
            _menuItemsToShows = new Dictionary<MenuItem, Show>();
			MenuItems.Clear();

			foreach( var show in ShowList ) 
			{
                /* TODO QUANTUM
				if ( show.IsDeleted )
					continue;
                */
			    var showMenuItem = new MenuItem(show.Display);
			    _menuItemsToShows[showMenuItem] = show;
			    showMenuItem.Click += showMenuItem_Click;
				MenuItems.Add( showMenuItem );
                if (MenuItems.Count > 10)
                    break;
			}
		}

		private void showMenuItem_Click(object sender, EventArgs e)
		{
		    if (_menuItemsToShows == null) return;
		    
            var show = _menuItemsToShows[ (MenuItem) sender ];
		    if ( show != null )
		    {
		        VaultAtlasApplication.MainForm.RequestEditShow( -1, show );
		    }
		}
	}
}
