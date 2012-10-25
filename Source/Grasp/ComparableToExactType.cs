using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grasp
{
	public abstract class ComparableToExactType<TThis> : ComparableNotion<TThis> where TThis : ComparableToExactType<TThis>
	{
		public override int CompareTo(TThis other)
		{
			return other == null || !IsExactType(other) ? 1 : CompareToExactType(other);
		}

		public override bool Equals(TThis other)
		{
			return other != null && IsExactType(other) && EqualsExactType(other);
		}

		protected abstract int CompareToExactType(TThis other);

		protected abstract bool EqualsExactType(TThis other);

		private bool IsExactType(TThis other)
		{
			return GetType() == other.GetType();
		}
	}
}