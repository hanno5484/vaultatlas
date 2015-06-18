using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaultAtlas.DataModel.sqlite
{
    class LegacyVaultAtlasModel
    {
        public DataSet GetDataSet()
        {

            var fileName = @"C:\Users\hannob\Dropbox\grapeliste.xml";
            var ds = new DataSet();
            ds.ReadXml(fileName, XmlReadMode.ReadSchema);

            return ds;
        }


        private void CreateTableStructure(DataSet data)
        {
            DataTable dt = new DataTable("Artists");
            dt.Columns.Add("DisplayName").DefaultValue = "";
            DataColumn dcArtist = dt.Columns.Add("SortName");
            dt.PrimaryKey = new DataColumn[] { dcArtist };
            dcArtist.DefaultValue = "";
            dt.Columns.Add("ETreeArtistID", typeof(int)).DefaultValue = 0;
            dt.Columns.Add("Abbreviation").DefaultValue = "";
            data.Tables.Add(dt);

            DataTable dtShows = new DataTable("Shows");
            DataColumn dcShowArtist = dtShows.Columns.Add("Artist");
            dtShows.Columns.Add("Date", typeof(string));
            dtShows.PrimaryKey = new DataColumn[] { dtShows.Columns.Add("UID") };
            dtShows.Columns.Add("Link").DefaultValue = "";
            dtShows.Columns.Add("Venue").DefaultValue = "";
            dtShows.Columns.Add("City").DefaultValue = "";
            dtShows.Columns.Add("Length").DefaultValue = "";
            dtShows.Columns.Add("Source").DefaultValue = "";
            dtShows.Columns.Add("Comments").DefaultValue = "";
            dtShows.Columns.Add("Loc").DefaultValue = "";
            dtShows.Columns.Add("SHN").DefaultValue = "";
            dtShows.Columns.Add("TSource").DefaultValue = "";
            dtShows.Columns.Add("FolderName").DefaultValue = "";
            dtShows.Columns.Add("Quality", typeof(byte)).DefaultValue = 0;
            dtShows.Columns.Add("DateUpdated", typeof(DateTime)).DefaultValue = DateTime.Now;
            data.Tables.Add(dtShows);

            dtShows.Columns["Artist"].ExtendedProperties["Width"] = "120";
            dtShows.Columns["Link"].ExtendedProperties["Width"] = "80";
            dtShows.Columns["Length"].ExtendedProperties["Width"] = "50";
            dtShows.Columns["Source"].ExtendedProperties["Width"] = "150";
            dtShows.Columns["Date"].ExtendedProperties["Width"] = "80";
            dtShows.Columns["DateUpdated"].ExtendedProperties["Width"] = "80";
            dtShows.Columns["Venue"].ExtendedProperties["Width"] = "100";
            dtShows.Columns["City"].ExtendedProperties["Width"] = "80";

            DataRelation relArtist = data.Relations.Add(dcArtist, dcShowArtist);
            relArtist.RelationName = "Show2Artist";
            relArtist.ChildKeyConstraint.UpdateRule = Rule.Cascade;
            relArtist.ChildKeyConstraint.DeleteRule = Rule.SetNull;
            relArtist.ChildKeyConstraint.AcceptRejectRule = AcceptRejectRule.Cascade;

            this.UpdateSchema(data);

            data.AcceptChanges();

            RegisterEventHandlers(data);
        }


        private void UpdateSchema(DataSet data)
        {
            if (data.Relations[0].RelationName == null)
                data.Relations[0].RelationName = "Show2Artist";

            var showTable = data.Tables["Shows"];
            if (showTable.Columns["Quality"] == null)
                showTable.Columns.Add("Quality", typeof(string));

            if (showTable.Columns["IsMaster"] == null)
                showTable.Columns.Add("IsMaster", typeof(bool));

            if (showTable.Columns["IsVideo"] == null)
                showTable.Columns.Add("IsVideo", typeof(bool));

            if (showTable.Columns["IsObsolete"] == null)
                showTable.Columns.Add("IsObsolete", typeof(bool));

            if (showTable.Columns["IsPublic"] == null)
            {
                var dcIsPublic = showTable.Columns.Add("IsPublic", typeof(bool));
                dcIsPublic.DefaultValue = true;
            }



            if (showTable.Columns["Loc"] == null)
                showTable.Columns.Add("Loc", typeof(string));

            if (showTable.Columns["SHN"] == null)
                showTable.Columns.Add("SHN", typeof(string));

            if (showTable.Columns["TSource"] == null)
                showTable.Columns.Add("TSource", typeof(string));

            if (showTable.Columns["FolderName"] == null)
                showTable.Columns.Add("FolderName", typeof(string));

            if (showTable.Columns["PlugInData"] != null)
                showTable.Columns.Remove("PlugInData");

            if (data.Tables["Settings"] == null)
            {
                var dtSettings = new DataTable("Settings");
                var dcSettingKey = new DataColumn("Key");
                var dcSettingValue = new DataColumn("Value");
                dcSettingKey.DataType = typeof(string);
                dcSettingValue.DataType = typeof(string);
                dtSettings.Columns.Add(dcSettingKey);
                dtSettings.Columns.Add(dcSettingValue);
                dtSettings.PrimaryKey = new[] { dcSettingKey };
                data.Tables.Add(dtSettings);
            }

            if (data.Tables["Artists"].Columns["Folder"] == null)
                data.Tables["Artists"].Columns.Add("Folder", typeof(string));

            if (data.Tables["Folders"] == null)
            {
                var dtFolder = new DataTable("Folders");
                var dcFolderName = new DataColumn("Name", typeof(string));
                dtFolder.Columns.Add(dcFolderName);
                dtFolder.PrimaryKey = new[] { dcFolderName };
                data.Tables.Add(dtFolder);

                DataRelation relFolders = data.Relations.Add(dcFolderName, data.Tables["Artists"].Columns["Folder"]);
                relFolders.RelationName = "Artist2Folder";
                relFolders.ChildKeyConstraint.UpdateRule = Rule.Cascade;
                relFolders.ChildKeyConstraint.DeleteRule = Rule.SetNull;
                relFolders.ChildKeyConstraint.AcceptRejectRule = AcceptRejectRule.Cascade;
            }

            if (data.Tables["Resources"] == null)
            {
                var dtResources = new DataTable("Resources");
                var dcResourceShowUID = new DataColumn("UID_Show");
                var dcResourceKey = new DataColumn("Key");
                var dcResourceValue = new DataColumn("Value");
                dcResourceKey.DataType = typeof(string);
                dcResourceShowUID.DataType = typeof(string);
                dcResourceValue.DataType = typeof(object);
                dtResources.Columns.Add(dcResourceShowUID);
                dtResources.Columns.Add(dcResourceKey);
                dtResources.Columns.Add(dcResourceValue);
                dtResources.PrimaryKey = new[] { dcResourceShowUID, dcResourceKey };
                data.Tables.Add(dtResources);
            }

            if (data.Tables["Resources"].Columns["Owner"] == null)
            {
                var dcOwner = new DataColumn("Owner");
                dcOwner.DataType = typeof(string);
                dcOwner.DefaultValue = string.Empty;
                data.Tables["Resources"].Columns.Add(dcOwner);
            }

            if (showTable.Columns["NeedReplacement"] == null)
            {
                var dcNeedReplacement = new DataColumn("NeedReplacement");
                dcNeedReplacement.DataType = typeof(bool);
                dcNeedReplacement.DefaultValue = false;
                showTable.Columns.Add(dcNeedReplacement);
            }

            if (data.Tables["Resources"].Columns["Type"] == null)
            {
                var dcType = new DataColumn("Type");
                dcType.DataType = typeof(string);
                dcType.DefaultValue = string.Empty;
                data.Tables["Resources"].Columns.Add(dcType);
            }

            if (data.Relations["Resource2Show"] == null)
            {
                var rowsArray = new DataRow[data.Tables["Resources"].Rows.Count];
                data.Tables["Resources"].Rows.CopyTo(rowsArray, 0);
                foreach (DataRow dr in rowsArray)
                {
                    var uidReferenced = dr["UID_Show"].ToString();
                    DataRow drRef = showTable.Rows.Find(uidReferenced);
                    if (drRef == null)
                    {
                        dr.Delete();
                    }
                }

                var relResources = data.Relations.Add(showTable.PrimaryKey[0], data.Tables["Resources"].Columns["UID_Show"]);
                relResources.RelationName = "Resource2Show";
                relResources.ChildKeyConstraint.UpdateRule = Rule.Cascade;
                relResources.ChildKeyConstraint.DeleteRule = Rule.Cascade;
                relResources.ChildKeyConstraint.AcceptRejectRule = AcceptRejectRule.Cascade;

                if (showTable.Columns["Link"] != null)
                    showTable.Columns.Remove("Link");

            }


            showTable.Columns["Artist"].DefaultValue = null;

            foreach (DataTable dt in data.Tables)
                foreach (DataColumn dc in dt.Columns)
                    dc.ColumnMapping = MappingType.Element;

            foreach (var row in showTable.Select("IsPublic is null"))
                row["IsPublic"] = true;
            foreach (var row in showTable.Select("Ismaster is null"))
                row["Ismaster"] = false;
            foreach (var row in showTable.Select("NeedReplacement is null"))
                row["NeedReplacement"] = false;

        }


        private void UnregisterEventHandlers(DataSet data)
        {
            data.Tables["Shows"].RowChanged -= ShowTableRowChanged;
            data.Tables["Shows"].RowChanging -= ShowTableRowChanging;
        }

        private void RegisterEventHandlers(DataSet data)
        {
            data.Tables["Shows"].RowChanged += ShowTableRowChanged;
            data.Tables["Shows"].RowChanging += ShowTableRowChanging;
            data.Tables["Shows"].RowDeleted += ShowTableRowDeleted;
        }

        private void ShowTableRowChanging(object sender, DataRowChangeEventArgs e)
        {
            /* TODO QUANTUM
            if (this.ShowChanging != null)
                this.ShowChanging(this, new ShowEventArgs(-1, e.Row));
             */
        }

        private void ShowTableRowChanged(object sender, DataRowChangeEventArgs e)
        {
            /* TODO QUANTUM
            if (e.Action == DataRowAction.Add && this.ShowAdded != null)
                this.ShowAdded(this, new ShowEventArgs(-1, e.Row));
            if (e.Action == DataRowAction.Delete && this.ShowDeleted != null)
                this.ShowDeleted(this, new ShowEventArgs(-1, e.Row));
            if (e.Action == DataRowAction.Commit && this.ShowChanged != null)
                this.ShowChanged(this, new ShowEventArgs(-1, e.Row));
             */
        }

        private void ShowTableRowDeleted(object sender, DataRowChangeEventArgs e)
        {
            /* TODO QUANTUM
            if (this.ShowDeleted != null)
                this.ShowDeleted(this, new ShowEventArgs(-1, e.Row));
             */
        }

        public void SaveFile()
        {
            // this.Data.WriteXml( filename, XmlWriteMode.WriteSchema );
        }

        private DataSet LoadOldFile(string filename)
        {
            var ds = new DataSet();
            ds.ReadXml(filename, XmlReadMode.ReadSchema);
            return ds;
        }

        public void LoadFile(string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException();

            if (this.ModelRecycling != null)
                this.ModelRecycling(this, new EventArgs());

            /*
            var data = new DataSet("VaultAtlas");
			UpdateSchema(data);
			RegisterEventHandlers(data);
			if (ModelRecycled != null)
				ModelRecycled( this, new EventArgs());

			LastFileName = filename;

			UndoRedoManager.RegisterEventHandlers(data);
			data.AcceptChanges();
             */

            var config = ApplicationConfig.GetConfig();
            var mruFile = new MRUList(5, config["MRUFiles"]);
            mruFile.Add(filename);
            config["MRUFiles"] = mruFile.GetStringRepresentation();
            config.Save();
        }

        public event EventHandler ModelRecycling;
        public event EventHandler ModelRecycled;

    }
}
