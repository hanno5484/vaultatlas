using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using VaultAtlas.DataModel;

namespace VaultAtlas.UI.Controls
{
	public class ShowList : System.Windows.Forms.Panel, IModelNavigator
	{
        public ShowList()
        {
            // Dieser Aufruf ist für den Windows Form-Designer erforderlich.
            InitializeComponent();

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.Selectable, true);

            this.Font = new Font("Tahoma", 8);
            this.ShowList_FontChanged(this, EventArgs.Empty);

            this.MouseDown += new MouseEventHandler(ShowList_MouseDown);
            this.Scroll += new ScrollEventHandler(ShowList_Scroll);
        }


		#region public writable properties
		
		private int commentMargin = 5;
		public int CommentMargin 
		{
			get 
			{
				return this.commentMargin;
			}
			set 
			{
				this.commentMargin = value;
			}
		}

				
		private bool showResources = false;
		public bool ShowResources 
		{
			get 
			{
				return this.ShowResources;
			}
			set 
			{
				if ( value )
					throw new NotImplementedException();
			}
		}

		private bool showComments = true;
		public bool ShowComments 
		{
			get 
			{
				return this.showComments;
			}
			set 
			{
				this.showComments = value;
			}
		}

		private int interColumnMargin = 10;
		public int InterColumnMargin 
		{
			get 
			{
				return this.interColumnMargin;
			}
			set 
			{
				this.interColumnMargin = value;
			}
		}

		#endregion

		private string[] columnNames = new string[]{ "Status", "Artist", "Date", "City", "Venue", "Quality", "Length", "Source" };

        public IList<string> ColumnNames
        {
            get
            {
                IList<string> l = new List<string>( columnNames );
                return l;
            }
        }
		private System.ComponentModel.Container components = null;

		private IDictionary<int,int> lastCalculatedHeight = new Dictionary<int,int>();

        private Brush selectedBrush = new System.Drawing.SolidBrush(Color.FromArgb(230, 230, 230));

        private VaultAtlas.DataModel.CustomDataView dataSource;
        public VaultAtlas.DataModel.CustomDataView DataSource 
		{
			get 
			{
				return this.dataSource;
			}
			set 
			{
				if ( this.dataSource != value ) 
				{
					if ( this.dataSource != null ) 
					{
						this.dataSource.ListChanged -= new ListChangedEventHandler( this.dataSource_ListChanged );
                        this.dataSource.Table.RowChanged -= new DataRowChangeEventHandler(Table_RowChanged);
					}
					this.dataSource = value;
					if ( this.dataSource != null ) 
					{
						this.dataSource.ListChanged +=new ListChangedEventHandler( this.dataSource_ListChanged );
                        this.dataSource.Table.RowChanged += new DataRowChangeEventHandler(Table_RowChanged);
					}

                    if (this.ColumnHeaderChange != null)
                        this.ColumnHeaderChange(this, EventArgs.Empty);
				}
			}
		}

