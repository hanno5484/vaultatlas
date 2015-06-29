using VaultAtlas.DataModel;

namespace VaultAtlas.FlacAtlas
{
	public interface IFileMetaInfoProvider
	{
        MediaFormatInfo GetMediaFormatInfo(string file);

	}
}
