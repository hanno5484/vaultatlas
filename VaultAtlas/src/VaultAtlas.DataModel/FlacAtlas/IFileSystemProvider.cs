using System;
using System.Collections.Generic;

namespace VaultAtlas.FlacAtlas
{
	public interface IFileSystemProvider
	{
		string RootName
		{
			get;
		}

		DateTime GetLastModifiedDate( string fileName );
		
        string GetSerialNumber(string volume);
        string GetVolumeName(string volume);

	    IFileSystemDirectory GetRootDirectory();
	}

    public interface IFileSystemDirectory
    {
        IEnumerable<IFileSystemDirectory> GetSubDirectories();

        IEnumerable<IFileSystemFile> GetFiles();

        string Name { get; }
    }

    public interface IFileSystemFile
    {
        string Name { get; }

        long Size { get; }

        byte[] GetFileContent();

        long GetLengthSeconds();

        DateTime LastModifiedDate { get; }
    }
}