        private void Table_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            if (this.cachedRowGraphics.ContainsKey(e.Row))
            {
                int index = this.dataSource.GetShowRowIndex( e.Row);
                if (index != -1)
                    this.DisposeFromCache(e.Row);
            }
        }

        void ShowList_Scroll(object sender, ScrollEventArgs e)
        {
            /*
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                switch (e.Type)
                {
                    case ScrollEventType.SmallIncrement:
                        this.UpdateFirstElementIndex(this.firstElementIndex + 1, true);
                        this.SelectFirstElementIfNotVisible(true);
                        break;
                    case ScrollEventType.SmallDecrement:
                        this.UpdateFirstElementIndex(this.firstElementIndex + (this.firstElementIndex - this.lastVisibleElement), true);
                        this.SelectFirstElementIfNotVisible(true);
                        break;
                    case ScrollEventType.First:
                        this.UpdateFirstElementIndex(0, true);
                        this.SelectFirstElementIfNotVisible(true);
                        break;
                    case ScrollEventType.Last:
                        this.UpdateFirstElementIndex(this.dataSource.Count, true);
                        this.SelectFirstElementIfNotVisible(true);
                        break;
                    case ScrollEventType.LargeIncrement:
                        this.UpdateFirstElementIndex(this.lastVisibleElement, true);
                        this.SelectFirstElementIfNotVisible(true);
                        break;
                    case ScrollEventType.LargeDecrement:
                        this.UpdateFirstElementIndex(this.firstElementIndex - (this.firstElementIndex - this.firstElementIndex), true);
                        this.SelectFirstElementIfNotVisible(true);
                        break;
                }

                this.AutoScrollPosition = new Point(this.ClientRectangle.Left, this.firstElementIndex * 25);
            }
            this.Refresh();
            */
        }

        public event EventHandler ColumnHeaderChange;

        public int GetColumnWidth(string columnName)
        {
            DataColumn dc = this.dataSource != null ? this.dataSource.Table.Columns[ columnName ] : null;
            if (dc != null && dc.ExtendedProperties["Width"] != null)
                return Int32.Parse(dc.ExtendedProperties["Width"].ToString());
            else
                return 100;
        }

        public void SetColumnWidth(string columnName, int width)
        {
            DataColumn dc = this.dataSource != null ? this.dataSource.Table.Columns[columnName] : null;
            if (dc != null)
                dc.ExtendedProperties["Width"] = width;
            else
                this.dataSource.Table.ExtendedProperties["Width" + columnName] = width;
            this.lastColumnWidths = null;
            this.DisposeFromCache();
            this.Invalidate();
        }

        private int[] lastColumnWidths;

		private int[] GetColumnWidths() 
		{
			int[] columnWidths = new int[ columnNames.Length ];
			for( int columnIndex=0;columnIndex < columnNames.Length; columnIndex++) 
			{
				DataColumn dc = this.dataSource.Table.Columns[ this.columnNames[ columnIndex ]];
                if (dc == null)
                {
                    string key = "Width"+columnNames[columnIndex];
                    if (this.dataSource.Table.ExtendedProperties[key] != null)
                        columnWidths[columnIndex] = Int32.Parse(this.dataSource.Table.ExtendedProperties[key].ToString());
                    else
                        columnWidths[columnIndex] = 100;
                }
                else
                {
                    if (dc.ExtendedProperties["Width"] != null)
                        columnWidths[columnIndex] = Int32.Parse(dc.ExtendedProperties["Width"].ToString());
                    else
                        columnWidths[columnIndex] = 100;
                }
			}
			return columnWidths;
		}
		
		private Font boldFont, commentFont;

		/// <summary> 
		/// Die verwendeten Ressourcen bereinigen.
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

		#region Vom Komponenten-Designer generierter Code
		/// <summary> 
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
            this.SuspendLayout();
            // 
            // ShowList
            // 
            this.AutoScroll = true;
            this.Name = "ShowList";
            this.Size = new System.Drawing.Size(232, 152);
            this.ResumeLayout(false);

		}
		#endregion

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            if (keyData == Keys.Down)
            {
                this.CurrentRow = this.currentRow + 1;
                return true;
            }
            else if (keyData == Keys.PageDown)
            {
                this.CurrentRow = this.lastVisibleElement;
                return true;
            }
            else if (keyData == Keys.PageUp)
            {
                this.CurrentRow -= (this.lastVisibleElement - this.firstElementIndex) / 2;
                return true;
            }
            else if (keyData == Keys.Up)
            {
                this.CurrentRow = this.currentRow - 1;
                return true;
            }
            else if (keyData == Keys.Enter)
            {
                this.ItemClicked(this, new ShowEventArgs(this.currentRow, this.dataSource[this.currentRow].Row));
                return true;
            }
            /*            else if (keyData == Keys.Delete)
                        {
                
                        }
              */
            return base.ProcessCmdKey(ref msg, keyData);
        }

		protected override void OnDoubleClick(EventArgs e)
		{
			base.OnDoubleClick (e);

			int selIndex = this.GetItemAtPosition( MousePosition );

			if ( selIndex != -1 && this.ItemClicked != null )
				this.ItemClicked( this, new ShowEventArgs( selIndex, this.dataSource[ selIndex ].Row ));
		}

		public int GetItemAtPosition( Point position ) 
		{
            Point myPosition = PointToClient( position );
			int x = myPosition.X;
			int y = myPosition.Y;

            int currentY = firstElementOffset - Math.Abs(this.AutoScrollPosition.Y);
			int currentElement = this.firstElementIndex;
			int selIndex = 0;

			while( currentY < this.ClientRectangle.Height ) 
			{
                if (currentY < y)
                {
                    selIndex = currentElement;
                }
                else
                    break;

				if ( currentElement < this.MaxElementCount )
				{
                    if (!this.lastCalculatedHeight.ContainsKey(currentElement))
                        return -1;
                    int lastCalcHeight =  this.lastCalculatedHeight[ currentElement ];
					currentY += lastCalcHeight;
				}
				if ( currentElement > this.MaxElementCount )
					break;
				currentElement++;

			}

			if ( selIndex > this.DataSource.Count -1 )
				return -1;

			return selIndex;
		}

		public event ShowEvent ItemClicked;

		private int MaxElementCount 
		{
			get 
			{
				return this.dataSource.Count;
			}
		}

		private int marginBottom = 4;
		public int MarginBottom 
		{
			get 
			{
				return this.marginBottom;
			}
		}

		private ShowHeight GetElemHeight( Graphics g, int index, int maxWidth )
		{
			int height = 15;
            int commentHeight = 0;

			if ( this.showComments ) 
			{
                height += (commentHeight = this.GetCommentHeight(g, index, maxWidth));
			}

			if ( this.showResources ) 
			{
				DataRow[] resources = this.dataSource[ index ].Row.GetChildRows( "Resource2Show" );
				if ( resources.Length != 0 ) 
				{
					height += 15 + commentMargin;
				}
			}

            height += marginBottom;

			this.lastCalculatedHeight[ index ] = height;
			return new ShowHeight(height, commentHeight);
		}

		private int GetCommentHeight( Graphics g, int index, int maxWidth ) 
		{
			object obj = this.dataSource[ index ].Row["Comments"];
			if ( obj != null && !(obj is DBNull) && !obj.Equals( string.Empty )) 
			{
				SizeF size = g.MeasureString( obj.ToString(), this.commentFont, maxWidth );
				return (int) size.Height + commentMargin;
			}
			return 0;
		}

        private Brush blackBrush = new SolidBrush(Color.Black);
        private Brush whiteBrush = new SolidBrush(Color.White);
        private Brush redBrush = new SolidBrush(Color.Red);
        private Pen gridLinePen = new Pen(Color.DarkKhaki, 1);

        private Dictionary<DataRow, CachedGraphics> cachedRowGraphics = new Dictionary<DataRow, CachedGraphics>();

        private void DisposeFromCache()
        {
            foreach (CachedGraphics cg in this.cachedRowGraphics.Values)
                cg.Dispose();
            this.cachedRowGraphics.Clear();
        }

        private void DisposeFromCache(DataRow row)
        {
            if (this.cachedRowGraphics.ContainsKey(row))
            {
                this.cachedRowGraphics[row].Dispose();
                this.cachedRowGraphics[row] = null;
                this.cachedRowGraphics.Remove(row);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Random rnd = new Random();
            if (this.cachedRowGraphics.Keys.Count > 300) // speicher sparen
                this.DisposeFromCache();

            Rectangle rect = e.ClipRectangle;
            this.lastCalculatedHeight.Clear();
            string dateFormat = VaultAtlasApplication.Model["DateFormat"];

            e.Graphics.FillRectangle(whiteBrush, rect);

            int currentY = firstElementOffset - Math.Abs( this.AutoScrollPosition.Y );
            int currentElement = this.firstElementIndex;

            int[] columnWidths = this.lastColumnWidths != null ? this.lastColumnWidths : this.GetColumnWidths();

            int commentXStart = columnWidths[0] + columnWidths[1] + (2 * interColumnMargin);
            int commentWidth = rect.Width - commentXStart;

            int boldFontColumn = Array.IndexOf(columnNames, "Artist");
            while (currentY < Math.Abs(this.AutoScrollPosition.Y) + this.Height)
            {
                System.Diagnostics.Debug.WriteLine("currentY: " + currentY + ", " + this.AutoScrollPosition.Y + ", " + this.Height);
                if (currentElement < 0 || currentElement > this.MaxElementCount)
                {
                    break;
                }

                if (currentElement < this.MaxElementCount)
                {
                    DataRowView rowView = this.dataSource[currentElement];

                    int thisHeight = 0;

                    int x = 0;

                    CachedGraphics cg = null;
                    if (currentElement == this.currentRow)
                        this.DisposeFromCache(rowView.Row);

                    bool disposeCachedGraphicsImmediately = false;

                    ShowHeight showHeight = new ShowHeight();

                    if (cachedRowGraphics.ContainsKey(rowView.Row))
                    {
                        cg = cachedRowGraphics[rowView.Row];
                        thisHeight = cg.GetImage().Height;
                        this.lastCalculatedHeight[currentElement] = thisHeight;
                    }
                    else
                    {
                        showHeight = this.GetElemHeight(e.Graphics, currentElement, commentWidth);
                        thisHeight = showHeight.TotalHeight;
                    }

                    bool noIntersect = false;
                    //e.ClipRectangle.Top > currentY + thisHeight
                      //   || e.ClipRectangle.Top + e.ClipRectangle.Height < currentY;

                    if (!noIntersect)
                    {

                        if (cg == null)
                        {
                            cg = new CachedGraphics(new Bitmap(rect.Width, thisHeight, e.Graphics));
                            Graphics thisShowGraphics = cg.GetGraphics();

                            thisShowGraphics.FillRectangle(whiteBrush, thisShowGraphics.ClipBounds);

                            // mark selected row
                            if (currentElement == this.currentRow)
                            {
                                thisShowGraphics.FillRectangle(this.selectedBrush,
                                    x, 1, rect.Width, thisHeight);
                            }

                            for (int columnIndex = 0; columnIndex < columnNames.Length; columnIndex++)
                            {
                                RectangleF drawRect = new RectangleF(x, 2, columnWidths[columnIndex], Font.Height);
                                if (columnNames[columnIndex].Equals("Status"))
                                {
                                    if (rowView["IsMaster"].Equals(true))
                                        thisShowGraphics.DrawString("MASTER", Font, blackBrush, drawRect);
                                    else if (rowView["IsPublic"].Equals(false))
                                        thisShowGraphics.DrawString("PRIVATE", Font, blackBrush, drawRect);
                                    else if (rowView["NeedReplacement"].Equals(true))
                                        thisShowGraphics.DrawString("REPLACE", Font, redBrush, drawRect);
                                    // TODO combine
                                }
                                else
                                {
                                    object cellValue = rowView[columnNames[columnIndex]];

                                    switch (this.columnNames[columnIndex])
                                    {
                                        case "Artist":
                                            cellValue = VaultAtlasApplication.Model.Artists.GetDisplayNameForSortName(
                                                cellValue.ToString());
                                            break;
                                        case "Quality":
                                            cellValue = cellValue is DBNull ? string.Empty :
                                                VaultAtlasApplication.Model.Quality.QualityNames[(byte)cellValue];
                                            break;
                                        case "Date":
                                            cellValue = DateFormat.Format(dateFormat, ShowDate.Parse(cellValue.ToString()));
                                            break;
                                    }

                                    thisShowGraphics.DrawString(cellValue.ToString(),
                                        columnIndex == boldFontColumn ? boldFont : Font, blackBrush,
                                        drawRect);
                                }

                                x += columnWidths[columnIndex];

                                if (columnIndex != columnNames.Length - 1)
                                    thisShowGraphics.DrawLine(gridLinePen, x + 2, 0, x + 2, thisHeight);

                                x += interColumnMargin;
                            }

                            if (this.showComments && showHeight.CommentHeight > 0)
                            {
                                RectangleF commentsR = new RectangleF(commentXStart, 2 + Font.Height + commentMargin,
                                    commentWidth, showHeight.CommentHeight);
                                thisShowGraphics.FillRectangle(currentElement == this.currentRow ? this.selectedBrush : whiteBrush, commentsR);
                                thisShowGraphics.DrawString(rowView["Comments"].ToString(), this.commentFont,
                                    blackBrush, commentsR);

                            }

                            if (this.showResources)
                            {
                                DataRow[] resources = rowView.Row.GetChildRows("Resource2Show");
                                if (resources.Length != 0)
                                {
                                    thisShowGraphics.DrawString("(there are resources)", this.commentFont, blackBrush,
                                        new RectangleF(commentXStart, 2 + Font.Height + commentMargin, commentWidth, Font.Height));
                                }
                            }

                            if (currentElement != this.currentRow)
                            {
                                this.DisposeFromCache(rowView.Row);
                                this.cachedRowGraphics[rowView.Row] = cg;
                            }
                            else
                                disposeCachedGraphicsImmediately = true;
                        }

                        e.Graphics.DrawImage(cg.GetImage(), 0, currentY);
                        e.Graphics.DrawLine(gridLinePen,
                            0, currentY + thisHeight - 1,
                            this.Width, currentY + thisHeight - 1);
                        
                        if (disposeCachedGraphicsImmediately)
                            cg.Dispose();

                    }

                    currentY += thisHeight;
                }
                currentElement++;
            }
            this.lastVisibleElement = currentElement;

            System.Diagnostics.Debug.WriteLine("autoscroll-y: "+this.AutoScrollPosition.Y+", fill rect: "
                + e.ClipRectangle.Top + ", " + e.ClipRectangle.Height + ", painted "
                + (this.lastVisibleElement - firstElementIndex) + " elements");

        }

        private int firstElementOffset = 0;

		private int firstElementIndex = 0;
		public int FirstElementIndex 
		{
			get 
			{
				return this.firstElementIndex;
			}
			set 
			{
				this.UpdateFirstElementIndex( value, true );
			}
		}

		private int lastVisibleElement = 0;

        private DataRow lastCurrentRow;

        private int currentRow = 0;
		public int CurrentRow 
		{
			get 
			{
				return this.currentRow;
			}
			set 
			{
                if (value < 0)
                    value = 0;
                if (this.currentRow != value)
                {
                    this.currentRow = value;
                    if (this.currentRow > this.lastVisibleElement-1)
                    {
                        this.UpdateFirstElementIndex(currentRow, false);
                    }
                    if (this.currentRow < this.firstElementIndex)
                    {
                        this.UpdateFirstElementIndex(value, false);
                        this.currentRow = value;
                    }
                    if (this.currentRow < this.dataSource.Count)
                        this.lastCurrentRow = this.DataSource[currentRow].Row;
                    this.Refresh();
                }
			}
		}

		private void UpdateFirstElementIndex( int newIndex, bool performRefreshIfNeeded ) 
		{
			int oldFirstElementIndex = this.firstElementIndex;

			this.firstElementIndex = newIndex;

            if (this.firstElementIndex > MaxElementCount - 1)
                this.firstElementIndex = MaxElementCount - 1;
            if (this.firstElementIndex < 0)
				this.firstElementIndex = 0;

			this.SelectFirstElementIfNotVisible( false );

			if ( this.firstElementIndex != oldFirstElementIndex ) 
			{
				if ( performRefreshIfNeeded )
					this.Refresh();
			}

		}

		private void SelectFirstElementIfNotVisible( bool performRefreshIfNeeded ) 
		{
			int oldCurr = this.currentRow;
			if( this.currentRow < 0 )
				this.currentRow = 0;
			if( this.currentRow > MaxElementCount-1 )
				this.currentRow = MaxElementCount-1;

			if ( oldCurr != this.currentRow && performRefreshIfNeeded )
				this.Refresh();
		}

        public void ScrollTo(int index)
        {
            this.UpdateFirstElementIndex(index, true);
            this.Refresh();
        }

        private void AdjustScrollbar()
        {
            this.Invoke(new VoidDelegate(delegate() { this.AutoScrollMinSize = new Size(4000, this.dataSource.Count * 25); }));
        }

		private void dataSource_ListChanged(object sender, ListChangedEventArgs e)
		{
			ListChangedType changeType = e.ListChangedType;
			switch( changeType ) 
			{
				case ListChangedType.ItemAdded:
				case ListChangedType.ItemMoved:
					int newIndex = e.NewIndex;
					this.UpdateFirstElementIndex( newIndex, true );
					this.CurrentRow = newIndex;
                    break;
				case ListChangedType.ItemDeleted:
                    this.CurrentRow = e.NewIndex-1;
                    this.AdjustScrollbar();
                    break;
                case ListChangedType.Reset:
                    this.AdjustScrollbar();
                    this.currentRow = this.FindLastRow() ;
					this.UpdateFirstElementIndex( 0, false );
                    this.Invalidate();
					break;

			}
		}

        private int FindLastRow()
        {
            if (this.lastCurrentRow != null)
                for (int i = 0; i < this.dataSource.Count; i++)
                    if (this.dataSource[i].Row == this.lastCurrentRow)
                        return i;
            return 0;
        }

		#region IModelNavigator Member

		private int FindRow( DataRow row ) 
		{
			for(int i = 0; i < this.dataSource.Count; ++i) 
				if ( this.dataSource[i].Row == row )
					return i;
			return -1;
		}

		public void NavigateTo(DataRow row, string columnName)
		{
			int ind = this.FindRow( row );
			if ( ind == -1 ) 
			{
				// TODO Flag einbauen ob diese Meldung angezeigt werden soll
				// MessageBox.Show("Changes are not seen. Blabla.", "VaultAtlas", MessageBoxButtons.OK, MessageBoxIcon.Information );
			} 
			else 
			{
				this.CurrentRow = ind;
			}
		}

		#endregion

		private void ShowList_FontChanged(object sender, EventArgs e)
		{
			this.boldFont = new Font( Font.Name, Font.Size, FontStyle.Bold );
			this.commentFont = new Font( Font.Name, 7, FontStyle.Regular );
		}


		private void SelectAtPosition( ) 
		{
			int selIndex = this.GetItemAtPosition( MousePosition );
			if ( selIndex != -1 )
				this.CurrentRow = selIndex;
		}

		private void ShowList_MouseDown(object sender, MouseEventArgs e)
		{
			if ( e.Button == MouseButtons.Left )
				this.SelectAtPosition();
		}
    }


}