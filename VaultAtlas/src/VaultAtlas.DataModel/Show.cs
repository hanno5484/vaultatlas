using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using VaultAtlas.DataModel.sqlite;

namespace VaultAtlas.DataModel
{
	public class Show : DataRowObject
    {
	    public Show(DataRow row) : base(row)
	    {
	        if (string.IsNullOrEmpty(UID))
	            UID = Guid.NewGuid().ToString();
	    }

	    public event ShowEvent ValueChanged;

	    public byte Quality
	    {
	        get
	        {
	            var s = Row.Field<string>("Quality");
	            return string.IsNullOrEmpty(s) ? (byte) 0 : (byte) (s[0] - '0');
	        }
	        set
	        {
	            Row["Quality"] = value + "";
	        }
	    }

	    public AdapterBase GetResourcesAdapter()
	    {
	        var conn = Model.ApplicationModel.Conn;
	        var data = new DataSet();
	        var dt = data.Tables.Add("Resources");
	        var cmd = new SQLiteCommand("select * from Resources where UID_Show = @uidshow", conn);
	        cmd.Parameters.Add("uidshow", DbType.String).Value = UID;
	        data.EnforceConstraints = false;

	        var da = new SQLiteDataAdapter(cmd);
	        da.FillSchema(dt, SchemaType.Source);
	        da.Fill(dt);

	        CorrectResourceUid(dt, conn);

	        return new AdapterBase(dt, da);
	    }

	    private void CorrectResourceUid(DataTable dt, SQLiteConnection conn)
	    {
	        foreach (DataRow row in dt.Rows)
	        {
	            if (string.IsNullOrEmpty(row["UID"] as string))
	            {
                    var newUid = Guid.NewGuid().ToString();
	                // imported resources have no UID
	                row["UID"] = newUid;

	                var updateCmd = new SQLiteCommand("update resources set UID = @newuid where UID_Show = @uidshow and Key = @key", conn);
                    updateCmd.Parameters.Add("newuid", DbType.String).Value = newUid;
                    updateCmd.Parameters.Add("uidshow", DbType.String).Value = UID;
	                updateCmd.Parameters.Add("key", DbType.String).Value = row["Key"];
	                conn.Open();
	                var chk = updateCmd.ExecuteScalar();
	                conn.Close();
                    row.AcceptChanges();
	            }
	        }
	    }

	    public IEnumerable<Resource> GetResources()
		{
		    var ad = GetResourcesAdapter();
            return ad.Table.Rows.Cast<DataRow>().Select(r => new Resource(r)).ToArray();
		}

        public string Loc
        {
            get { return Row.Field<string>("Loc"); }
            set { Row["Loc"] = value; }
        }


        public string TradingSource
        {
            get { return Row.Field<string>("TSource"); }
            set { Row["TSource"] = value; }
        }

        public string FolderName
        {
            get { return Row.Field<string>("FolderName"); }
            set { Row["FolderName"] = value; }
        }

	    public string UID
	    {
	        get { return Row.Field<string>("UID"); }
            private set { Row["UID"] = value; }
	    }

	    public string Display 
		{
			get 
			{
			
                var art = Artist;
                var dateFormat = Model.ApplicationModel["DateFormat"];
				return ( ( art != null ? art.DisplayName : "unknown artist" ) + " - " + Date.ToString(dateFormat, null) );
			}
		}

		public static Show GetShow( DataRow row ) 
		{
			if ( row == null )
				return null;
			if ( row["UID"] is DBNull )
				row["UID"] = Guid.NewGuid().ToString();
		    return new Show(row);
		}

		public Artist Artist 
		{
			get 
			{
                return Model.ApplicationModel.Artists.Get(ArtistSortName);
			}
            set
            {
                if (Artist != value)
                    ArtistSortName = value.SortName;
            }
		}

	    public bool IsVideo
	    {
	        get { return 1 == Row.Field<long?>("IsVideo"); }
	        set { Row["IsVideo"] = value ? 1 : 0; }
	    }

        public bool IsPublic
        {
            get { return 1 == Row.Field<long?>("IsPublic"); }
            set { Row["IsPublic"] = value ? 1 : 0; }
        }
        public bool NeedReplacement
        {
            get { return 1 == Row.Field<long?>("NeedReplacement"); }
            set { Row["NeedReplacement"] = value ? 1 : 0; }
        }

        public bool IsMaster
        {
            get { return 1 == Row.Field<long?>("IsMaster"); }
            set { Row["IsMaster"] = value ? 1 : 0; }
        }

        public bool IsObsolete
        {
            get { return 1 == Row.Field<long?>("IsObsolete"); }
            set { Row["IsObsolete"] = value ? 1 : 0; }
        }

	    public string ArtistSortName
	    {
	        get { return Row.Field<string>("Artist"); }
	        set { Row["Artist"] = value; }
	    }

	    public string Source
	    {
	        get { return Row.Field<string>("Source"); }
	        set { Row["Source"] = value; }
	    }

	    public string DateString
	    {
	        get { return Row.Field<string>("Date"); }
	        set { Row["Date"] = value; }
	    }

	    public ShowDate Date 
		{
			get 
			{
				return new ShowDate( DateString );
			}
            set
            {
                if (!Date.Equals(value))
                    DateString = value.ToString();
            }
		}

        public DateTime DateUpdated
        {
            get
            {
                var s = (string)Row["DateUpdated"];
                return DateTime.ParseExact(s, "s", System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat);
            }
            set { Row["DateUpdated"] = value.ToString("s", System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat); }
        }

	    public void Touch() 
		{
			DateUpdated = DateTime.Now;
		}

	    public string LengthRaw
	    {
	        get { return Row.Field<string>("Length"); }
	        set { Row["Length"] = value; }
	    }

	    public string Length
		{
			get 
			{
				return LengthRaw;
			}
			set 
			{
				if (value.IndexOf("+") != -1) 
				{
					try 
					{
						string[] lengths = value.Split('+');
						int sum = 0;
						for(int i=0;i<lengths.Length;i++)
							sum += Int32.Parse( lengths[i] );
						LengthRaw = sum.ToString();
					}
					catch 
					{
						LengthRaw = value;
					}
				}
				else if( value != Length)
					LengthRaw = value;
			}
		}

	    public string Venue
	    {
	        get { return Row.Field<string>("Venue"); }
	        set { Row["Venue"] = value; }
	    }

	    public string City
	    {
	        get { return Row.Field<string>("City"); }
	        set { Row["City"] = value; }
	    }

	    public string SHN
	    {
	        get { return Row.Field<string>("SHN"); }
	        set { Row["SHN"] = value; }
	    }

	    public string Comments
	    {
	        get { return Row.Field<string>("Comments"); }
	        set { Row["Comments"] = value; }
	    }
	}
}
