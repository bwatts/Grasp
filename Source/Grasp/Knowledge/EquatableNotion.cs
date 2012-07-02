using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grasp.Knowledge
{
	public abstract class EquatableNotion<TThis> : Notion, IEquatable<TThis> where TThis : EquatableNotion<TThis>
	{
		public static bool operator ==(EquatableNotion<TThis> x, TThis y)
		{
			return Object.ReferenceEquals(x, y) || (!Object.ReferenceEquals(x, null) && x.Equals(y));
		}

		public static bool operator !=(EquatableNotion<TThis> x, TThis y)
		{
			return !(x == y);
		}

		public abstract bool Equals(TThis other);

		public override abstract int GetHashCode();

		public override bool Equals(object obj)
		{
			return Equals(obj as TThis);
		}
	}
}