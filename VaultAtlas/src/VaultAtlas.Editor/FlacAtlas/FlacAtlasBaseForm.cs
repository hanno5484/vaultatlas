using System;
using System.Collections.Generic;
using System.Text;

namespace VaultAtlas.FlacAtlas
{
    public class FlacAtlasBaseForm : System.Windows.Forms.Form
    {
        static FlacAtlasBaseForm()
        {
            try
            {
                System.IO.Stream s = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(
               "VaultAtlas.FlacAtlas.Resources.FlacAtlas.ico");
                if (s != null)
                    AppIcon = new System.Drawing.Icon(s);
            }
            catch
            {
            }
        }

        private static readonly System.Drawing.Icon AppIcon;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (AppIcon != null)
                Icon = AppIcon;
        }
    }
}
