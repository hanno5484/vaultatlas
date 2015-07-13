using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using VaultAtlas.DataModel;
using VaultAtlas.DataModel.FlacAtlas;
using VaultAtlas.DataModel.sqlite;

namespace VaultAtlas.FlacAtlas
{
    internal class ShowDirectoryMatcher
    {
        private static readonly MD5 Md5 = new MD5Cng();

        public void Match(Action<string, Show> associateAction)
        {
            var showResourceInfos = new Dictionary<string, Resource>();

            foreach (var resource in GetResourcesAdapter().Table.Rows.Cast<DataRow>().Select(r => new Resource(r)))
            {
                try
                {
                    var str = GetCanonicalString(resource.Value);
                    if (string.IsNullOrEmpty(str))
                        continue;

                    str = GetMd5String(str);

                    showResourceInfos[str] = resource;
                }
                catch
                {
                }
            }

            var res = GetCatalogInfos(showResourceInfos, DataManager.Get().Discs.Table.Rows.Cast<DataRow>().Select(r => new Disc(r)));


            var form = new Form();
            var v = new MatchingResultViewer {Dock = DockStyle.Fill, AssociateAction = associateAction};
            form.Controls.Add(v);
            v.AddItems(res);
            form.ShowDialog();
        }


        public AdapterBase GetResourcesAdapter()
        {
            var conn = Model.SingleModel.Conn;
            var data = new DataSet();
            var dt = data.Tables.Add("Resources");
            var cmd = new SQLiteCommand("select * from Resources where UID_Show in (select UID from Shows where UID_Directory is null)", conn);
            data.EnforceConstraints = false;

            var da = new SQLiteDataAdapter(cmd);
            da.FillSchema(dt, SchemaType.Source);
            da.Fill(dt);

            return new AdapterBase(dt, da);
        }

        private static string GetMd5String(string str)
        {
            return Encoding.UTF8.GetString(Md5.ComputeHash(Encoding.UTF8.GetBytes(str)));
        }

        private static IDictionary<string, Show> GetCatalogInfos(IDictionary<string, Resource> dict, IEnumerable<Disc> rootDiscs)
        {
            var res = new Dictionary<string, Show>();

            foreach (var rootDisc in rootDiscs)
            {
                if (rootDisc.IsWritable)
                {
                    // writable disc -> assume volume is available
                    var localPath = rootDisc.GetLocalDirectoryPath();
                    GetCatalogInfoRecursive(dict, localPath, res);
                }
                else
                {
                    continue;
                    // non-writable disc -> read all resources directly from SQL
                    var data = new DataSet(Constants.ApplicationName);
                    data.Tables.Add("FileInfo");

                    var selectCmd = new SQLiteCommand("select * from FileInfo where content is not null and (name like '%.ffp' or name like '%.txt') and directory in (select uid from directory where discnumber = @DiscNr)", Model.SingleModel.Conn);
                    selectCmd.Parameters.Add("DiscNr", DbType.String).Value = rootDisc.DiscNumber;
                    var da = new SQLiteDataAdapter(selectCmd);
                    var dt = data.Tables["FileInfo"];
                    da.FillSchema(dt, SchemaType.Source);
                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        var s1 = Encoding.UTF8.GetString((byte[]) row["Content"]);
                        var s = GetMd5String(GetCanonicalString(s1));

                        if (dict.ContainsKey(s))
                        {
                            res[s] = new Show(Model.SingleModel.Shows.Table.Rows.Find(dict[s].UidShow));
                        }
                    }
                }
            }
            return res;
        }

        private static void GetCatalogInfoRecursive(IDictionary<string, Resource> targetDict, string localPath, IDictionary<string, Show> result)
        {
            try
            {
                foreach (var subDir in Directory.GetDirectories(localPath))
                {
                    GetCatalogInfoRecursive(targetDict, subDir, result);
                }
            }
            catch (Exception exc)
            {
                // TODO log - could be access denied
            }

            try
            {

                foreach (var f in Directory.GetFiles(localPath))
                {
                    try
                    {
                        var fi = new LocalFileInfo(f);
                        if (fi.Size >= 10000)
                            continue;
                        var s = GetMd5String(GetCanonicalString(Encoding.UTF8.GetString(fi.FileContent)));
                        if (targetDict.ContainsKey(s))
                        {
                            result[localPath] = new Show(Model.SingleModel.Shows.Table.Rows.Find(targetDict[s].UidShow));
                        }
                    }
                    catch (Exception exc)
                    {
                        // TODO log - could be access denied

                    }
                }
            }
            catch
            {
                // TODO log - could be access denied
            }
        }

        private static string GetCanonicalString(string s1)
        {
            var s = s1.Replace("\r", "").Replace("\n", "").Replace(" ", "").ToLower();
            return s;
        }
    }
}
