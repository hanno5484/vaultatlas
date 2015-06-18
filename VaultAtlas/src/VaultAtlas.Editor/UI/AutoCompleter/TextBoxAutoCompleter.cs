using System;
using System.Windows.Forms;

namespace VaultAtlas.UI.AutoCompleter
{
	public class TextBoxAutoCompleter
	{

	    private Func<string, string> bestFitDelegate;
		private TextBox textBox;
		private int textLengthBefore = 0;

        public TextBoxAutoCompleter(TextBox target, Func<string, string> bestFitDelegate)
        {
            target.TextChanged += target_TextChanged;
            target.Disposed += target_Disposed;
			this.textBox = target;
			this.bestFitDelegate = bestFitDelegate;
			this.textBox.KeyPress +=textBox_KeyPress;
		}

		private void target_TextChanged(object sender, EventArgs e)
		{
			string enteredString = this.textBox.Text;
			if ( enteredString.Length <= this.textLengthBefore ) 
			{
				this.textLengthBefore = enteredString.Length;
				return;
			}
			string completedString = this.bestFitDelegate( enteredString );
			this.textBox.TextChanged -=new EventHandler(this.target_TextChanged);
			this.textBox.Text = completedString;
			this.textBox.SelectionStart = enteredString.Length;
			if (completedString.Length >= enteredString.Length)
				this.textBox.SelectionLength = completedString.Length - enteredString.Length;
			this.textBox.TextChanged +=new EventHandler(this.target_TextChanged);
			this.textLengthBefore = enteredString.Length;
		}

		private void target_Disposed(object sender, EventArgs e)
		{
			this.textBox.TextChanged -= new EventHandler( this.target_TextChanged );
		}

		private void textBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (this.textBox.SelectionLength + this.textBox.SelectionStart == this.textBox.Text.Length)
				this.textLengthBefore = this.textBox.SelectionStart;
		}
	}
}
