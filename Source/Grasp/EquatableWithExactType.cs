using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grasp
{
	public abstract class EquatableWithExactType<TThis> : EquatableNotion<TThis> where TThis : EquatableWithExactType<TThis>
	{
		public override bool Equals(TThis other)
		{
			return other != null && IsExactType(other) && EqualsExactType(other);
		}

		protected abstract bool EqualsExactType(TThis other);

		private bool IsExactType(TThis other)
		{
			return GetType() == other.GetType();
		}
	}
}