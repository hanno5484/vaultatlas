using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VaultAtlas.DataModel.FlacAtlas;

namespace VaultAtlas.FlacAtlas
{
	public class LocalFileSystemProvider : IFileSystemProvider
	{
		public LocalFileSystemProvider( string root )
		{
			RootName = root;
		}

		#region IFileSystemProvider Member

		public string[] GetSubDirectories( string dir )
		{
			try 
			{
				return Directory.GetDirectories( dir );
			}			
			catch 
			{
				return new string[0];
			}
		}

	    public IFileSystemDirectory GetRootDirectory()
	    {
	        return new LocalDirectory(RootName);
	    }

	    public string[] GetFiles(string dir)
		{
			try 
			{
				return Directory.GetFiles( dir );
			} 
			catch 
			{
				return new string[0];
			}
		}

	    public string RootName { get; private set; }

        public byte[] GetFileContent(string fileName)
        {
            if (GetSize(fileName) < Int32.Parse(DataManager.Get()["EmbedFilesSmallerThan", "8192"]))
            {
                using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    var res = new byte[fs.Length];
                    fs.Read(res, 0, (int)fs.Length);
                    return res;
                }
            }
            return null;
        }

        private readonly FlacProvider _flacProvider = new FlacProvider();

        public long GetLengthSecondsDirectory(string directory)
        {
            return 0;
        }

		public long GetSize( string fileName ) 
		{
			try 
			{
				return new FileInfo( fileName ).Length;
			} 
			catch 
			{
				return 0;
			}
		}

		#endregion

	    class LocalDirectory : IFileSystemDirectory
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
	    }

        class LocalFileInfo : IFileSystemFile
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
}
