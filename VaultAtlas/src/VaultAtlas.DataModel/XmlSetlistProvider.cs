using System;
using System.Collections;
using System.Data;
using System.Xml;
using System.Xml.XPath;

namespace VaultAtlas.DataModel
{
	public class XmlSetlistProvider : ISetlistProvider
	{
		public XmlSetlistProvider( IPlugInRegister register, XmlDocument doc, string name, string keyAttribute )
		{
			this.register = register;
			this.doc = doc;
			this.name = name;
			this.keyAttribute = keyAttribute;
		}

		private IPlugInRegister register;

		private XmlDocument doc;

		private string keyAttribute;
		public string KeyAttribute
		{
			get
			{
				return this.keyAttribute;
			}
		}

        private System.Windows.Forms.WebBrowser browser = new System.Windows.Forms.WebBrowser();

		public System.Windows.Forms.Control GetControl( object resultItem ) 
		{
			IShowAbstract res = resultItem as IShowAbstract;
			if ( res != null ) 
			{
				if ( this.keyAttribute.Equals("cboDate")) 
				{
					string postData = "cboDate=" + res.Key;                    
					this.browser.Navigate( "http://www.deadlists.com/deadlists/showresults.asp", null, System.Text.Encoding.UTF8.GetBytes(postData), null );
				} 
				else
				{
				}

				return this.browser;
			}
			return new System.Windows.Forms.Panel();
		}

		#region ISetlistProvider Member

		private string name;
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		public System.Collections.IList GetEntriesForYear( string year )
		{
			ArrayList res = new ArrayList();
			XmlNodeList nodes = this.doc.SelectNodes("//Show[@Year='"+year+"']");
			foreach( XmlNode node in nodes ) 
			{
				string venue = node.Attributes["Venue"] != null ? node.Attributes["Venue"].Value : string.Empty;
				string city = node.Attributes["City"] != null ? node.Attributes["City"].Value : string.Empty;
				string artist = node.Attributes["Artist"] != null ? node.Attributes["Artist"].Value : "Grateful Dead";
				res.Add( new ShowAbstract( node.Attributes["Date"].Value,
					node.Attributes[keyAttribute].Value,
					ShowDate.Parse( node.Attributes["Date"].Value ), venue, city, artist ));
			}

			return res;
			
		}

		public System.Collections.IList GetYears()
		{
			ArrayList years = new ArrayList();
			for(int i=1965;i<1996;i++)
				years.Add(i.ToString());
			return years;
		}

		#endregion
	}
}
