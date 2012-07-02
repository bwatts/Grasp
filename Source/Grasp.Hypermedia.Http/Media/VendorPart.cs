using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Grasp.Hypermedia.Http.Media
{
	public abstract class VendorPart<TThis> : ComparableNotion<TThis> where TThis : VendorPart<TThis>
	{
		public static readonly Field<string> ValueField = Field.On<VendorPart<TThis>>.Backing(x => x.Value);

		protected VendorPart(string value)
		{
			Contract.Requires(value != null);

			Value = value;
		}

		public string Value { get { return GetValue(ValueField); } private set { SetValue(ValueField, value); } }

		public override int CompareTo(TThis other)
		{
			return other == null ? 1 : Value.CompareTo(other.Value);
		}

		public override bool Equals(TThis other)
		{
			return other != null && Value.Equals(other.Value);
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public override string ToString()
		{
			return Value;
		}
	}
}