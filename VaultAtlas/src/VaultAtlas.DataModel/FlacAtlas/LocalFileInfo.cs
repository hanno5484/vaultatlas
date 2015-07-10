using System;
using System.IO;
using VaultAtlas.FlacAtlas;

namespace VaultAtlas.DataModel
{
    public class LocalFileInfo : IFileSystemFile
    {
        public LocalFileInfo(string fileName)
        {
            _fileName = fileName;
        }

        private readonly string _fileName;

        public string Name
        {
            get { return _fileName; }
        }

        public long Size
        {
            get { return new FileInfo(_fileName).Length; }
        }

        public byte[] GetFileContent()
        {
            using (var sr = new FileStream(_fileName, FileMode.Open))
            {
                var buff = new byte[sr.Length];
                sr.Read(buff, 0, (int)sr.Length);
                return buff;
            }
        }

        public long GetLengthSeconds()
        {
            return 0;
        }

        public DateTime LastModifiedDate
        {
            get { return File.GetLastWriteTime(_fileName); }
        }
    }
}
