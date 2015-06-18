using System;
using System.Xml;
using System.Windows.Forms;
using VaultAtlas.DataModel;

namespace VaultAtlas.PlugIn.Archive.WilcoBase
{
	public class ShowEditPlugIn : IShowEditPlugIn
	{
		public ShowEditPlugIn()
		{
		}
		#region IShowEditPlugIn Member

		public ITabPage[] CreateTabPages(System.Windows.Forms.ImageList defaultImageList)
		{
            if (this.editedShow != null)
            {
                string artistName = this.editedShow.ArtistSortName;
                if (artistName.Equals("Wilco"))
                {

                    if (PlugIn.wilcoBaseDoc != null)
                    {
                        XmlNode node = PlugIn.wilcoBaseDoc.SelectSingleNode("/Events/Event[@Date='" + editedShow.Date.ToString() + "']");
                        if (node != null)
                        {
                            this.browser.Navigate("http://www.wilcobase.com/event.php?event_key=" + node.Attributes["Key"].Value, null);
                            return new ITabPage[] { new GenericTabPage("WilcoBase", this.browser, defaultImageList, -1) };
                        }
                    }
                }
            }
			return new ITabPage[0];
		}

		private WebBrowser browser = new WebBrowser();
		private IPlugInFunctionProvider provider;

        public void OnBind(IPlugInFunctionProvider provider, int dataSourceIndex, Show show)
        {
            this.provider = provider;
            this.editedShow = show;
        }


		private Show editedShow;
		public Show EditedShow
		{
			get
			{
				return this.editedShow;
			}
			set
			{
				this.editedShow = value;
			}
		}

		public void OnShowPropertyChanged(string propertyName)
		{
			// TODO:  Implementierung von ShowEditPlugIn.OnShowPropertyChanged hinzufügen
		}

		public void OnShowAcceptChanges()
		{
			// TODO:  Implementierung von ShowEditPlugIn.OnShowAcceptChanges hinzufügen
		}

		public void OnFormClosing(System.ComponentModel.CancelEventArgs args)
		{
			// TODO:  Implementierung von ShowEditPlugIn.OnFormClosing hinzufügen
		}

		public void OnShowRejectChanges()
		{
			// TODO:  Implementierung von ShowEditPlugIn.OnShowRejectChanges hinzufügen
		}

		#endregion
	}
}
