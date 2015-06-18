using System.Windows.Forms;
using System;
using System.IO;
using System.Xml;
using System.Reflection;
using VaultAtlas.DataModel;

namespace VaultAtlas.PlugIn.Archive.WilcoBase
{
	public class PlugIn : IPlugIn
	{
		public PlugIn()
		{
			string configurationXmlPath = Path.Combine( AppDomain.CurrentDomain.BaseDirectory, "WilcoBaseEvents.xml" );
			if ( File.Exists( configurationXmlPath )) 
			{
				try 
				{
					wilcoBaseDoc.Load( configurationXmlPath );
				}
				catch 
				{
					MessageBox.Show( "Could not load WilcoBase file. Loading default WilcoBase instead.", "VaultAtlas", MessageBoxButtons.OK, MessageBoxIcon.Error );
				}
			}
			else 
			{
				LoadDefaultConfigurationXml();
			}

			string hash = this.GetLocalHashCode();

		}

		private void LoadDefaultConfigurationXml() 
		{
			wilcoBaseDoc.Load( Assembly.GetExecutingAssembly().GetManifestResourceStream("VaultAtlas.PlugIn.Archive.WilcoBase.events.xml"));
		}

		#region IPlugIn Member

		public void RegisterPlugIns(IPlugInRegister register)
		{
			register.RegisterShowEditor( typeof( ShowEditPlugIn ));
		}

		public string PlugInName
		{
			get
			{
				return "WilcoBase";
			}
		}

		public string GetLocalHashCode( ) 
		{
			return VaultAtlas.Util.GetDocumentHash( wilcoBaseDoc );
		}

		internal static XmlDocument wilcoBaseDoc = new XmlDocument();

		#endregion
	}
}
