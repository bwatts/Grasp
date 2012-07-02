using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grasp.Knowledge
{
	public abstract class ComparableNotion<TThis> : EquatableNotion<TThis>, IComparable<TThis> where TThis : ComparableNotion<TThis>
	{
		public static bool operator >(ComparableNotion<TThis> x, TThis y)
		{
			return x.CompareTo(y) > 0;
		}

		public static bool operator <(ComparableNotion<TThis> x, TThis y)
		{
			return x.CompareTo(y) < 0;
		}

		public static bool operator >=(ComparableNotion<TThis> x, TThis y)
		{
			return x.CompareTo(y) >= 0;
		}

		public static bool operator <=(ComparableNotion<TThis> x, TThis y)
		{
			return x.CompareTo(y) <= 0;
		}

		public abstract int CompareTo(TThis other);
	}
}