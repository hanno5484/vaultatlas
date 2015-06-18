using System;
using System.IO;

namespace VaultAtlas
{
	public class FileTool
	{
	    public static string TolerantInfoFileRead(string file)
	    {
	        try
	        {
	            if (!File.Exists(file))
	                return "";
	            using (var sr = new StreamReader(file, System.Text.Encoding.Default))
	            {
	                var s = sr.ReadToEnd();
	                sr.Close();
	                return s;
	            }
	        }
	        catch
	        {
	            return null;
	        }
	    }
	}
}
