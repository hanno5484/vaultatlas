using System;
using System.Text;
using System.Xml;
using System.Collections;

namespace VaultAtlas
{

	public class MRUList : ICollection
	{
		private int maxEntries;
		public int MaxEntries { get { return this.maxEntries; } }
		private ArrayList list;

		public MRUList( int capacity )
		{
			this.maxEntries = capacity;
			this.list = new ArrayList();
		}

		public MRUList( int capacity, string representation ) : this( capacity )
		{
			if ( representation != null )
				foreach( string item in representation.Split(',') ) 
					if ( !item.Equals(string.Empty) )
						this.list.Add( item );
		}

		public void Reverse()
		{
			this.list.Reverse();
		}

		public void Remove( object obj ) 
		{
			// TODO es entsteht hier ein loch in er arrayliste
			this.list.Remove( obj );
			this.OnListChanged(EventArgs.Empty);
		}

        private bool ListContains(object fileName)
        {
            if (fileName is string)
            {
                for (int i = 0; i < this.list.Count; i++)
                    if (this.list[i].ToString().ToLower().Equals(fileName.ToString().ToLower()))
                        return true;
                return false;
            }
            return this.list.Contains(fileName);
        }

        public void Add(object fileName)
		{
			if (this.ListContains( fileName ))
			{
				this.list.Remove(fileName);
			}
			else
			{
				if (this.list.Count == this.maxEntries)
					this.list.RemoveAt(this.maxEntries - 1);
			}
			this.list.Insert(0, fileName);
			this.OnListChanged(EventArgs.Empty);
		}
		
		public string GetStringRepresentation() 
		{
			StringBuilder sb = new StringBuilder();
			foreach( object obj in this.list ) 
			{
				if ( sb.Length > 0)
					sb.Append( ',' );
				sb.Append( obj );
			}
			return sb.ToString();
		}

		public object this[int i]
		{
			get
			{
				return this.list[i];
			}
		}

		public event EventHandler ListChanged;
		protected virtual void OnListChanged(EventArgs e)
		{
			if (this.ListChanged != null)
				this.ListChanged(this, e);
		}

		#region ICollection Members
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		public void CopyTo(Array array, int index)
		{
			this.list.CopyTo(array, index);
		}

		public object SyncRoot
		{
			get
			{
				return null;
			}
		}

		#endregion

		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		#endregion

	}
}