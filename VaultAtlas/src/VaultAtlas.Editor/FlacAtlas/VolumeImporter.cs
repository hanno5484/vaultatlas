using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public DiscDirectoryInfo TargetDirectoryInfo { get; private set; }

        private void RecursiveImport(DiscDirectoryInfo parentDir, IFileSystemDirectory sourceDir, Disc disc, bool importAllSub)
        {
            var targetDir = parentDir == null
                ? disc.GetRootDir()
                : new DiscDirectoryInfo(GetOrCreateDirectoryRow(parentDir, sourceDir, disc));

            var subDirAdapter = targetDir.GetSubDirAdapter();
            var filesAdapter = targetDir.GetFilesAdapter();

            try
            {

                if (_targetPath != null && targetDir.GetLocalDirectoryPath().Equals(_targetPath))
                {
                    TargetDirectoryInfo = targetDir;
                    importAllSub = true;
                }

                foreach (var subdir in sourceDir.GetSubDirectories())
                {
                    // support sparse structure
                    if (!importAllSub && (_targetPath != null && !_targetPath.StartsWith(subdir.GetLocalDirectoryPath())))
                    {
                        continue;
                    }

                    RecursiveImport(targetDir, subdir, disc, importAllSub);
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

        private static DataRow GetOrCreateDirectoryRow(DiscDirectoryInfo parentDir, IFileSystemDirectory dir, Disc disc)
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

            var newFileInfo = new DiscFileInfo(GetOrCreateFileRow(filesAdapter, fileName));

            var size = file.Size;

            if (size <= 10000)
            {
                newFileInfo.FileContent = file.FileContent;
            }

            newFileInfo.UidDirectory = directoryUid;
            newFileInfo.Size = size;
            newFileInfo.LastModifiedDate = file.LastModifiedDate;

            var mediaprovider = MediaFileInfoProvider.GetMetaInfoProvider(file.Name);
            if (mediaprovider != null)
            {
                var formatInfo = mediaprovider.GetMediaFormatInfo(file.Name);
                newFileInfo.BitRate = formatInfo.BitRate;
                newFileInfo.SampleRate = formatInfo.SampleRate;
                newFileInfo.Bps = formatInfo.BitsPerSample;
                newFileInfo.NrChannels = formatInfo.NumberChannels;
                newFileInfo.FormatIdentifier = formatInfo.FormatIdentifier;
                newFileInfo.Length = formatInfo.LengthSeconds;
            }
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

        public async Task<Disc> ImportDisc(IProgressCallback status, string discNumber, string volumeName, string serialNumber)
        {
            try
            {
                var presentBySerialNumber = DataManager.Get().Discs.Table.Select("SerialNumber = '" + DataManager.SafeSelect(serialNumber) + "'");
                if (presentBySerialNumber.Length > 0)
                {
                    var result = MessageBox.Show(string.Format(resources.AskAlreadyPresentVolumeIDOverwrite, presentBySerialNumber[0]["DiscNumber"], serialNumber),
                        Constants.ApplicationName, MessageBoxButtons.YesNoCancel);

                    if (result == DialogResult.Cancel)
                        return null;

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
                await Task.Factory.StartNew(() => RecursiveImport(rootDir, _rootDirectory, newDisc, true)).ConfigureAwait(false);
                rootDir.GetSubDirAdapter().Update();

                rootDir.UpdateFullPath();

                return newDisc;
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

            return null;
        }

        public async Task<DiscDirectoryInfo> ImportPartialStructure(IProgressCallback status, DiscDirectoryInfo rootDir, string targetPath, Disc targetDisc)
        {
            try
            {
                _targetPath = targetPath;
                _callback = status;
                await Task.Factory.StartNew(() => RecursiveImport(null, _rootDirectory, targetDisc, false)).ConfigureAwait(false);
                rootDir.GetSubDirAdapter().Update();

                rootDir.UpdateFullPath();
                return TargetDirectoryInfo;
            }
            finally
            {
                if (_callback != null)
                    _callback.End();
            }
        }
    }

}
