using System;
using VaultAtlas.DataModel;

namespace VaultAtlas.UI
{
	public class BuiltInPlugIn : IPlugIn
	{
		public BuiltInPlugIn()
		{
		}
		#region IPlugIn Member

		public void RegisterPlugIns(IPlugInRegister register)
		{
			register.RegisterShowEditor( typeof( ShowEditData ));
		}

		public string PlugInName
		{
			get
			{
				return "Built-in";
			}
		}

		#endregion
	}
}
