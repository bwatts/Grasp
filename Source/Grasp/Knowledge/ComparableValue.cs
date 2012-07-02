using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Knowledge
{
	public abstract class ComparableValue<TThis, TValue> : ComparableToExactType<TThis>
		where TThis : ComparableValue<TThis, TValue>
		where TValue : IEquatable<TValue>, IComparable<TValue>
	{
		public static readonly Field<TValue> ValueField = Field.On<ComparableValue<TThis, TValue>>.Backing(x => x.Value);

		protected ComparableValue(TValue value)
		{
			Contract.Requires(value != null);

			Value = value;
		}

		public TValue Value { get { return GetValue(ValueField); } private set { SetValue(ValueField, value); } }

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public override string ToString()
		{
			return Value.ToString();
		}

		protected override int CompareToExactType(TThis other)
		{
			return Value.CompareTo(other.Value);
		}

		protected override bool EqualsExactType(TThis other)
		{
			return Value.Equals(other.Value);
		}
	}
}