using System;

namespace VaultAtlas.DataModel
{
	public interface IShowVisualization 
	{
		bool OnSelectedShowChanged( Show currentShow );

		string TabCaption 
		{
			get;
		}

		System.Windows.Forms.Control GenerateControl();
	}
}
