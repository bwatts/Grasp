using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Grasp.Semantics.Relationships
{
	[Serializable]
	public struct CardinalityLimit : IEquatable<CardinalityLimit>, IComparable<CardinalityLimit>
	{
		#region Operators

		public static bool operator ==(CardinalityLimit x, CardinalityLimit y)
		{
			return x.Value == y.Value;
		}

		public static bool operator !=(CardinalityLimit x, CardinalityLimit y)
		{
			return x.Value != y.Value;
		}

		public static bool operator >(CardinalityLimit x, CardinalityLimit y)
		{
			return x.Value > y.Value;
		}

		public static bool operator <(CardinalityLimit x, CardinalityLimit y)
		{
			return x.Value < y.Value;
		}

		public static bool operator >=(CardinalityLimit x, CardinalityLimit y)
		{
			return x.Value >= y.Value;
		}

		public static bool operator <=(CardinalityLimit x, CardinalityLimit y)
		{
			return x.Value <= y.Value;
		}
		#endregion

		public static readonly CardinalityLimit Zero = new CardinalityLimit(0);
		public static readonly CardinalityLimit One = new CardinalityLimit(1);
		public static readonly CardinalityLimit Many = new CardinalityLimit(Int32.MaxValue);

		public CardinalityLimit(int value) : this()
		{
			Contract.Requires(value >= 0);

			Value = value;
		}

		public int Value { get; private set; }

		#region Overrides : Object

		public override bool Equals(object obj)
		{
			return obj is CardinalityLimit && Equals((CardinalityLimit) obj);
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public override string ToString()
		{
			return ToString(CultureInfo.InvariantCulture);
		}
		#endregion

		#region IEquatable

		public bool Equals(CardinalityLimit other)
		{
			return Value == other.Value;
		}
		#endregion

		#region IComparable

		public int CompareTo(CardinalityLimit other)
		{
			return Value.CompareTo(other.Value);
		}
		#endregion

		public string ToString(IFormatProvider format)
		{
			return this == Many ? Resources.ManyLimit : Value.ToString(format);
		}
	}
}