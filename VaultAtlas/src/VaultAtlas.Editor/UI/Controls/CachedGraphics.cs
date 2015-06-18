using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace VaultAtlas.UI.Controls
{
    public class CachedGraphics : IDisposable
    {
        public CachedGraphics(Bitmap b)
        {
            this.thisShowBitmap = b;
            this.intptr = this.thisShowBitmap.GetHbitmap();
            this.thisShowImage = Image.FromHbitmap(intptr);
            this.graphics = Graphics.FromImage(thisShowImage);

        }

        private IntPtr intptr;

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        private Graphics graphics;
        public Graphics GetGraphics()
        {
            return this.graphics;
        }

        private Image thisShowImage;
        private Bitmap thisShowBitmap;

        public Image GetImage()
        {
            return this.thisShowImage;
        }

        #region IDisposable Members

        public void Dispose()
        {
            this.graphics.Dispose();
            this.thisShowImage.Dispose();
            this.thisShowBitmap.Dispose();
            this.graphics = null;
            this.thisShowBitmap = null;
            this.thisShowImage = null;
            DeleteObject(this.intptr);
        }

        #endregion


    }
}
