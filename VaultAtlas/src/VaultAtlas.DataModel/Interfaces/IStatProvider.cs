using System;

namespace VaultAtlas.DataModel
{
	public interface IStatProvider
	{
		System.Windows.Forms.ColumnHeader[] GetColumnHeaders( Model model );

		System.Collections.ICollection GetValueCollection( Model model, System.Windows.Forms.ColumnHeader[] headers );

		string Name 
		{
			get;
		}
	}
}
