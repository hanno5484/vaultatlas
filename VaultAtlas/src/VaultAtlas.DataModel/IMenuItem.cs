using System;

namespace VaultAtlas.DataModel
{

	public enum MenuKey { File, Edit, Data, Tools }

	public interface IMenuItem 
	{
		MenuKey MenuKey 
		{
			get;
		}

		string Caption 
		{
			get;
			set;
		}

		void OnMenuItemClicked( IPlugInEnvironment env );

		void OnMenuItemSelected( IPlugInEnvironment env );
	}

	public interface IPlugInEnvironment 
	{
		Model Model 
		{
			get;
		}
	}
}
