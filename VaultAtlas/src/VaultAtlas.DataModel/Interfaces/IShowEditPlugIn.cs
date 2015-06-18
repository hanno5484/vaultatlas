using System;
using System.Collections;

namespace VaultAtlas.DataModel
{
	public interface IShowEditPlugIn
	{
		/// <summary>
		/// Call to create tab pages managed by a plug in.
		/// </summary>
		/// <returns></returns>
		ITabPage[] CreateTabPages( System.Windows.Forms.ImageList defaultImageList );

		/// <summary>
		/// Re-bind the controls to the data content. Called by the application after Undo.
		/// </summary>
		void OnBind( IPlugInFunctionProvider provider, int dataSourceIndex, Show show );

		void OnShowPropertyChanged( string propertyName );

		void OnShowRejectChanges();

		void OnShowAcceptChanges();

		void OnFormClosing( System.ComponentModel.CancelEventArgs args );

	}
}
