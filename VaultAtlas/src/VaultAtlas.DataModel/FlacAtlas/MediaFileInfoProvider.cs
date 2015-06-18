using System.IO;
using System.Linq;
using VaultAtlas.DataModel.FlacAtlas;

namespace VaultAtlas.FlacAtlas
{
    public class MediaFileInfoProvider
    {
        public MediaFileInfoProvider(string directory)
        {
            _directory = directory;
        }

        private readonly string _directory;

        public long GetLengthSecondsAllFiles()
        {
            var p = new FlacProvider();

            var acc = Directory.GetFiles(_directory, "*.flac", SearchOption.AllDirectories).Sum(file => p.GetLengthSeconds(file));

            return Directory.GetFiles(_directory, "*.mp3", SearchOption.AllDirectories).Aggregate(acc, (current, file) => current + GetLengthMpegFile(file));
        }

        private int GetLengthMpegFile(string fileName)
        {
            var mp3hdr = new MpegProvider();
            bool ismp3 = mp3hdr.ReadMpegInformation(fileName);
            if (ismp3)
                return mp3hdr.intLength;
            return 0;
        }
    }
}
