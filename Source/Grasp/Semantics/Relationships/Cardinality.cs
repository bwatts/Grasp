using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Text;
using Cloak;

namespace Grasp.Semantics.Relationships
{
	[Serializable]
	public struct Cardinality : IEquatable<Cardinality>
	{
		public static bool operator ==(Cardinality x, Cardinality y)
		{
			return x.Equals(y);
		}

		public static bool operator !=(Cardinality x, Cardinality y)
		{
			return !(x == y);
		}

		public static readonly Cardinality Unspecified = new Cardinality();
		public static readonly Cardinality ZeroToOne = new Cardinality(CardinalityLimit.Zero, CardinalityLimit.One);
		public static readonly Cardinality ZeroToMany = new Cardinality(CardinalityLimit.Zero, CardinalityLimit.Many);
		public static readonly Cardinality OneToOne = new Cardinality(CardinalityLimit.One, CardinalityLimit.One);
		public static readonly Cardinality OneToMany = new Cardinality(CardinalityLimit.One, CardinalityLimit.Many);

		public Cardinality(CardinalityLimit lowerLimit, CardinalityLimit upperLimit) : this()
		{
			Contract.Requires(lowerLimit <= upperLimit);

			LowerLimit = lowerLimit;
			UpperLimit = upperLimit;
		}

		#region Overrides : Object

		public override bool Equals(object obj)
		{
			return obj is Cardinality && Equals((Cardinality) obj);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(LowerLimit, UpperLimit);
		}

		public override string ToString()
		{
			return ToUnabbreviatedString();
		}
		#endregion

		#region IEquatable

		public bool Equals(Cardinality other)
		{
			return LowerLimit == other.LowerLimit && UpperLimit == other.UpperLimit;
		}
		#endregion

		public CardinalityLimit LowerLimit { get; private set; }

		public CardinalityLimit UpperLimit { get; private set; }

		public string ToUnabbreviatedString(IFormatProvider provider)
		{
			return this == Unspecified
				? Resources.UnspecifiedLimit
				: Resources.CardinalityFormat.Format(provider, LowerLimit, UpperLimit);
		}

		public string ToUnabbreviatedString()
		{
			return ToUnabbreviatedString(CultureInfo.InvariantCulture);
		}

		public string ToAbbreviatedString(IFormatProvider provider)
		{
			// Abbreviations gleaned from http://www.essentialstrategies.com/publications/modeling/uml.htm

			if(this == Unspecified)
			{
				return Resources.UnspecifiedLimit;
			}
			else if(this == ZeroToMany)
			{
				return CardinalityLimit.Many.ToString(provider);
			}
			else if(this == OneToOne)
			{
				return CardinalityLimit.One.ToString(provider);
			}
			else
			{
				return Resources.CardinalityFormat.Format(provider, LowerLimit, UpperLimit);
			}
		}

		public string ToAbbreviatedString()
		{
			return ToAbbreviatedString(CultureInfo.InvariantCulture);
		}
	}
}