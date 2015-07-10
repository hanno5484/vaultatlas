using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using VaultAtlas.DataModel.sqlite;
using VaultAtlas.FlacAtlas;

namespace VaultAtlas.DataModel.FlacAtlas
{
    public class Disc : DataRowObject, IFileSystemProvider
    {
        public Disc(DataRow row) : base(row)
        {
        }

        #region IFileSystemProvider Members

        private AdapterBase _directories;

        public AdapterBase GetDirectoriesAdapter(string whereClause = null)
        {
            if (_directories != null)
                return _directories;

            var data = new DataSet("FlacAtlas");

            var dt = data.Tables.Add("DirectoryInfo");

            var queryText = "select * from Directory where DiscNumber = @DiscNumber" + (whereClause != null ? " and (" + whereClause + ")" : "");
            var da = new SQLiteDataAdapter(queryText, Model.ApplicationModel.Conn);
            da.SelectCommand.Parameters.Add("DiscNumber", DbType.String).Value = DiscNumber;

            da.FillSchema(dt, SchemaType.Source);
            da.Fill(dt);
            
            _directories = new AdapterBase(dt, da);
            return _directories;
        }

        string IFileSystemProvider.RootName
        {
            get { return "/"; }
        }

        public DiscDirectoryInfo GetRootDir()
        {
            return new DiscDirectoryInfo(GetDirectoriesAdapter("parentuid = ''").Table.Rows.Cast<DataRow>().First());
        }

        public const string RootName = "/";

        #endregion

        public DataRow CreateRootDirOnDisc()
        {
            var newRow = GetDirectoriesAdapter().Table.NewRow();
            newRow["UID"] = Guid.NewGuid().ToString();
            newRow["DiscNumber"] = DiscNumber;
            newRow["ParentUID"] = "";
            newRow.Table.Rows.Add(newRow);
            return newRow;
        }

        IFileSystemDirectory IFileSystemProvider.GetRootDirectory()
        {
            return GetRootDir();
        }

        public string DiscNumber
        {
            get { return Row.Field<string>("DiscNumber"); }
            set { Row["DiscNumber"] = value; }
        }

        public bool IsWritable
        {
            get { return 1 == Row.Field<long?>("IsWritable"); }
            set { Row["IsWritable"] = value ? 1 : 0; }
        }

        public string VolumeID
        {
            get { return Row.Field<string>("VolumeID"); }
            set { Row["VolumeID"] = value; }
        }

        public string SerialNumber
        {
            get { return Row.Field<string>("SerialNumber"); }
            set { Row["SerialNumber"] = value; }
        }

        public string PathOnVolume
        {
            get { return Row.Field<string>("PathOnVolume"); }
            set { Row["PathOnVolume"] = value; }
        }

        public string GetLocalDirectoryPath()
        {
            // find the drive that currently contains the volume matching my serial number
            var logDrives = Directory.GetLogicalDrives();
            var drive = logDrives.Select(logDrive => new DriveInformation(logDrive))
                .FirstOrDefault(dr => string.Equals(dr.SerialNumber, SerialNumber));

            if (drive == null)
                throw new DirectoryNotFoundException();

            var p = PathOnVolume;
            // http://stackoverflow.com/questions/53102/why-does-path-combine-not-properly-concatenate-filenames-that-start-with-path-di
            if (p.StartsWith(Path.DirectorySeparatorChar + ""))
                p = p.Substring(1);

            return Path.Combine(drive.LogDrive, p);
        }
    }
}
