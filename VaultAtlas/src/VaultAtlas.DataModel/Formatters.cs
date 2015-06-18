using System;
using System.Globalization;

namespace VaultAtlas.FlacAtlas
{
	public class Formatters
	{
	    public static string GetTimeString(long seconds)
	    {
	        return Stuff(seconds/60) + ":" + Stuff(seconds%60);
	    }

	    private static string Stuff(long v)
        {
            return v < 10 ? "0" + v : v.ToString();
        }

		static Formatters()
		{
		    (NumberFormatter = (NumberFormatInfo) CultureInfo.CurrentCulture.NumberFormat.Clone()).NumberDecimalDigits = 2;
		}

		private static readonly NumberFormatInfo NumberFormatter;

		private static readonly int Mebibyte = (int) Math.Pow( 2, 20 );
		private static readonly int Kibibyte = (int) Math.Pow( 2, 10 );

	    private const string NumberFormatIdentifier = "n";

	    private const string KibibyteSuffix = " KB";
        private const string MebibyteSuffix = " MB";

	    public static string GetFileSizeString(long size)
	    {
	        double frag;
	        string suffix;

	        if (size < Mebibyte)
	        {
	            frag = size/(double) Kibibyte;
	            suffix = KibibyteSuffix;
	        }
	        else
	        {
	            frag = size/(double) Mebibyte;
	            suffix = MebibyteSuffix;
	        }

	        return frag.ToString(NumberFormatIdentifier, NumberFormatter) + suffix;
	    }

	}
}