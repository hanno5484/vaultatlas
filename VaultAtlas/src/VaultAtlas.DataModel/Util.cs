using System.Xml;
using System.IO;
using System.Security.Cryptography;

namespace VaultAtlas
{
	public class Util
	{
		public static string MakeSelectSafe( string statementFragment ) 
		{
			return statementFragment.Replace("'","''");
		}

		public static string ConvertText( string input ) 
		{
            string s1 = "\r\r\n"; // 13 13 10
            string s2 = "\r\n"; // 13 10
            string s3 = input;
            while( s3.IndexOf(s1) != -1 )
                s3 = s3.Replace(s1,s2);

            int startSearch = 0;
            int index = 0;
            while ((index = s3.IndexOf("\n", startSearch)) != -1)
            {
                if (index > 0 && s3[index - 1] != '\r')
                {
                    s3 = s3.Substring(0, index) + "\r\n" + s3.Substring(index + 1);
                    index++;
                }
                startSearch = index+1;
            }

            return s3;
		}

		public static string GetDocumentHash( XmlDocument doc ) 
		{
			return GetDocumentHash( doc.OuterXml );
		}

		public static string GetDocumentHash( string doc ) 
		{
			MD5 md5 = MD5.Create();
			md5.Initialize();
			MemoryStream sr = new MemoryStream( System.Text.Encoding.Default.GetBytes( doc) );
			sr.Position = 0;
			string hash = System.Text.Encoding.UTF8.GetString( md5.ComputeHash( sr ) );
			sr.Close();
			return hash;
		}



	}
}
