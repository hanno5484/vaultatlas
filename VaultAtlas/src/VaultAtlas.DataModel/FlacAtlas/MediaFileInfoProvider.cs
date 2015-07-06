using System.Collections.Generic;
using System.IO;
using System.Linq;
using VaultAtlas.DataModel;
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

        public void ApplyToShow(Show show)
        {
            var p = new FlacProvider();

            var infos = GetAllFiles(_directory, "*.flac").Select(p.GetMediaFormatInfo).ToList();

            if (!infos.Any())
            {
                infos = GetAllFiles(_directory, "*.mp3", "*.mp2").Select(fi => new MpegProvider().GetMediaFormatInfo(fi)).ToList();
            }

            var formatInfo = GetAggregateFormatInfo(infos);

            if (formatInfo.LengthSeconds > 0)
                show.LengthRaw = (formatInfo.LengthSeconds/60) + "";

            show.BitRate = formatInfo.BitRate;
            show.SampleRate = formatInfo.SampleRate;
            show.Bps = formatInfo.BitsPerSample;
            show.NrChannels = formatInfo.NumberChannels;
            show.FormatIdentifier = formatInfo.FormatIdentifier;
        }

        private static IEnumerable<string> GetAllFiles(string directory, params string[] patterns)
        {
            return patterns.SelectMany(p => Directory.GetFiles(directory, p, SearchOption.AllDirectories)).Distinct();
        }

        private static MediaFormatInfo GetAggregateFormatInfo(IEnumerable<MediaFormatInfo> formatInfos)
        {
            return new MediaFormatInfo
            {
                SampleRate = formatInfos.Max(f => f.SampleRate),
                BitsPerSample = formatInfos.Max(f => f.BitsPerSample),
                NumberChannels = formatInfos.Max(f => f.NumberChannels),
                LengthSeconds = formatInfos.Sum(f => f.LengthSeconds),
                FormatIdentifier = formatInfos.Select(f => f.FormatIdentifier).FirstOrDefault(f => f != null)
            };
        }
    }
}
