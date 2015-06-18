using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaultAtlas.DataModel.sqlite;
using VaultAtlas.FlacAtlas;

namespace VaultAtlas.DataModel.FlacAtlas
{
    public class DiscDirectoryInfo : DataRowObject, IFileSystemDirectory
    {
        public DiscDirectoryInfo(DataRow row) : base(row)
        {
        }

        private AdapterBase _files;
        private AdapterBase _subDirs;

        public AdapterBase GetFilesAdapter()
        {
            if (_files != null)
                return _files;

            var data = new DataSet("FlacAtlas");

            var dt = data.Tables.Add("FileInfo");

            var da = new SQLiteDataAdapter("select * from FileInfo where Directory = @Directory", Model.SingleModel.Conn);
            da.SelectCommand.Parameters.Add("Directory", DbType.String).Value = UID;

            da.FillSchema(dt, SchemaType.Source);
            da.Fill(dt);

            _files = new AdapterBase(dt, da);
            return _files;
        }

        public AdapterBase GetSubDirAdapter()
        {
            if (_subDirs != null)
                return _subDirs;

            var data = new DataSet("FlacAtlas");

            var dt = data.Tables.Add("Directory");

            var da = new SQLiteDataAdapter("select * from Directory where ParentUID = @Directory", Model.SingleModel.Conn);
            da.SelectCommand.Parameters.Add("Directory", DbType.String).Value = UID;

            da.FillSchema(dt, SchemaType.Source);
            da.Fill(dt);

            _subDirs = new AdapterBase(dt, da);
            return _subDirs;
        }

        public string UID
        {
            get { return Row.Field<string>("UID"); }
        }

        public string Name
        {
            get { return Row.Field<string>("Name"); }
        }

        public string DisplayName
        {
            get
            {
                var displayName = Row.Field<string>("DisplayName");
                return string.IsNullOrEmpty(displayName) ? Name : displayName;
            }
            set { Row.SetField("DisplayName", value); }
        }

        public bool IsNotRead
        {
            get { return Row.Field<long>("IsNotRead") != 0; }
        }

        public string GetNodeCaption()
        {
            string text = DisplayName;
            long seconds = GetLengthSecondsDirectory();
            if (seconds != 0)
                text += " (" + Formatters.GetTimeString(seconds) + ")";

            return text;
        }

        public long GetLengthSecondsDirectory()
        {
            var l = GetSubDirectories().Sum(subDir => subDir.GetLengthSecondsDirectory());

            l += GetFiles().Sum(f => f.Length);

            return l;
        }

        IEnumerable<IFileSystemDirectory> IFileSystemDirectory.GetSubDirectories()
        {
            return GetSubDirectories();
        }

        public IEnumerable<DiscDirectoryInfo> GetSubDirectories()
        {
            return GetSubDirAdapter().Table.Rows.Cast<DataRow>().Select(r => new DiscDirectoryInfo(r));
        }

        IEnumerable<IFileSystemFile> IFileSystemDirectory.GetFiles()
        {
            return GetFiles();
        }

        public IEnumerable<DiscFileInfo> GetFiles()
        {
            return GetFilesAdapter().Table.Rows.Cast<DataRow>().Select(r => new DiscFileInfo(r));
        }

        public void UpdateFullPath()
        {
            foreach (var fileName in GetFiles())
            {
                fileName.FullPath = Name + "/" + fileName.Name;
            }
            foreach (var subDir in GetSubDirectories())
            {
                subDir.UpdateFullPath();
            }
        }
    }
}
