using System;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace VaultAtlas.DataModel
{
	public class ApplicationConfig
	{
		public static ApplicationConfig GetConfig() 
		{
			if ( config == null )
				config = new ApplicationConfig();
			return config;
		}

		private static ApplicationConfig config;

		private ApplicationConfig()
		{
            var settingsPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
		    var appSettingsPath = Path.Combine(settingsPath, "VaultAtlas");
            if (!Directory.Exists(appSettingsPath))
                Directory.CreateDirectory(appSettingsPath);
		    this.configurationXmlPath = Path.Combine(appSettingsPath, "VaultAtlasConfiguration.xml");
			if ( File.Exists( this.configurationXmlPath )) 
			{
				try 
				{
					configurationXml.Load( this.configurationXmlPath );
				}
				catch 
				{
                    MessageBox.Show("Could not load configuration file. Loading default configuration.", Constants.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private string configurationXmlPath;
		internal XmlDocument configurationXml = new XmlDocument();
		public void Save() 
		{
			try 
			{
				configurationXml.Save( configurationXmlPath );
			} 
			catch 
			{
				MessageBox.Show( "Could not save configuration file.", Constants.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error );
			}
		}

        public string this[ string key, string defaultValue ] 
		{
			get 
			{
				string s = this[ key ];
				return s ?? defaultValue;
			}
		}

		public string this[ string key ] 
		{
			get 
			{
				var node = this.configurationXml.SelectSingleNode("/VaultAtlasConfiguration/AppConfiguration/add[@key='"+key+"']/@value");
				if ( node == null )
					return null;
				return node.Value;
			}
			set 
			{
				var n = this.configurationXml.SelectSingleNode("/VaultAtlasConfiguration");
			    if (n == null)
			    {
			        configurationXml.AppendChild(n = configurationXml.CreateElement("VaultAtlasConfiguration"));
			    }

			    n = n.SelectSingleNode("AppConfiguration");
			    if (n == null)
			    {
			        n = configurationXml.CreateElement("AppConfiguration");
			        configurationXml.DocumentElement.AppendChild(n);
			    } 
				var node = n.SelectSingleNode("add[@key='"+key+"']") as XmlElement;
				if ( node == null ) 
				{
					n.AppendChild( node = n.OwnerDocument.CreateElement("add"));
					node.SetAttribute( "key", key );
				}
				node.SetAttribute( "value", value );
				this.Save();
			}
		}
	}
}
