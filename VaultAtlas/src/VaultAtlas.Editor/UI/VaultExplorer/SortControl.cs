using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace VaultAtlas.UI.VaultExplorer
{
	public class SortControl : System.Windows.Forms.UserControl
	{

		static SortControl() 
		{
			sortExpressions = new SortExpression[]{
													  new SortExpression( "Artist, Date", "Artist, Date" ),
													  new SortExpression( "Date", "Date" ),
													  new SortExpression( "City, Venue, Date", "City, Venue, Date" )
			};
		}

		private static SortExpression[] sortExpressions;

		private System.Windows.Forms.ComboBox comboBox1;
		private System.ComponentModel.Container components = null;

		public SortControl()
		{
			InitializeComponent();
			
			this.comboBox1.DataSource = sortExpressions;
			this.comboBox1.ValueMember = "Expression";
			this.comboBox1.DisplayMember = "DisplayName";
			this.comboBox1.SelectedIndexChanged +=new EventHandler(comboBox1_Validated);
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// comboBox1
			// 
			this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.Location = new System.Drawing.Point(16, 8);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(208, 21);
			this.comboBox1.TabIndex = 0;
			// 
			// SortControl
			// 
			this.Controls.Add(this.comboBox1);
			this.Name = "SortControl";
			this.Size = new System.Drawing.Size(240, 40);
			this.ResumeLayout(false);

		}
		#endregion

		class SortExpression 
		{
			private string displayName;
			public string DisplayName 
			{
				get 
				{
					return this.displayName;
				}
			}

			private string expression;
			public string Expression 
			{
				get 
				{
					return this.expression;
				}
			}

			internal SortExpression(string displayName, string expression) 
			{
				this.displayName = displayName;
				this.expression = expression;
			}
		}

		private void comboBox1_Validated(object sender, EventArgs e)
		{
			if (this.comboBox1.SelectedIndex != -1)
				VaultAtlasApplication.Model.ShowView.Sort = (string) this.comboBox1.SelectedValue;

		}
	}
}
