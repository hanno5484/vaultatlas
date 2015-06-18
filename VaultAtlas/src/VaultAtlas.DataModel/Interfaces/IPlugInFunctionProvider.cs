using System;

namespace VaultAtlas.DataModel
{
	public interface IPlugInFunctionProvider
	{
		void DeleteShow( Show show );

		void UndoChanges( Show show );

		void RequestClose();

		void SetTabVisible( ITabPage tabPage, bool visible );

        Show GetShow(int index);
	}
}