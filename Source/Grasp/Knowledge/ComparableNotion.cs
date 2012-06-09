using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grasp.Knowledge
{
	public abstract class ComparableNotion<T> : EquatableNotion<T>, IComparable<T> where T : ComparableNotion<T>
	{
		public static bool operator >(ComparableNotion<T> x, T y)
		{
			return x.CompareTo(y) > 0;
		}

		public static bool operator <(ComparableNotion<T> x, T y)
		{
			return x.CompareTo(y) < 0;
		}

		public static bool operator >=(ComparableNotion<T> x, T y)
		{
			return x.CompareTo(y) >= 0;
		}

		public static bool operator <=(ComparableNotion<T> x, T y)
		{
			return x.CompareTo(y) <= 0;
		}

		public abstract int CompareTo(T other);
	}
}