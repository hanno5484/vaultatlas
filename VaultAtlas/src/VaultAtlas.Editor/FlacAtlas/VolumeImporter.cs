using System;
using System.Collections;
using System.Collections.Generic;
using VaultAtlas.DataModel;
using VaultAtlas.DataModel.FlacAtlas;
using VaultAtlas.DataModel.ModelUI;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using VaultAtlas.DataModel.sqlite;

namespace VaultAtlas.FlacAtlas
{
    public class RecursiveImporter
    {
        public RecursiveImporter(IFileSystemDirectory rootDirectory)
        {
            _rootDirectory = rootDirectory;
        }

        private readonly IFileSystemDirectory _rootDirectory;

        private IProgressCallback _callback;

        /// <summary>
        /// Defines the target path for importing a sparse structure. Only the hierarchy leading to this path will be imported.
        /// </summary>
        private string _targetPath;

        private void RecursiveImport(DiscDirectoryInfo parentDir, IFileSystemDirectory sourceDir, Disc disc)
        {
            var targetDir = parentDir == null
                ? disc.GetRootDir()
                : new DiscDirectoryInfo(GetOrCreateDirectoryRow(parentDir, sourceDir, disc));

            var subDirAdapter = targetDir.GetSubDirAdapter();
            var filesAdapter = targetDir.GetFilesAdapter();

            try
            {

                foreach (var subdir in sourceDir.GetSubDirectories())
                {
                    // support sparse structure
                    if (_targetPath != null && !_targetPath.StartsWith(subdir.GetLocalDirectoryPath()))
                    {
                        continue;
                    }

                    RecursiveImport(targetDir, subdir, disc);
                }

                foreach (var file in sourceDir.GetFiles())
                {
                    ImportFile(file, filesAdapter, targetDir.UID);
                }

                subDirAdapter.Update();
                filesAdapter.Update();
            }
            catch
            {
            }
        }

        private DataRow GetOrCreateDirectoryRow(DiscDirectoryInfo parentDir, IFileSystemDirectory dir, Disc disc)
        {
            var parentDirAdapter = parentDir.GetSubDirAdapter();
            var existingRows = parentDirAdapter.Table.Select("Name = '" + Util.MakeSelectSafe(dir.Name) + "'");
            if (existingRows.Length > 0)
                return existingRows[0];

            var newRow = parentDirAdapter.Table.NewRow();
            newRow["UID"] = Guid.NewGuid().ToString();
            newRow["Name"] = dir.Name;
            newRow["ParentUID"] = parentDir.UID;
            newRow["DiscNumber"] = disc.DiscNumber;
            newRow.Table.Rows.Add(newRow);
            return newRow;
        }

        private void ImportFile(IFileSystemFile file, AdapterBase filesAdapter, string directoryUid)
        {
            var fileName = file.Name;
            _callback.SetText(fileName);

            var newFileInfo = GetOrCreateFileRow(filesAdapter, fileName);

            var size = file.Size;

            if (size <= 10000)
            {
                var content = file.GetFileContent();
                newFileInfo["Content"] = content;
            }

            newFileInfo["Directory"] = directoryUid;
            newFileInfo["Size"] = size;
            newFileInfo["Length"] = file.GetLengthSeconds();
            newFileInfo["DateLastModified"] = file.LastModifiedDate.ToString(System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat);
        }

        private static DataRow GetOrCreateFileRow(AdapterBase filesAdapter, string fileName)
        {
            var existingRows = filesAdapter.Table.Select("Name = '" + Util.MakeSelectSafe(fileName) + "'");
            if (existingRows.Length > 0)
                return existingRows[0];

            var newFileInfo = filesAdapter.Table.NewRow();
            newFileInfo["Name"] = fileName;
            newFileInfo["UID"] = Guid.NewGuid().ToString();
            newFileInfo.Table.Rows.Add(newFileInfo);
            return newFileInfo;
        }

        public void ImportDisc(IProgressCallback status, string discNumber, string volumeName, string serialNumber)
        {
            try
            {
                var presentBySerialNumber = DataManager.Get().Discs.Table.Select("SerialNumber = '" + DataManager.SafeSelect(serialNumber) + "'");
                if (presentBySerialNumber.Length > 0)
                {
                    var result = MessageBox.Show(string.Format(resources.AskAlreadyPresentVolumeIDOverwrite, presentBySerialNumber[0]["DiscNumber"], serialNumber),
                        Constants.ApplicationName, MessageBoxButtons.YesNoCancel);

                    if (result == DialogResult.Cancel)
                        return;

                    if (result == DialogResult.Yes)
                    {
                        discNumber = presentBySerialNumber[0]["DiscNumber"].ToString();
                        // TODO QUANTUM this.manager.DeleteRecursive(this.manager.RootName + this.discNumber, false);
                    }
                }


                var newRow = DataManager.Get().Discs.Table.NewRow();
                newRow["DiscNumber"] = discNumber;
                newRow["VolumeID"] = volumeName;
                newRow["SerialNumber"] = serialNumber;
                newRow.Table.Rows.Add(newRow);
                var newDisc = new Disc(newRow);
                var rootDir = newDisc.GetRootDir();
                RecursiveImport(rootDir, _rootDirectory, newDisc);
                rootDir.GetSubDirAdapter().Update();

                rootDir.UpdateFullPath();
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
                if (_callback != null)
                    _callback.End();
            }
        }

        public void ImportPartialStructure(IProgressCallback status, DiscDirectoryInfo rootDir, string targetPath, Disc targetDisc)
        {
            try
            {
                _targetPath = targetPath;
                _callback = status;
                RecursiveImport(null, _rootDirectory, targetDisc);
                rootDir.GetSubDirAdapter().Update();

                rootDir.UpdateFullPath();
            }
            finally
            {
                if (_callback != null)
                    _callback.End();
            }
        }
    }

}
