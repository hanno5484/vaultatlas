using System;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Data;
using VaultAtlas.DataModel.Indexer;
using VaultAtlas.DataModel.sqlite;
using VaultAtlas.DataModel.UndoRedo;

namespace VaultAtlas.DataModel
{
	public class Model
	{
		internal static Model ApplicationModel;

        public static readonly Model SingleModel = new Model();

		private Model() 
		{
			PlugInManager = new PlugInManager( this );
			UndoRedoManager = new UndoRedoManager( this );
			ApplicationModel = this;

            UndoRedoManager.UndoRedoStateChanged +=undoRedoManager_UndoRedoStateChanged;
		}

	    private SQLiteConnection _conn;
	    public SQLiteConnection Conn
	    {
	        get
	        {
	            if (_conn == null)
	                _conn = new Connection().GetConnection();

	            return _conn;
	        }
	    }

	    private readonly string[] _categories = { "Alt.country", "Jazz", "Pop", "Jamband", "Belgium", "Rock", "Alternate", "Classical" };
		public string[] Categories 
		{
			get 
			{
				return this._categories;
			}
		}

	    public UndoRedoManager UndoRedoManager { get; private set; }

	    private CustomDataView _view;
	    public CustomDataView ShowView
	    {
	        get
	        {
	            if (_view == null)
	            {
                    _view = new CustomDataView(Shows.Table);

	            }
                return _view;
            }
	    }

	    private void InitAdapters()
	    {
            var data = new DataSet("VaultAtlas");

            data.Tables.Add("Shows");
            data.Tables.Add("Artists");

            var da = new SQLiteDataAdapter("select * from Shows", Conn);
            var dt = data.Tables["Shows"];
            da.FillSchema(dt, SchemaType.Source);
            da.Fill(dt);

            _venueIndexer = new DependendIndexer(dt.Columns["Venue"], dt.Columns["City"]);
            _cityPrefixIndexer = new PrefixIndexer(dt.Columns["City"]);

            _shows = new ShowsAdapter(dt, da);

            var adapterArtists = new SQLiteDataAdapter("select * from Artists", Conn);
            var dtArtists = data.Tables["Artists"];
            adapterArtists.FillSchema(dtArtists, SchemaType.Source);
            adapterArtists.Fill(dtArtists);

            _artists = new ArtistsAdapter(dtArtists, adapterArtists);
	    }

	    public ShowsAdapter Shows
	    {
	        get
	        {
	            if (_shows == null)
	                InitAdapters();

	            return _shows;
	        }
	    }

	    public ArtistsAdapter Artists
	    {
	        get
	        {
	            if (_artists == null)
	                InitAdapters();

	            return _artists;
	        }
	    }

	    public PlugInManager PlugInManager { get; private set; }

	    public string this[ string settingKey ] 
		{
			get { return "yyyy-MM-dd"; }
			set 
			{
                /* TODO QUANTUM
				var dr = Data.Tables[ "Settings" ].Rows.Find( settingKey );
				if ( dr == null ) 
				{
					Data.Tables[ "Settings" ].Rows.Add( new object[]{ settingKey, value } );
				}
				else
					dr[ "Value" ] = value;
                 */
			}
		}

		public event UndoRedoStateEventHandler UndoRedoStatusChanged;

	    public IModelNavigator ModelNavigator { get; set; }

	    public Show NewShow(string artistSortName, ShowDate date)
		{
		    var newShow = new Show(Shows.Table.NewRow())
		    {
		        ArtistSortName = artistSortName,
                DateUpdated = DateTime.Now,
                Date = date,
		        IsPublic = true
		    };
            newShow.Row.Table.Rows.Add(newShow.Row);
	        return newShow;
		}

	    public Show NewShow()
	    {
	        var newShow = new Show(Shows.Table.NewRow())
	        {
	            DateUpdated = DateTime.Now,
	            Date = new ShowDate("????-??-??"),
	            IsPublic = true
	        };
	        newShow.Row.Table.Rows.Add(newShow.Row);
	        return newShow;
	    }

	    private DependendIndexer _venueIndexer;
		public IEnumerable<string> GetBestForVenuePrefix( string input ) 
		{
			return _venueIndexer.GetValues( input );
		}

		private PrefixIndexer _cityPrefixIndexer;
	    private ShowsAdapter _shows;
	    private ArtistsAdapter _artists;

	    public IEnumerable<string> GetBestForCityPrefix( string prefix ) 
		{
			return _cityPrefixIndexer.GetBestForPrefixes( prefix );
		}

	    public IEnumerable<string> GetCities()
	    {
	        return _cityPrefixIndexer.GetAllEntries();
	    }

		private void undoRedoManager_UndoRedoStateChanged(object sender, UndoRedoStateEventArgs args)
		{
			if (UndoRedoStatusChanged != null)
				UndoRedoStatusChanged( this, args );
		}
	}
}
