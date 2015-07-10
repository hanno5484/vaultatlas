using System;
using System.Data;

namespace VaultAtlas.DataModel
{
	public class Resource : DataRowObject
    {
	    public Resource(DataRow row) : base(row)
	    {
	    }

        public string Uid
        {
            get { return Row["UID"] as string; }
            set { Row["UID"] = value; }
        }

	    public string UidShow
	    {
	        get { return Row["UID_Show"] as string; }
	        set { Row["UID_Show"] = value; }
	    }

	    public string ResourceType
	    {
	        get { return Row["Type"] as string; }
	        set { Row["Type"] = value; }
	    }

        public string Key
        {
            get { return Row["Key"] as string; }
            set { Row["Key"] = value; }
        }

        public string Value
        {
            get { return Row.Field<string>("Value"); }
            set { Row["Value"] = value; }
        }
	}
}
