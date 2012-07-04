using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Grasp.Hypermedia.Http.Mesh
{
	public sealed class UriPart : ComparableNotion<UriPart>
	{
		public const string Separator = "/";

		public static readonly Field<UriPart> BasePartField = Field.On<UriPart>.Backing(x => x.BasePart);
		public static readonly Field<string> ValueField = Field.On<UriPart>.Backing(x => x.Value);

		public static readonly UriPart Root = new UriPart();

		public UriPart(UriPart basePart, string value)
		{
			Contract.Requires(basePart != null);
			Contract.Requires(!String.IsNullOrEmpty(value));

			BasePart = basePart;
			Value = value;
		}

		private UriPart(UriPart basePart)
		{
			BasePart = basePart;
			Value = "";
		}

		private UriPart()
		{
			Value = "";
		}

		public UriPart BasePart { get { return GetValue(BasePartField); } private set { SetValue(BasePartField, value); } }
		public string Value { get { return GetValue(ValueField); } private set { SetValue(ValueField, value); } }

		public UriPart Then(string nextPart)
		{
			return nextPart == Separator ? new UriPart(this) : new UriPart(this, nextPart);
		}

		public UriPart Then(object nextPart)
		{
			Contract.Requires(nextPart != null);

			return Then(nextPart.ToString());
		}

		public UriPart Then(object nextPart, IFormatProvider formatProvider)
		{
			Contract.Requires(nextPart != null);
			Contract.Requires(formatProvider != null);

			return Then(String.Format(formatProvider, "{0}", nextPart));
		}

		public UriPart ThenParameter(string parameterName)
		{
			return new UriPart(this, "{" + parameterName + "}");
		}

		public bool TryGetParameterName(out string name)
		{
			var isParameter = Value.StartsWith("{") && Value.EndsWith("}");

			name = !isParameter ? null : Value.Substring(1, Value.Length - 2);

			return isParameter;
		}

		public Uri ToAbsoluteUrl(Uri baseUrl)
		{
			Contract.Requires(baseUrl != null);

			return new Uri(baseUrl, ToString());
		}

		public override int CompareTo(UriPart other)
		{
			return other == null ? 1 : Value.CompareTo(other.Value);
		}

		public override bool Equals(UriPart other)
		{
			return other != null && Value.Equals(other.Value);
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public override string ToString()
		{
			var uri = new StringBuilder();

			if(BasePart != null)
			{
				var basePartText = BasePart.ToString();

				uri.Append(basePartText);

				if(!String.IsNullOrEmpty(basePartText) && !basePartText.EndsWith(Separator))
				{
					uri.Append(Separator);
				}
			}

			uri.Append(Value);

			return uri.ToString();
		}
	}
}