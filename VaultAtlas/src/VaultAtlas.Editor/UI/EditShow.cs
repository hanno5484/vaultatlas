using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using VaultAtlas.DataModel;

using System.Runtime.Serialization;

namespace VaultAtlas.UI
{
	public class EditShow : UserControl, IPlugInFunctionProvider
	{
		private System.Windows.Forms.ImageList imageList1;
		private TabControl baseTabControl1;
		private TabPage tabPage2;
		private System.Windows.Forms.Panel panel4;
		private System.ComponentModel.IContainer components;

		protected EditShow( SerializationInfo info, StreamingContext context ) : this()
		{
			this.InternalConstruct();
			
		}

		public EditShow()
		{
			this.InternalConstruct();
		}

        public Show GetShow(int index)
        {
            return VaultAtlasApplication.Model.ShowView.GetShow(index);
        }

		public void InternalConstruct() 
		{
			InitializeComponent();

			this.baseTabControl1.TabPages.Add(this.tabPage2);

		
		}

	    public Show EditedShow { get; private set; }

	    public int ShowIndex { get; private set; }

	    public void Bind( int showIndex, Show show) 
		{
            this.ShowIndex = showIndex;
            this.EditedShow = show;

			this.LoadPlugIns();
			Text = EditedShow.Display;

            foreach (var showEdit in this._plugInShowEditors)
            {
                showEdit.OnBind(this, showIndex, show);
            }

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditShow));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.baseTabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            // 
            // baseTabControl1
            // 
            this.baseTabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.baseTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.baseTabControl1.ImageList = this.imageList1;
            this.baseTabControl1.Location = new System.Drawing.Point(0, 0);
            this.baseTabControl1.Name = "baseTabControl1";
            this.baseTabControl1.SelectedIndex = 0;
            this.baseTabControl1.Size = new System.Drawing.Size(482, 310);
            this.baseTabControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel4);
            this.tabPage2.ImageIndex = 0;
            this.tabPage2.Location = new System.Drawing.Point(0, 0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(448, 163);
            this.tabPage2.TabIndex = 4;
            this.tabPage2.Text = "Summary";
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(448, 163);
            this.panel4.TabIndex = 3;
            // 
            // EditShow
            // 
            this.Controls.Add(this.baseTabControl1);
            this.Name = "EditShow";
            this.Size = new System.Drawing.Size(482, 310);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

        private readonly IList<IShowEditPlugIn> _plugInShowEditors = new List<IShowEditPlugIn>();

		private void AddTabPage( ITabPage tabPage, int index ) 
		{
			var newTabPage = new TabPage(tabPage.Title);
            newTabPage.Controls.Add(tabPage.Control);
		    newTabPage.ImageIndex = tabPage.ImageIndex;
			newTabPage.BorderStyle = BorderStyle.FixedSingle;

			this.baseTabControl1.TabPages.Insert( index, newTabPage);
		}

		private void LoadPlugIns()
		{
			this.baseTabControl1.TabPages.Clear();
			this._plugInShowEditors.Clear();
			foreach( var showEdit in VaultAtlasApplication.Model.PlugInManager.CreateShowEditorPlugIns())
			{
				this._plugInShowEditors.Add( showEdit );

				var tabPages = showEdit.CreateTabPages( this.imageList1 );

				if ( tabPages != null ) 
				{
					foreach( ITabPage tabPage in tabPages ) 
					{
						this.AddTabPage( tabPage, ( showEdit is ShowEditData ) ? 0 : this.baseTabControl1.TabPages.Count);
					}
				}
			}
			this.baseTabControl1.SelectedTab = this.baseTabControl1.TabPages[0];
		}

		private void FireRejectChanges() 
		{
			EditedShow.Row.RejectChanges();
			foreach( var plugIn in _plugInShowEditors )
				plugIn.OnShowRejectChanges();
		}


		public void SetTabVisible( ITabPage tabPage, bool visible ) 
		{
			foreach( TabPage tab in baseTabControl1.TabPages ) 
			{
				if ( tab.Controls[0] == tabPage.Control ) 
				{
					if ( !visible)
						this.baseTabControl1.TabPages.Remove( tab );
					return;
				}
			}
			if ( visible )
				this.AddTabPage( tabPage, this.baseTabControl1.TabPages.Count );
		}

		#region IPlugInFunctionProvider Member

        public void RequestClose()
        {
            if (EditedShow.Row.RowState != DataRowState.Deleted)
                ValidateChildren();

            VaultAtlasApplication.MainForm.RequestCloseTab(this);
            var navi = VaultAtlasApplication.Model.ModelNavigator;
            if (navi != null)
                navi.NavigateTo(this.ShowIndex);
        }

		public void UndoChanges(Show show)
		{
			FireRejectChanges();
		}

	    public void DeleteShow(Show show)
	    {
	        if (MessageBox.Show(resources.DeleteThisShow, "", MessageBoxButtons.YesNo) == DialogResult.No)
	            return;

	        RequestClose();

	        EditedShow.Row.Delete();
	    }

	    #endregion
    }
}
