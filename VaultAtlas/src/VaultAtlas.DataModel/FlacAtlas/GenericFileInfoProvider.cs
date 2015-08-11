using Microsoft.WindowsAPICodePack.Shell;
using VaultAtlas.FlacAtlas;

namespace VaultAtlas.DataModel.FlacAtlas
{
    public class GenericFileInfoProvider : IFileMetaInfoProvider
    {
        public MediaFormatInfo GetMediaFormatInfo(string file)
        {
            var formatInfo = new MediaFormatInfo();
            var so = ShellFile.FromFilePath(file);
            double nanoseconds;
            if (double.TryParse(so.Properties.System.Media.Duration.Value.ToString(), out nanoseconds))
            {
                formatInfo.LengthSeconds = (long) Convert100NanosecondsToMilliseconds(nanoseconds)/1000;
                formatInfo.FormatIdentifier = "MP4";
            }

            return formatInfo;
        }

        public static double Convert100NanosecondsToMilliseconds(double nanoseconds)
        {
            // One million nanoseconds in 1 millisecond, but we are passing in 100ns units...
            return nanoseconds * 0.0001;
        }
    }
}
