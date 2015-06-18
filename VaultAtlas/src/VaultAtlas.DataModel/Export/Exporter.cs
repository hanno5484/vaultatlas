using System;
using System.Collections;
using System.IO;
using System.Xml.Xsl;
using System.Windows.Forms;
using System.Data;
using System.Xml;
using VaultAtlas.DataModel.sqlite;

namespace VaultAtlas.DataModel.Export
{
	/// <summary>
	/// Summary description for Exporter.
	/// </summary>
	public class Exporter
	{
		public Exporter( ExportSettings settings )
		{
			Settings = settings;
		}

	    public ExportSettings Settings { get; private set; }


	    public string GenerateDocument( DataView view, ArtistsAdapter artists )
		{
            string dateFormat = Model.ApplicationModel["DateFormat"];

			XmlDocument result = new XmlDocument();
			XmlDeclaration declaration = result.CreateXmlDeclaration( "1.0", "UTF-8", "");
			result.AppendChild( declaration );

			XmlElement VaultAtlasRoot = result.CreateElement("VaultAtlas");

			foreach( DataRowView rowView in view ) 
			{
				XmlElement showElement = result.CreateElement("Shows");
				foreach( DataColumn dc in view.Table.Columns ) 
				{
					string propertyValue = rowView[ dc.ColumnName ].ToString();
					if ( propertyValue.Length > 1 ) 
					{
						XmlElement propertyElement = result.CreateElement( dc.ColumnName );
						propertyElement.InnerText = propertyValue;
						showElement.AppendChild( propertyElement );
					}
				}

                XmlElement dateDisplay = result.CreateElement( "DateDisplay" );
                dateDisplay.InnerText = ShowDate.Format( dateFormat, ShowDate.Parse( rowView["Date"].ToString()));
                showElement.AppendChild(dateDisplay);

                VaultAtlasRoot.AppendChild( showElement );
			}

			result.AppendChild( VaultAtlasRoot );

			string rowFilter = view.RowFilter;
		
			XmlDocument settingsDoc = new XmlDocument();
			settingsDoc.LoadXml( "<Settings><Name>Stylesheet</Name><Value></Value></Settings>" );
			XmlNode settingsNode = result.ImportNode( settingsDoc.DocumentElement, true );
			result.DocumentElement.AppendChild( settingsNode );
			settingsNode.SelectSingleNode("Value").InnerText = this.Settings.Stylesheet;

			// ANFANG Artist-Helper-table erstellen

			DataTable dtIncludedArtists = new DataTable("IncludedArtists");
			dtIncludedArtists.Columns.Add("DisplayName");
			dtIncludedArtists.Columns.Add("ShowCount");
			
			Hashtable arrList = new Hashtable();

			int artistColumnIndex = view.Table.Columns.IndexOf("Artist");
			
			foreach(DataRowView drv in view) 
			{
				string artist = drv[ artistColumnIndex ].ToString();

				if( arrList[ artist ] == null )
					arrList[ artist ] = 1;
				else
					arrList[ artist ] = ((int)arrList[ artist ])+1;
			}

			string[] artistNames = new string[ arrList.Keys.Count ];
			arrList.Keys.CopyTo( artistNames, 0 );
			Array.Sort( artistNames );

			foreach( string key in artistNames ) 
				dtIncludedArtists.Rows.Add( new object[]{
                    ( this.Settings.UseArtistFriendlyNames )
                    ? artists.GetDisplayNameForSortName( key )
                    : key, arrList[ key ] } );

			DataSet includedArtistsDataSet = new DataSet();
			includedArtistsDataSet.DataSetName = "IncludedArtistsSet";
			includedArtistsDataSet.Tables.Add( dtIncludedArtists );
			XmlDocument includedArtistsDoc = new XmlDocument();
			includedArtistsDoc.LoadXml( includedArtistsDataSet.GetXml() );
			XmlNode artistsNode = result.ImportNode( includedArtistsDoc.DocumentElement, true );
			result.DocumentElement.AppendChild( artistsNode );

			// ENDE Artist-Helper-table erstellen 

			System.Collections.Hashtable hash = new System.Collections.Hashtable();
			
			if (!rowFilter.Equals( string.Empty ))
			{
				foreach( DataRowView drv in view )
					hash[ drv.Row["UID"] ] = drv.Row;

				XmlNode VaultAtlasNode = result.SelectSingleNode("VaultAtlas");
				
				XmlNodeList nodeList = VaultAtlasNode.ChildNodes;
				for(int i=0;i<nodeList.Count;i++) 
				{
					XmlNode node = nodeList[i];
					if ( (node.Name.Equals("Shows") && (!hash.ContainsKey( node.SelectSingleNode("UID").InnerText )))
						|| (node.Name.Equals("Artists")))
					{
						VaultAtlasNode.RemoveChild( node );
						i--;
					}
				}
			}

			if (this.Settings.UseArtistFriendlyNames) 
				foreach(XmlNode node in result.SelectNodes("VaultAtlas/Shows/Artist")) 
				{
					XmlElement artistDisplay = result.CreateElement("ArtistDisplay");
					artistDisplay.InnerText = artists.GetDisplayNameForSortName( node.InnerText );
					node.ParentNode.AppendChild( artistDisplay );
				}

            // result.Save(@"c:\theExport.xml");

			if (this.Settings.UseXSLT) 
			{
                XslCompiledTransform transformDoc = this.Settings.GetTransform();

                MemoryStream sw = new MemoryStream();
                XmlTextWriter xtw = new XmlTextWriter(sw, System.Text.Encoding.UTF8);
				transformDoc.Transform( result, xtw );

			    sw.Position = 0;
			    string res = System.Text.Encoding.UTF8.GetString( sw.GetBuffer() );
                xtw.Close();
                return res;
			
				
			}
			else
				return result.OuterXml;
		}

		private static XmlNode[] CopyToArray( XmlNodeList nodes ) 
		{
			XmlNode[] res = new XmlNode[ nodes.Count ];
			for(int i=0;i<res.Length;i++)
				res[i] = nodes[i];
			return res;
		}
	}
}
