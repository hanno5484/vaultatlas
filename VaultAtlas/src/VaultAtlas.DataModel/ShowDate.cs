using System;

namespace VaultAtlas.DataModel
{
    [Flags]
    public enum DateKnownState
    {
        AllKnown = 0,
        YearUnknown = 1,
        MonthUnknown = 2,
        DayUnknown = 4
    }

    [Serializable]
	public struct ShowDate : IComparable, IFormattable, IConvertible
	{
		public static char[] UnknownDigits = "?Xx".ToCharArray();
		
		private DateTime internalDate;
		public DateTime InternalDate 
		{
			get 
			{
				return this.internalDate;
			}
		}

		private long ticks 
		{
			get 
			{
				return this.internalDate.Ticks;
			}
		}

		private DateKnownState dateState;
		public DateKnownState KnownState 
		{
			get 
			{
				return this.dateState;
			}
		}

		public ShowDate(string showDate)
		{
			dateState = DateKnownState.AllKnown;
			internalDate = DateTime.Now;
		    var fields = (showDate ?? string.Empty).Split('-', '/', ' ');
			if ( fields.Length > 2 ) 
			{
				if (fields[0].IndexOfAny( UnknownDigits ) != -1)
				{
					this.dateState = DateKnownState.YearUnknown;
					fields[0] = "1900";
				}
				if (fields[1].IndexOfAny( UnknownDigits ) != -1) 
				{
					this.dateState |= DateKnownState.MonthUnknown;
					fields[1] = "1";
				}			
				if (fields[2].IndexOfAny( UnknownDigits ) != -1)
				{
					this.dateState |= DateKnownState.DayUnknown;
					fields[2] = "1";
				}
				this.internalDate = new DateTime( Int32.Parse(fields[0]), Int32.Parse(fields[1]), Int32.Parse(fields[2]) );
			}
		}

		public ShowDate(long ticks)
		{
			this.internalDate = new DateTime(ticks);
			this.dateState = DateKnownState.AllKnown;
		}

		public static ShowDate Parse(string str) 
		{
			return new ShowDate(str);
		}

		#region IComparable Members

		public int CompareTo(object obj)
		{
			if (obj == null)
				return 1;
			if (!(obj is ShowDate))
				throw new ArgumentException("CompareTo argument must be ShowDate");
			DateTime dt2 = ((ShowDate)obj).internalDate;
			return this.internalDate.CompareTo(dt2);
		}

		#endregion

		#region IFormattable Members

		public string ToString(string format, IFormatProvider formatProvider)
		{
			string s = this.internalDate.ToString(format, formatProvider);
			return s;
		}

		public override string ToString() 
		{
            return this.Year + "-" + this.Month + "-" + this.Day;
        }

		#endregion

		public static ShowDate Now 
		{
			get 
			{
				return new ShowDate( DateTime.Now.Ticks );
			}
		}

		private static string Stuff( int s, int mask ) 
		{
			return mask != 0 ? "??" : (s > 9) ? s+"" : "0"+s;
		}

        public string Year
        {
            get
            {
                return (this.dateState & DateKnownState.YearUnknown) != 0 ? "????" : this.internalDate.Year.ToString();
            }
        }

        public string Month
        {
            get
            {
                return Stuff(this.internalDate.Month, (int)(this.dateState & DateKnownState.MonthUnknown));
            }
        }

        public string Day
        {
            get
            {
                return Stuff(this.internalDate.Day, (int)(this.dateState & DateKnownState.DayUnknown));
            }
        }

		#region IConvertible Members

		public ulong ToUInt64(IFormatProvider provider)
		{
			throw new InvalidCastException("not supported");
		}

		public sbyte ToSByte(IFormatProvider provider)
		{
			throw new InvalidCastException("not supported");
		}

		public double ToDouble(IFormatProvider provider)
		{
			throw new InvalidCastException("not supported");
		}

		public DateTime ToDateTime(IFormatProvider provider)
		{
			return new DateTime(this.ticks);
		}

		public float ToSingle(IFormatProvider provider)
		{
			throw new InvalidCastException("not supported");
		}

		public bool ToBoolean(IFormatProvider provider)
		{
			throw new InvalidCastException("not supported");
		}

		public int ToInt32(IFormatProvider provider)
		{
			throw new InvalidCastException("not supported");
		}

		public ushort ToUInt16(IFormatProvider provider)
		{
			throw new InvalidCastException("not supported");
		}

		public short ToInt16(IFormatProvider provider)
		{
			throw new InvalidCastException("not supported");
		}

		string System.IConvertible.ToString(IFormatProvider provider)
		{
			return this.ToString( null, provider );
		}

		public byte ToByte(IFormatProvider provider)
		{
			throw new InvalidCastException("not supported");
		}

		public char ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException("not supported");
		}

		public long ToInt64(IFormatProvider provider)
		{
			throw new InvalidCastException("not supported");
		}

		public System.TypeCode GetTypeCode()
		{
			return TypeCode.Object;
		}

		public decimal ToDecimal(IFormatProvider provider)
		{
			throw new InvalidCastException("not supported");
		}
		
		public object ToType(Type conversionType, IFormatProvider provider)
		{
			if (conversionType == typeof(ShowDate))
				return this;

			throw new InvalidCastException("not supported");
		}

		public uint ToUInt32(IFormatProvider provider)
		{
			throw new InvalidCastException("not supported");
		}

		#endregion

        public static string Format(string format, ShowDate date)
        {
            return format.Replace("yyyy", date.Year).Replace("MM", date.Month).Replace("dd", date.Day);
        }
    }

}
