using System;
using System.IO;
using VaultAtlas.DataModel;

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
	}
}
