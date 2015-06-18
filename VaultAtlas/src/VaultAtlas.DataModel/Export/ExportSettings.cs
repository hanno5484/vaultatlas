using System;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Xml.Xsl;

namespace VaultAtlas.DataModel.Export
{
	public class ExportSettings
	{
		public ExportSettings()
		{
			this.LoadDefaultTransform();
			this.LoadDefaultStylesheet();
		}

		private string listName = "";
		public string ListName 
		{

			get 
			{
				return this.listName;
			}
		}

		private string stylesheet;
		public string Stylesheet 
		{
			get 
			{
				return this.stylesheet;
			}
		}

		private bool useXSLT = true;
		public bool UseXSLT 
		{
			get 
			{
				return this.useXSLT;
			}
			set 
			{
				this.useXSLT = value;
			}
		}

        private bool includeNonPublic = false;
        public bool IncludeNonPublic
        {
            get
            {
                return this.includeNonPublic;
            }
            set
            {
                this.includeNonPublic = value;
            }
        }

		private bool useArtistFriendlyNames = true;
		public bool UseArtistFriendlyNames 
		{
			get 
			{
				return this.useArtistFriendlyNames;
			}
			set 
			{
				this.useArtistFriendlyNames = value;
			}
		}

		private string transformFile;
		private XslTransform transform;
		public XslTransform TransformDocument 
		{
			get 
			{
				return this.transform;
			}
			set 
			{
				this.transform = value;
			}
		}

		public XslCompiledTransform GetTransform() 
		{
            XmlDocument doc = new XmlDocument();
            doc.Load( this.transformFile != null ? new StreamReader( this.transformFile ): new StreamReader( this.GetType().Assembly.GetManifestResourceStream( typeof(ExportSettings), "deftransform.xslt" )));

            XslCompiledTransform transform = new XslCompiledTransform();
            transform.Load(doc);
            return transform;
        }

		public void LoadDefaultTransform() 
		{
			this.transform = new XslTransform();
			XmlTextReader xtr = new XmlTextReader( this.GetType().Assembly.GetManifestResourceStream( typeof(ExportSettings), "deftransform.xslt" ) );
			this.transform.Load(xtr, null, null);
			xtr.Close();
		}

		public void LoadDefaultStylesheet() 
		{
			StreamReader str = new StreamReader( this.GetType().Assembly.GetManifestResourceStream( typeof(ExportSettings), "defstylesheet.css" ) );
			this.stylesheet = str.ReadToEnd();
			str.Close();
		}

		public void LoadTransform( string xsltFile ) 
		{
			this.transformFile = xsltFile;
			this.transform = new XslTransform();
			XmlTextReader xtr = new XmlTextReader( new StreamReader( xsltFile ));
			this.transform.Load(xtr, null, null);
			xtr.Close();
		}

		public void LoadStylesheet( string cssFile ) 
		{
			StreamReader str = new StreamReader( cssFile );
			this.stylesheet = str.ReadToEnd();
			str.Close();
		}
	}
}
