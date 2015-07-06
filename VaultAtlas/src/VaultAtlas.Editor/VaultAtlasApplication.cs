using System.Text.RegularExpressions;
using VaultAtlas.DataModel;
using System;
using System.IO;
using System.Windows.Forms;
using VaultAtlas.Properties;

namespace VaultAtlas
{
	public class VaultAtlasApplication
	{
	    public static Model Model
	    {
	        get { return Model.SingleModel; }
	    }

	    public static MainForm MainForm;

        private static readonly Regex RegexShnId = new Regex(@"\d{5,6}");

	    public static Artist RequestEnterArtist(string nameSuggestion)
		{
		    var newArtist = new Artist(Model.Artists.Table.NewRow()) { DisplayName = nameSuggestion, SortName = nameSuggestion };

            if (new UI.DialogArtist(newArtist).ShowDialog(MainForm) == DialogResult.OK)
            {
                while (Model.Artists.Get(newArtist.SortName) != null)
                {
                    MessageBox.Show(string.Format(resources.ArtistAlreadyExists, newArtist.SortName),
                        Constants.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Hand);

                    if (new UI.DialogArtist(newArtist).ShowDialog(MainForm) != DialogResult.OK)
						return null;
				}

                newArtist.Row.Table.Rows.Add(newArtist.Row);
                Model.Artists.Adapter.Update(Model.Artists.Table);

				return newArtist;
			}
            newArtist.Row.Delete();
			return null;
		}


	    public static Show RequestEnterShow(string artistSortName, ShowDate dateSuggestion, string fileName, Action<Show> newShowModifier = null)
		{
			var newShow = Model.NewShow( artistSortName, dateSuggestion );
			if ( File.Exists( fileName ))
			{
			    var content = FileTool.TolerantInfoFileRead(fileName);
			    if (!string.IsNullOrEmpty(content))
			    {
			        var adapter = newShow.GetResourcesAdapter();
			        var newRow = adapter.Table.NewRow();
			        var res = new Resource(newRow)
			        {
			            Key = Resources.KeyTextFile, Value = content,
			            UidShow = newShow.UID,
			            Uid = Guid.NewGuid().ToString(),
			            ResourceType = "Text"
			        };
			        adapter.Table.Rows.Add(newRow);
			        adapter.Adapter.Update(new[] {res.Row});
			    }
			}

	        if (newShowModifier != null)
	            newShowModifier(newShow);

		    if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
		    {
		        var folderName = Path.GetFileName(Path.GetDirectoryName(fileName));

		        if (!string.IsNullOrEmpty(folderName))
		        {
                    newShow.FolderName = folderName;
		            var match = RegexShnId.Match(newShow.FolderName);
		            if (match.Success)
		            {
		                newShow.SHN = match.Value;
		            }
		        }
		    }

		    MainForm.RequestEditShow( -1, newShow);
			return newShow;
		}
	}
}