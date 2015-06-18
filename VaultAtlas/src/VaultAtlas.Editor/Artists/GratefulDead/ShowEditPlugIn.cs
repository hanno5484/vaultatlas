using System;
using System.Xml;
using System.Collections;
using System.Windows.Forms;
using VaultAtlas.DataModel;

namespace VaultAtlas.PlugIn.Archive.GratefulDead
{
	public class ShowEditPlugIn : IShowEditPlugIn
	{
		public ShowEditPlugIn()
		{
		}

		#region IShowEditPlugIn Member

		private ITabPage tabPage;

		public ITabPage[] CreateTabPages( System.Windows.Forms.ImageList defaultImageList )
		{
            if (this.editedShow != null)
            {
                string artistName = this.editedShow.ArtistSortName;
                if (artistName.IndexOf("Grateful Dead") != -1)
                {

                    if (PlugIn.deadlistsDoc != null)
                    {
                        XmlNode node = PlugIn.deadlistsDoc.SelectSingleNode("/Deadlists/Show[@Date='" + editedShow.Date.ToString() + "']");
                        if (node != null)
                        {
                            byte[] postData = System.Text.Encoding.UTF8.GetBytes("cboDate=" + node.Attributes["cboDate"].Value);
                            this.browser.Navigate("http://www.deadlists.com/deadlists/showresults.asp", null, postData, null);
                            return new ITabPage[] { new GenericTabPage("Archive", this.browser, defaultImageList, -1) };
                        }
                        else
                        {
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

		public object PlugInData
		{
			get
			{
				// TODO:  Getter-Implementierung f�r ShowBrowser.PlugInData hinzuf�gen
				return null;
			}
			set
			{
				// TODO:  Getter-Implementierung f�r ShowBrowser.PlugInData hinzuf�gen
			}
		}

		private Show editedShow;

		public void OnShowPropertyChanged(string propertyName)
		{
			// TODO:  Implementierung von ShowBrowser.OnShowPropertyChanged hinzuf�gen
		}

		public void OnShowAcceptChanges()
		{
			// TODO:  Implementierung von ShowBrowser.OnShowAcceptChanges hinzuf�gen
		}

		public void OnFormClosing(System.ComponentModel.CancelEventArgs args)
		{
			// TODO:  Implementierung von ShowBrowser.OnFormClosing hinzuf�gen
		}

		public void OnShowRejectChanges()
		{
			// TODO:  Implementierung von ShowBrowser.OnShowRejectChanges hinzuf�gen
		}

		#endregion
	}
}
