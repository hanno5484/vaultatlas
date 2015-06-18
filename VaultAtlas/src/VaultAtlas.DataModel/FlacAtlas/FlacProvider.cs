using System;
using System.Collections.Generic;
using System.IO;

namespace VaultAtlas.FlacAtlas
{
	public class FlacProvider : IFileMetaInfoProvider
	{
	    public const string KeyLengthSeconds = "LengthSeconds";
	    public const string KeySampleRate = "SampleRate";
        public const string KeyBitsPerSample = "BitsPerSample";
	    public const string KeyNumberChannels = "NumberChannels";

        private string GetByteRepresentation(long b)
        {
            var build = new System.Text.StringBuilder();
            build.Append("value: " + b + "\n");
            long maxUlong = 256 * 256 * 256 * 64;
            for (long i = maxUlong; i > 0; i /= 2)
            {
                build.Append(((b & i) != 0) ? "1" : "0");
            }
            return build.ToString();
        }


        public enum FLAC_BLOCK_TYPE
        {
            StreamInfo = 0,
            Padding = 1,
            Application = 2,
            Seektable = 3,
            VorbisComment = 4,
            Cuesheet = 5
        }

        #region IFileMetaInfoProvider Members

        public IDictionary<string,object> GetMetaInfo(string file)
        {
           var res = new Dictionary<string, object>();

           using (var reader = new BinaryReader(new FileStream( file, FileMode.Open, FileAccess.Read, FileShare.Read)))
           {
                byte[] firstBytes = reader.ReadBytes(4);
                if ((System.Text.UTF8Encoding.UTF8.GetString(firstBytes)) != "fLaC")
                {
                    System.Windows.Forms.MessageBox.Show("Warning: " + file + " is not a valid FLAC file.");
                    // TODO mark file
                    return res;
                }

                reader.BaseStream.Position = 4;

                bool isLast = false;
                while (!isLast)
                {
                    byte metadatablockHeader = reader.ReadByte();
                    var blockType = (FLAC_BLOCK_TYPE)(metadatablockHeader & 7);
                    isLast = (metadatablockHeader & 128) != 0;
                    int blockSize = reader.ReadByte() * (256 * 256)
                        + reader.ReadByte() * 256
                        + reader.ReadByte();

                    long continuePosition = reader.BaseStream.Position + blockSize;

                    if (blockType == FLAC_BLOCK_TYPE.StreamInfo)
                    {
                        reader.BaseStream.Position += 10;
                        byte firstByte = reader.ReadByte();
                        byte secondByte = reader.ReadByte();
                        byte thirdByte = reader.ReadByte();
                        byte fourthByte = reader.ReadByte();
                        int numberChannels = ((thirdByte >> 1) & 7) + 1;
                        int bitsPerSample = ((fourthByte >> 4) + ((thirdByte & 1) << 4)) + 1;
                        long sampleCount = reader.ReadByte() * (256 * 256 * 256)
                            + reader.ReadByte() * (256 * 256)
                            + reader.ReadByte() * 256
                            + reader.ReadByte();
                        sampleCount += ((fourthByte & 15) << 32);
                        int sampleRate = ((firstByte * 256 * 256)
                            + (secondByte * 256)
                            + thirdByte) >> 4;
                        
                        var seconds = (int)(sampleCount / sampleRate);
                        res[KeyLengthSeconds] = seconds;
                        res[KeySampleRate] = sampleRate;
                        res[KeyBitsPerSample] = bitsPerSample;
                        res[KeyNumberChannels] = numberChannels;
                    }

                    reader.BaseStream.Position = continuePosition;

                }
            }

            return res;
        }

        public long GetLengthSeconds(string file)
        {
            var metainfo = GetMetaInfo(file);
            if (metainfo.ContainsKey(KeyLengthSeconds))
                return (int) metainfo[KeyLengthSeconds];
            return 0;
        }

        #endregion

    }
}
