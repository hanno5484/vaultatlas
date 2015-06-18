using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace VaultAtlas.UI.AutoCompleter
{
	public class ComboBoxAutoCompleter
	{

		private readonly Func<string, IEnumerable<string>> bestFitDelegate;
		private readonly ComboBox _comboBox;
	    private readonly ComboBox _source;

	    public ComboBoxAutoCompleter(ComboBox target, ComboBox source, Func<string, IEnumerable<string>> bestFitDelegate)
		{
			this._comboBox = target;
			this._source = source;
			this.bestFitDelegate = bestFitDelegate;

            this._source.Disposed += target_Disposed;
            this._source.TextChanged += source_TextChanged;

			this.FillComboBox();
		}

		private void target_Disposed(object sender, EventArgs e)
		{
			this._source.TextChanged -= new EventHandler( this.source_TextChanged );
		}

		private void source_TextChanged(object sender, EventArgs e)
		{
			this.FillComboBox();
		}

		private void FillComboBox() 
		{
			this._comboBox.BeginUpdate();
			this._comboBox.Items.Clear();
		    this._comboBox.Items.AddRange(this.bestFitDelegate(this._source.Text).ToArray());
			this._comboBox.EndUpdate();
			
		}

	}
}
