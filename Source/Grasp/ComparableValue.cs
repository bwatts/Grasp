using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp
{
	public abstract class ComparableValue<TThis, TValue> : ComparableToExactType<TThis>
		where TThis : ComparableValue<TThis, TValue>
		where TValue : IEquatable<TValue>, IComparable<TValue>
	{
		public static readonly Field<TValue> ValueField = Field.On<ComparableValue<TThis, TValue>>.For(x => x.Value);

		protected ComparableValue(TValue value)
		{
			Contract.Requires(value != null);

			Value = value;
		}

		protected ComparableValue(Func<TThis, TValue> initializeValue)
		{
			Contract.Requires(initializeValue != null);

			Value = initializeValue((TThis) this);
		}

		public TValue Value { get { return GetValue(ValueField); } protected set { SetValue(ValueField, value); } }

		protected sealed override bool EqualsExactType(TThis other)
		{
			return Value.Equals(other.Value);
		}

		protected sealed override int CompareToExactType(TThis other)
		{
			return Value.CompareTo(other.Value);
		}

		public sealed override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public sealed override string ToString()
		{
			return Value.ToString();
		}
	}
}