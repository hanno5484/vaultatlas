using System;
using System.Collections;
using System.Collections.Generic;

namespace VaultAtlas.FlacAtlas
{
	public interface IFileMetaInfoProvider
	{
        IDictionary<string, object> GetMetaInfo(string file);

		long GetLengthSeconds( string file );

	}
}
