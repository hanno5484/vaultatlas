using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaultAtlas.FlacAtlas;

namespace VaultAtlas.DataModel
{
    public class LocalDirectory : IFileSystemDirectory
    {
        public LocalDirectory(string path)
        {
            _path = path;
        }

        private readonly string _path;

        public IEnumerable<IFileSystemDirectory> GetSubDirectories()
        {
            return Directory.GetDirectories(_path).Select(d => new LocalDirectory(d));
        }

        public IEnumerable<IFileSystemFile> GetFiles()
        {
            return Directory.GetFiles(_path).Select(f => new LocalFileInfo(f));
        }

        public string Name { get { return Path.GetFileName(_path); } }

        string IFileSystemDirectory.GetLocalDirectoryPath()
        {
            return _path;
        }
    }
}
