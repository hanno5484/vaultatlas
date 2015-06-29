namespace VaultAtlas.DataModel
{
    public class MediaFormatInfo
    {
        public string FormatIdentifier { get; set; }

        public int BitRate { get; set; }

        public long LengthSeconds { get; set; }

        public int BitsPerSample { get; set; }

        public int SampleRate { get; set; }

        public int NumberChannels { get; set; }
    }
}
