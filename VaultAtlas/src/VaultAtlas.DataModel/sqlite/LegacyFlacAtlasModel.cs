using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaultAtlas.DataModel.sqlite
{
    class LegacyFlacAtlasModel
    {
        private void CreateEmptyDataSet()
        {
            DataSet ds = this.dataSet = new DataSet();
            DataTable dtDisc = new DataTable("Disc");
            DataTable dtDirectory = new DataTable("Directory");
            DataTable dtFileInfo = new DataTable("FileInfo");
            DataTable dtFileMetaData = new DataTable("FileMetaData");
            ds.Tables.Add(dtDisc);
            ds.Tables.Add(dtDirectory);
            ds.Tables.Add(dtFileInfo);
            ds.Tables.Add(dtFileMetaData);

            DataColumn dcDiscNo = dtDisc.Columns.Add("DiscNumber", typeof(string));
            DataColumn dcDiscVolumeID = dtDisc.Columns.Add("VolumeID", typeof(string));
            DataColumn dcDiscSerialNumber = dtDisc.Columns.Add("SerialNumber", typeof(string));
            DataColumn dcDirectoryUID = dtDirectory.Columns.Add("DirectoryUID", typeof(string));
            DataColumn dcDirectoryName = dtDirectory.Columns.Add("Name", typeof(string));
            DataColumn dcDirectoryParent = dtDirectory.Columns.Add("ParentUID", typeof(string));
            DataColumn dcDirectoryDiscNo = dtDirectory.Columns.Add("DiscNumber", typeof(string));
            DataColumn dcFileInfoDirectory = dtFileInfo.Columns.Add("Directory", typeof(string));
            DataColumn dcFileInfoName = dtFileInfo.Columns.Add("Name", typeof(string));
            DataColumn dcFileInfoSize = dtFileInfo.Columns.Add("Size", typeof(long));
            DataColumn dcFileInfoLength = dtFileInfo.Columns.Add("Length", typeof(long));
            DataColumn dcFileInfoLastMod = dtFileInfo.Columns.Add("DateLastModified", typeof(DateTime));
            DataColumn dcFileInfoFileUID = dtFileInfo.Columns.Add("FileUID", typeof(string));
            DataColumn dcFileDataContent = dtFileInfo.Columns.Add("Content", typeof(byte[]));
            DataColumn dcFileMetaFileUID = dtFileMetaData.Columns.Add("FileUID", typeof(string));
            DataColumn dcFileMetaKey = dtFileMetaData.Columns.Add("Key", typeof(string));
            DataColumn dcFileMetaValue = dtFileMetaData.Columns.Add("Value", typeof(string));

            DataTable dtSettings = new DataTable("Settings");
            ds.Tables.Add(dtSettings);
            DataColumn dcOptionsKey = dtSettings.Columns.Add("Key", typeof(string));
            DataColumn dcOptionsValue = dtSettings.Columns.Add("Value", typeof(string));

            dtDisc.PrimaryKey = new DataColumn[] { dcDiscNo };
            dtDirectory.PrimaryKey = new DataColumn[] { dcDirectoryUID };
            dtFileInfo.PrimaryKey = new DataColumn[] { dcFileInfoFileUID };
            dtFileMetaData.PrimaryKey = new DataColumn[] { dcFileMetaFileUID, dcFileMetaKey };
            dtSettings.PrimaryKey = new DataColumn[] { dcOptionsKey };

            ds.Relations.Add(dcDiscNo, dcDirectoryDiscNo);
            ds.Relations.Add(dcDirectoryUID, dcFileInfoDirectory);
            ds.Relations.Add(dcFileInfoFileUID, dcFileMetaFileUID);
        }

        private void EnsureFullPathAssigned()
        {
            bool needUpdateFullPath = true;
            while (needUpdateFullPath)
            {
                DataRow[] noFullPath = this.dataSet.Tables["FileInfo"].Select("isnull( fullpath, '' ) = ''");
                if (noFullPath.Length == 0)
                    needUpdateFullPath = false;
                else
                {
                    this.UpdateFullPath(this.RootName + this.dataSet.Tables["Directory"].Select("DirectoryUID = '" +
                        noFullPath[0]["Directory"].ToString() + "'")[0]["DiscNumber"].ToString());
                }
            }
        }

        public void Init()
        {
            if (File.Exists(this.xmlFileName))
            {
                this.CreateEmptyDataSet();
                this.UpdateSchema(); // init data schema to
                this.dataSet.ReadXml(this.xmlFileName, XmlReadMode.IgnoreSchema);
                this.EnsureFullPathAssigned();

            }
            else
            {
                this.CreateEmptyDataSet();
                this.UpdateSchema(); // init data schema to
            }
        }

        private void UpdateSchema()
        {
            try
            {
                DataTable tblDirectory = this.dataSet.Tables["Directory"];
                if (tblDirectory.Columns["IsNotRead"] == null)
                {
                    tblDirectory.Columns.Add("IsNotRead", typeof(bool));
                }

                DataTable tblFileInfo = this.dataSet.Tables["FileInfo"];
                if (tblFileInfo.Columns["FullPath"] == null)
                {
                    tblFileInfo.Columns.Add("FullPath");
                    this.UpdateFullPath("/");
                }

                if (tblFileInfo.Columns["DisplayName"] == null)
                {
                    tblFileInfo.Columns.Add("DisplayName");
                }

                if (tblDirectory.Columns["DisplayName"] == null)
                {
                    tblDirectory.Columns.Add("DisplayName");
                }

            }
            catch (Exception exc)
            {
                System.Windows.Forms.MessageBox.Show("Exception while updating schema: " + exc.Message + " " + exc.StackTrace);
            }
        }
    }

}
