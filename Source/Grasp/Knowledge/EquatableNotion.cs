using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grasp.Knowledge
{
	public abstract class EquatableNotion<T> : Notion, IEquatable<T> where T : EquatableNotion<T>
	{
		public static bool operator ==(EquatableNotion<T> x, T y)
		{
			return Object.ReferenceEquals(x, y) || (!Object.ReferenceEquals(x, null) && x.Equals(y));
		}

		public static bool operator !=(EquatableNotion<T> x, T y)
		{
			return !(x == y);
		}

		public abstract bool Equals(T other);

		public override abstract int GetHashCode();

		public override bool Equals(object obj)
		{
			return Equals(obj as T);
		}
	}
}