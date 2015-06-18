using System;
using System.Collections;

namespace VaultAtlas.DataModel
{
	public interface IPlugIn
	{
		void RegisterPlugIns( IPlugInRegister register );

		string PlugInName 
		{
			get;
		}
	}

	public interface IPlugInRegister
	{

		void RegisterShowEditor( Type showEditorType );

		void RegisterMenuItem( IMenuItem menuItem );

		void RegisterShowVisualization( IShowVisualization visualization );

		void RegisterSetlistProvider( ISetlistProvider setlistProvider );
	}

	public interface ISetlistProvider 
	{
		IList GetYears();

		IList GetEntriesForYear( string year );

		string Name 
		{
			get;
		}

		System.Windows.Forms.Control GetControl( object setlistItem );
	}
}
