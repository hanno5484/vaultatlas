using System;
using System.Collections;

namespace Grape.UI.Export
{
	public class StringCollection : IEnumerable
	{
		public StringCollection()
		{
		}

		private ArrayList internalList;
		
		public int Count 
		{
			get 
			{
				return this.internalList.Count;
			}
		}

		public void Add(string s) 
		{
			this.internalList.Add(s);
		}

		public void Remove(string s) 
		{
			this.internalList.Remove(s);
		}

		#region IEnumerable Member

		public IEnumerator GetEnumerator()
		{
			return this.internalList.GetEnumerator();
		}

		#endregion
	}
}
