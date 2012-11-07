using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Lists
{
	[Serializable]
	public struct Number : IEquatable<Number>, IComparable<Number>
	{
		#region Operators

		public static bool operator ==(Number x, Number y)
		{
			return x.Value == y.Value;
		}

		public static bool operator !=(Number x, Number y)
		{
			return x.Value != y.Value;
		}

		public static bool operator >(Number x, Number y)
		{
			return x.Value > y.Value;
		}

		public static bool operator <(Number x, Number y)
		{
			return x.Value < y.Value;
		}

		public static bool operator >=(Number x, Number y)
		{
			return x.Value >= y.Value;
		}

		public static bool operator <=(Number x, Number y)
		{
			return x.Value <= y.Value;
		}

		public static Number operator +(Number x, Number y)
		{
			return new Number(x.Value + y.Value);
		}

		public static Number operator -(Number x, Number y)
		{
			return new Number(x.Value - y.Value);
		}

		public static Number operator +(Number x, int y)
		{
			return new Number(x.Value + y);
		}

		public static Number operator -(Number x, int y)
		{
			return new Number(x.Value - y);
		}
		#endregion

		public static readonly Number None = new Number();
		public static readonly Number First = new Number(1);

		public Number(int value) : this()
		{
			Contract.Requires(value >= 1);

			Value = value;
		}

		public int Value { get; private set; }

		public override bool Equals(object obj)
		{
			return obj is Number && Equals((Number) obj);
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public override string ToString()
		{
			return ToString(CultureInfo.InvariantCulture);
		}

		public bool Equals(Number other)
		{
			return Value == other.Value;
		}
		
		public int CompareTo(Number other)
		{
			return Value.CompareTo(other.Value);
		}

		public string ToString(IFormatProvider format)
		{
			return Value.ToString(format);
		}
	}
}