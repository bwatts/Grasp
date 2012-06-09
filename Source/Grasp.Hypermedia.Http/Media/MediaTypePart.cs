using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Grasp.Hypermedia.Http.Media
{
	public abstract class MediaTypePart<T> : ComparableNotion<T> where T : MediaTypePart<T>
	{
		public static readonly Field<string> ValueField = Field.On<MediaTypePart<T>>.Backing(x => x.Value);

		protected MediaTypePart(string value)
		{
			Contract.Requires(value != null);

			Value = value;
		}

		public string Value { get { return GetValue(ValueField); } private set { SetValue(ValueField, value); } }

		public override int CompareTo(T other)
		{
			return other == null ? 1 : Value.CompareTo(other.Value);
		}

		public override bool Equals(T other)
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