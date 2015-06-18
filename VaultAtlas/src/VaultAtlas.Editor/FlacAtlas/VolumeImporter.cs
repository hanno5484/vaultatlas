using System;
using VaultAtlas.DataModel.FlacAtlas;
using VaultAtlas.DataModel.ModelUI;
using System.Data;
using System.Threading;
using System.Windows.Forms;

namespace VaultAtlas.FlacAtlas
{

    public class RecursiveImporter
    {
        public RecursiveImporter( IFileSystemProvider localFileProvider, DataManager manager,
            string serialNumber, string volumeName, string discNumber)
        {
            this.manager = manager;
            this.serialNumber = serialNumber;
            this.discNumber = discNumber;
            this.volumeName = volumeName;
            this.localFileProvider = localFileProvider;
        }

        private DataManager manager;
        private string discNumber;
        private string volumeName;
        private string serialNumber;
        private IFileSystemProvider localFileProvider;

        private VaultAtlas.DataModel.ModelUI.IProgressCallback callback;

        private void RecursiveImport(string parentDirUid, IFileSystemDirectory dir)
        {
            string directoryUid = Guid.NewGuid().ToString();
            var newRow = transact.AddRow("Directory");
            newRow["UID"] = directoryUid;
            newRow["Name"] = dir.Name;
            newRow["ParentUID"] = parentDirUid;
            newRow["DiscNumber"] = discNumber;
            newRow.Table.Rows.Add(newRow);

            try
            {
                foreach (var subdir in dir.GetSubDirectories())
                {
                    this.RecursiveImport(directoryUid, subdir);
                    if (this.callback.IsAborting)
                    {
                        return;
                    }
                }

                foreach (var file in dir.GetFiles())
                {
                    var fileName = file.Name;
                    callback.SetText(fileName);

                    var size = file.Size;

                    var content = file.GetFileContent();

                    var newFileInfo = transact.AddRow("FileInfo");
                    newFileInfo["Directory"] = directoryUid;
                    newFileInfo["Name"] = fileName;
                    newFileInfo["Size"] = size;
                    newFileInfo["Length"] = file.GetLengthSeconds();
                    newFileInfo["DateLastModified"] = file.LastModifiedDate;
                    newFileInfo["UID"] = Guid.NewGuid().ToString();
                    newFileInfo["Content"] = content;
                    newFileInfo.Table.Rows.Add(newFileInfo);

                    if (callback.IsAborting)
                    {
                        return;
                    }
                }
            }
            catch
            {
            }
        }

        private AddRowsTransaction transact;

        public void DoImport(object status)
        {
            var callback = status as IProgressCallback;
            this.callback = callback;

            using (this.transact =new AddRowsTransaction(this.manager ))
            {
                try
                {
                    DataRow[] presentBySerialNumber = this.manager.Discs.Table.Select("SerialNumber = '" +
                        DataManager.SafeSelect(this.serialNumber) + "'");
                    if (presentBySerialNumber.Length > 0)
                    {
                        var result = MessageBox.Show(string.Format(resources.AskAlreadyPresentVolumeIDOverwrite, presentBySerialNumber[0]["DiscNumber"], serialNumber),
                            "FlacAtlas", MessageBoxButtons.YesNoCancel);

                        if (result == DialogResult.Cancel)
                            return;

                        if (result == DialogResult.Yes)
                        {
                            discNumber = presentBySerialNumber[0]["DiscNumber"].ToString();
                            // TODO QUANTUM this.manager.DeleteRecursive(this.manager.RootName + this.discNumber, false);
                        }
                    }


                    var newRow = transact.AddRow("Disc");
                    newRow["DiscNumber"] = discNumber;
                    newRow["VolumeID"] = volumeName;
                    newRow["SerialNumber"] = serialNumber;
                    newRow.Table.Rows.Add(newRow);

                    RecursiveImport( null, localFileProvider.GetRootDirectory());

                    new Disc(newRow).GetRootDir().UpdateFullPath();
                }
                catch (ThreadAbortException)
                {
                    // We want to exit gracefully here (if we're lucky)
                }
                catch (ThreadInterruptedException)
                {
                    // And here, if we can
                }
                finally
                {
                    if (this.callback.IsAborting)
                        transact.Abort();

                    if (callback != null)
                        callback.End();
                }
            }
        }
    }

}
