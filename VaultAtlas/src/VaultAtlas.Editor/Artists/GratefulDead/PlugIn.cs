using System;
using System.Collections;
using System.Xml;
using VaultAtlas.DataModel;
using System.Reflection;

namespace VaultAtlas.PlugIn.Archive.GratefulDead
{
	public class PlugIn : IPlugIn
	{
		public PlugIn()
		{
			deadlistsDoc.Load( Assembly.GetExecutingAssembly().GetManifestResourceStream("VaultAtlas.PlugIn.Archive.GratefulDead.Deadlists.xml"));
			jerrysiteDoc.Load( Assembly.GetExecutingAssembly().GetManifestResourceStream("VaultAtlas.PlugIn.Archive.GratefulDead.Jerrysite.xml"));

		}

		private XmlSetlistProvider deadlistsProvider;
		private XmlSetlistProvider jerrysiteProvider;

		#region IPlugIn Member

		private IPlugInRegister register;

		public void RegisterPlugIns(IPlugInRegister register)
		{
			this.register = register;

			this.deadlistsProvider = new XmlSetlistProvider( register, deadlistsDoc, "Grateful Dead", "cboDate" );
			this.jerrysiteProvider = new XmlSetlistProvider( register, jerrysiteDoc, "Jerry Garcia", "Key" );
			
			register.RegisterShowEditor( typeof( ShowEditPlugIn ));

			register.RegisterSetlistProvider( this.deadlistsProvider );
			register.RegisterSetlistProvider( this.jerrysiteProvider );
		}

		public string PlugInName
		{
			get
			{
				return "Grateful Dead Archive & Setlists";
			}
		}


		internal static XmlDocument deadlistsDoc = new XmlDocument();

		internal static XmlDocument jerrysiteDoc = new XmlDocument();

		#endregion
	}

}
