using System;
using System.Data;
using VaultAtlas.FlacAtlas;

namespace VaultAtlas.DataModel.FlacAtlas
{
    public class DiscFileInfo : DataRowObject, IFileSystemFile
    {
        public DiscFileInfo(DataRow row) : base(row)
        {
        }

        public long Size
        {
            get { return Row.Field<long>("Size"); }
            set { Row.SetField("Size", value); }
        }

        public byte[] FileContent
        {
            get { return Row.Field<byte[]>("Content"); }
            set { Row.SetField("Content", value); }
        }

        public long GetLengthSeconds()
        {
            return Length;
        }

        public string Name
        {
            get { return Row.Field<string>("Name"); }
        }

        public string DisplayName
        {
            get { return Row.Field<string>("DisplayName"); }
            set { Row.SetField("DisplayName", value); }
        }

        public string FullPath
        {
            get { return Row.Field<string>("FullPath"); }
            set { Row.SetField("FullPath", value); }
        }

        public string UidDirectory
        {
            get { return Row.Field<string>("Directory"); }
            set { Row.SetField("Directory", value); }
        }

        public long Length
        {
            get { return Row.Field<long?>("Length").GetValueOrDefault(); }
            set { Row.SetField("Length", value); }
        }

        public string FormatIdentifier
        {
            get { return Row.Field<string>("FormatId"); }
            set { Row["FormatId"] = value; }
        }

        public long BitRate
        {
            get { return Row.Field<long>("BitRate"); }
            set { Row["BitRate"] = value; }
        }

        public long Bps
        {
            get { return Row.Field<long>("Bps"); }
            set { Row["Bps"] = value; }
        }

        public long SampleRate
        {
            get { return Row.Field<long>("SampleRate"); }
            set { Row["SampleRate"] = value; }
        }

        public long NrChannels
        {
            get { return Row.Field<long>("NrChannels"); }
            set { Row["NrChannels"] = value; }
        }

        public DateTime LastModifiedDate
        {
            get
            {
                var s = (string)Row["DateLastModified"];
                return DateTime.ParseExact(s, "s", System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat);
            }
            set
            {
                Row["DateLastModified"] = value.ToString("s", System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat);
            }
        }
    }
}
