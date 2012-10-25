using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Cloak;
using Grasp.Checks;

namespace Grasp.Hypermedia
{
	public sealed class Href : ComparableValue<Href, string>
	{
		public static string Escape(string unescapedText)
		{
			return Uri.EscapeDataString(unescapedText);
		}

		public static string Unescape(string escapedText)
		{
			Contract.Requires(escapedText != null);

			// http://www.west-wind.com/weblog/posts/2009/Feb/05/Html-and-Uri-String-Encoding-without-SystemWeb

			escapedText = escapedText.Replace("+", " ");

			return Uri.UnescapeDataString(escapedText);
		}

		public static bool HasParameters(string text)
		{
			Contract.Requires(text != null);

			return Regex.IsMatch(text, @"{[\w-]+}");
		}

		public static readonly Field<Many<HrefPart>> PartsField = Field.On<Href>.For(x => x.Parts);
		public static readonly Field<HrefQuery> QueryField = Field.On<Href>.For(x => x.Query);

		public static readonly Href Root = new Href();

		public Href(Uri relativeUri) : base(@this => @this.InitializeAndGetText(relativeUri))
		{}

		public Href(string relativeUri) : base(@this => @this.InitializeAndGetText(relativeUri))
		{}

		public Href(IEnumerable<HrefPart> parts, HrefQuery query = null) : base(@this => @this.InitializeAndGetText(parts, query))
		{}

		private Href(Href baseHref, HrefPart nextPart) : this(new Many<HrefPart>(baseHref.Parts.Concat(new[] { nextPart })), baseHref.Query)
		{}

		private Href() : this(Enumerable.Empty<HrefPart>(), HrefQuery.Empty)
		{}

		public Many<HrefPart> Parts { get { return GetValue(PartsField); } private set { SetValue(PartsField, value); } }
		public HrefQuery Query { get { return GetValue(QueryField); } private set { SetValue(QueryField, value); } }
		
		public bool IsTemplate
		{
			get { return Parts.Any(part => part.IsParameter) || Query.IsTemplate; }
		}

		public Href Then(HrefPart nextPart)
		{
			return nextPart == HrefPart.Separator ? this : new Href(this, nextPart);
		}

		public Href Then(string nextPart)
		{
			return Then(new HrefPart(nextPart));
		}

		public Href Then(object nextPart)
		{
			Contract.Requires(nextPart != null);

			return Then(nextPart.ToString());
		}

		public Href Then(object nextPart, IFormatProvider formatProvider)
		{
			Contract.Requires(nextPart != null);
			Contract.Requires(formatProvider != null);

			return Then(String.Format(formatProvider, "{0}", nextPart));
		}

		public Href ThenParameter(string name)
		{
			return Then(HrefPart.Parameter(name));
		}

		public Href BindParameter(string parameterName, string value)
		{
			Contract.Requires(parameterName != null);

			var newParts = Parts.Select(part => part.BindParameter(parameterName, value));

			var newQuery = Query.BindParameter(parameterName, value);

			return new Href(newParts, newQuery);
		}

		public Uri ToUri(bool allowTemplate = false)
		{
			if(!allowTemplate)
			{
				EnsureNotTemplate();
			}

			var text = ToString();

			Uri uri;

			if(!Uri.TryCreate(text, UriKind.Absolute, out uri))
			{
				uri = new Uri(text, UriKind.Relative);
			}

			return uri;
		}

		public Uri ToAbsoluteUri(Uri baseUri, bool allowTemplate = false)
		{
			Contract.Requires(baseUri != null);

			if(!allowTemplate)
			{
				EnsureNotTemplate();
			}

			return new Uri(baseUri, ToString());
		}

		private void EnsureNotTemplate()
		{
			if(IsTemplate)
			{
				throw new InvalidOperationException(Resources.TemplatesNotAllowed.FormatCurrent(Value));
			}
		}

		private string InitializeAndGetText(Uri relativeUri)
		{
			Contract.Requires(relativeUri != null);

			var query = Check.That(relativeUri.PathAndQuery).IsNotNullOrEmpty() ? new HrefQuery(relativeUri.PathAndQuery) : HrefQuery.Empty;

			return InitializeAndGetText(HrefPart.Split(relativeUri), query);
		}

		private string InitializeAndGetText(string relativeUri)
		{
			Contract.Requires(relativeUri != null);

			var parts = relativeUri.Split('?');

			switch(parts.Length)
			{
				case 1: return InitializeAndGetText(HrefPart.Split(parts[0]), HrefQuery.Empty);
				case 2: return InitializeAndGetText(HrefPart.Split(parts[0]), new HrefQuery(parts[1]));
				default: throw new FormatException(Resources.InvalidHrefUri.FormatCurrent(relativeUri));
			}
		}

		private string InitializeAndGetText(IEnumerable<HrefPart> parts, HrefQuery query)
		{
			Contract.Requires(parts != null);

			Parts = new Many<HrefPart>(parts);
			Query = query ?? HrefQuery.Empty;

			var text = new StringBuilder();

			foreach(var part in Parts)
			{
				if(text.Length > 0)
				{
					text.Append(HrefPart.Separator);
				}

				text.Append(part);
			}

			if(Query != HrefQuery.Empty)
			{
				text.Append('?').Append(Query);
			}

			return text.ToString();
		}
	}
}