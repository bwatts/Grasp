using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Hypermedia.Http
{
	public sealed class ResourceUriPart
	{
		public const string Separator = "/";

		public static readonly ResourceUriPart Root = new ResourceUriPart();

		public ResourceUriPart(ResourceUriPart basePart, string value)
		{
			Contract.Requires(basePart != null);
			Contract.Requires(!String.IsNullOrEmpty(value));

			BasePart = basePart;
			Value = value;
		}

		private ResourceUriPart()
		{
			Value = "";
		}

		public ResourceUriPart BasePart { get; private set; }

		public string Value { get; private set; }

		public ResourceUriPart Then(string nextPart)
		{
			return new ResourceUriPart(this, nextPart);
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

		public Uri ToAbsoluteUri(Uri baseUri)
		{
			Contract.Requires(baseUri != null);

			return new Uri(baseUri, ToString());
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