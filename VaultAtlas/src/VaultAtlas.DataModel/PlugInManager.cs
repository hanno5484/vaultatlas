using System;
using System.Windows.Forms;
using System.Xml;
using System.Reflection;
using System.Collections.Generic;
using VaultAtlas.DataModel;

namespace VaultAtlas.DataModel
{
	public class PlugInManager : IPlugInRegister
	{
		internal PlugInManager( Model model )
		{
		    MenuItems = new List<IMenuItem>();
		    Controls = new List<ITabPage>();
		    SetlistProviders = new List<ISetlistProvider>();
		    this.Model = model;
			foreach(XmlNode node in ApplicationConfig.GetConfig().configurationXml.SelectNodes("/VaultAtlasConfiguration/PlugIns/PlugIn")) 
			{
				string assemblyName = node.Attributes["Assembly"].Value;
				string className = node.Attributes["Class"].Value;

                if (System.IO.File.Exists(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, assemblyName+".dll")))
                {
                    try
                    {
                        Assembly assembly = AppDomain.CurrentDomain.Load(assemblyName);
                        object plugInObj = assembly.CreateInstance(className);
                        this.LoadPlugIn(plugInObj as IPlugIn);
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Error while loading plugin " + assemblyName + ": " + exc.Message);
                    }
                }

			}
		}

	    public Model Model { get; private set; }

	    private readonly Dictionary<string, IPlugIn> _plugIns = new Dictionary<string, IPlugIn>();

		public void LoadPlugIn( IPlugIn plugIn ) 
		{
			plugIn.RegisterPlugIns( this );
			this._plugIns[ plugIn.PlugInName ] = plugIn;	

		}

	    public IList<ISetlistProvider> SetlistProviders { get; private set; }

	    internal void LoadPlugIn( string fileName, string className ) 
		{
			Assembly assembly = Assembly.LoadFile( fileName );
			Type plugInType = assembly != null ? assembly.GetType( className ) : null;
			ConstructorInfo ci = plugInType != null ? plugInType.GetConstructor( new Type[]{ typeof(Model)}) : null;
			IPlugIn plugIn = ci != null ? ci.Invoke( new object[]{ this.Model }) as IPlugIn : null;
			if ( plugIn == null )
				throw new Exception("Not a plugin.");
			this.LoadPlugIn( plugIn );
		}

		public IPlugIn GetPlugInByName( string plugInName ) 
		{
            return this._plugIns[plugInName];
		}

		#region IPlugInRegister Member

		private readonly IList<Type> _showEditorTypes = new List<Type>();
		public IList<IShowEditPlugIn> CreateShowEditorPlugIns() 
		{
			var res = new List<IShowEditPlugIn>();
            foreach( var showEditorType in this._showEditorTypes )
				res.Add( (IShowEditPlugIn) (showEditorType.GetConstructor( new Type[]{} ).Invoke( new object[]{} )));

			return res;
		}

		public void RegisterShowEditor(Type showEditorType)
		{
            this._showEditorTypes.Add( showEditorType );			
		}

	    public IList<IMenuItem> MenuItems { get; private set; }

	    public IList<ITabPage> Controls { get; private set; }

	    public void RegisterMenuItem(IMenuItem menuItem)
		{
			this.MenuItems.Add( menuItem );
		}

		private IList<IShowVisualization> showVisulizations = new List<IShowVisualization>();
		public void RegisterShowVisualization(IShowVisualization visualization)
		{
			this.showVisulizations.Add( visualization );
		}

		private IList<IStatProvider> statProviders = new List<IStatProvider>();
		public IList<IStatProvider> StatProviders 
		{
			get 
			{
				return this.statProviders;
			}
		}

		public void RegisterStatProvider(IStatProvider statProvider)
		{
			this.statProviders.Add( statProvider );
		}

		public void RegisterTopLevelTabPage( ITabPage tabPage ) 
		{
			this.Controls.Add( tabPage );
		}

		public void RegisterSetlistProvider( ISetlistProvider provider ) 
		{
			this.SetlistProviders.Add( provider );
		}

		#endregion
	}

	public delegate void PlugInEventHandler( object sender, PlugInEventArgs args );
	public class PlugInEventArgs : EventArgs 
	{
		private IPlugIn plugIn;
		public IPlugIn PlugIn 
		{
			get 
			{
				return this.plugIn;
			}
		}

		internal PlugInEventArgs( IPlugIn plugIn )
		{
			this.plugIn = plugIn;
		}
	}
}
