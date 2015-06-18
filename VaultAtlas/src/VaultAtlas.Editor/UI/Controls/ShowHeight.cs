using System;
using System.Collections.Generic;
using System.Text;

namespace VaultAtlas.UI.Controls
{
    internal struct ShowHeight
    {
        internal ShowHeight(int totalHeight, int commentHeight)
        {
            this.totalheight = totalHeight;
            this.commentHeight = commentHeight;
        }

        private int totalheight;
        public int TotalHeight
        {
            get
            {
                return this.totalheight;
            }
        }

        private int commentHeight;
        public int CommentHeight
        {
            get
            {
                return this.commentHeight;
            }
        }
    }
}
