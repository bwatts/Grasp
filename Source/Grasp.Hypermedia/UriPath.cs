using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Grasp.Hypermedia
{
	public sealed class UriPath : ComparableNotion<UriPath>
	{
		public const string Separator = "/";

		public static readonly Field<UriPath> BasePartField = Field.On<UriPath>.Backing(x => x.BasePath);
		public static readonly Field<string> ValueField = Field.On<UriPath>.Backing(x => x.Value);

		public static readonly UriPath Root = new UriPath();

		public UriPath(UriPath basePart, string value)
		{
			Contract.Requires(basePart != null);
			Contract.Requires(!String.IsNullOrEmpty(value));

			BasePath = basePart;
			Value = value;
		}

		private UriPath(UriPath basePart)
		{
			BasePath = basePart;
			Value = "";
		}

		private UriPath()
		{
			Value = "";
		}

		public UriPath BasePath { get { return GetValue(BasePartField); } private set { SetValue(BasePartField, value); } }
		public string Value { get { return GetValue(ValueField); } private set { SetValue(ValueField, value); } }

		public UriPath Then(string nextPart)
		{
			return nextPart == Separator ? new UriPath(this) : new UriPath(this, nextPart);
		}

		public UriPath Then(object nextPart)
		{
			Contract.Requires(nextPart != null);

			return Then(nextPart.ToString());
		}

		public UriPath Then(object nextPart, IFormatProvider formatProvider)
		{
			Contract.Requires(nextPart != null);
			Contract.Requires(formatProvider != null);

			return Then(String.Format(formatProvider, "{0}", nextPart));
		}

		public UriPath ThenParameter(string parameterName)
		{
			return new UriPath(this, "{" + parameterName + "}");
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

		public override int CompareTo(UriPath other)
		{
			return other == null ? 1 : Value.CompareTo(other.Value);
		}

		public override bool Equals(UriPath other)
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

			if(BasePath != null)
			{
				var basePartText = BasePath.ToString();

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