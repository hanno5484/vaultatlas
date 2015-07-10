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
		
	    IFileSystemDirectory GetRootDirectory();
	}

    public interface IFileSystemDirectory
    {
        IEnumerable<IFileSystemDirectory> GetSubDirectories();

        IEnumerable<IFileSystemFile> GetFiles();

        string Name { get; }

        string GetLocalDirectoryPath();
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
