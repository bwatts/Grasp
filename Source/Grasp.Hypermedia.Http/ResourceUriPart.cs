using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Grasp.Hypermedia.Http
{
	public sealed class ResourceUriPart : ComparableNotion<ResourceUriPart>
	{
		public const string Separator = "/";

		public static readonly Field<ResourceUriPart> BasePartField = Field.On<ResourceUriPart>.Backing(x => x.BasePart);
		public static readonly Field<string> ValueField = Field.On<ResourceUriPart>.Backing(x => x.Value);

		public static readonly ResourceUriPart Root = new ResourceUriPart();

		public ResourceUriPart(ResourceUriPart basePart, string value)
		{
			Contract.Requires(basePart != null);
			Contract.Requires(!String.IsNullOrEmpty(value));

			BasePart = basePart;
			Value = value;
		}

		private ResourceUriPart(ResourceUriPart basePart)
		{
			BasePart = basePart;
			Value = "";
		}

		private ResourceUriPart()
		{
			Value = "";
		}

		public ResourceUriPart BasePart { get { return GetValue(BasePartField); } private set { SetValue(BasePartField, value); } }
		public string Value { get { return GetValue(ValueField); } private set { SetValue(ValueField, value); } }

		public ResourceUriPart Then(string nextPart)
		{
			return nextPart == Separator ? new ResourceUriPart(this) : new ResourceUriPart(this, nextPart);
		}

		public ResourceUriPart Then(object nextPart)
		{
			Contract.Requires(nextPart != null);

			return Then(nextPart.ToString());
		}

		public ResourceUriPart Then(object nextPart, IFormatProvider formatProvider)
		{
			Contract.Requires(nextPart != null);
			Contract.Requires(formatProvider != null);

			return Then(String.Format(formatProvider, "{0}", nextPart));
		}

		public ResourceUriPart ThenParameter(string parameterName)
		{
			return new ResourceUriPart(this, "{" + parameterName + "}");
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

		public override int CompareTo(ResourceUriPart other)
		{
			return other == null ? 1 : Value.CompareTo(other.Value);
		}

		public override bool Equals(ResourceUriPart other)
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