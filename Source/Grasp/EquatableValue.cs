using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp
{
	public abstract class EquatableValue<TThis, TValue> : EquatableWithExactType<TThis>
		where TThis : EquatableValue<TThis, TValue>
		where TValue : IEquatable<TValue>
	{
		public static readonly Field<TValue> ValueField = Field.On<EquatableValue<TThis, TValue>>.For(x => x.Value);

		protected EquatableValue(TValue value)
		{
			Contract.Requires(value != null);

			Value = value;
		}

		protected EquatableValue(Func<TThis, TValue> initializeValue)
		{
			Contract.Requires(initializeValue != null);

			Value = initializeValue((TThis) this);
		}

		public TValue Value { get { return GetValue(ValueField); } protected set { SetValue(ValueField, value); } }

		protected sealed override bool EqualsExactType(TThis other)
		{
			return Value.Equals(other.Value);
		}

		public sealed override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public sealed override string ToString()
		{
			return Value == null ? "" : Value.ToString();
		}
	}
}