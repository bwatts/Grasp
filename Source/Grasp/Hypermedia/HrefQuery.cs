using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;

namespace Grasp.Hypermedia
{
	public sealed class HrefQuery : ComparableValue<HrefQuery, string>, ILookup<HrefPart, HrefPart>
	{
		public static readonly Field<ILookup<HrefPart, HrefPart>> _pairsField = Field.On<HrefQuery>.For(x => x._pairs);

		public static readonly HrefQuery Empty = new HrefQuery("");

		private ILookup<HrefPart, HrefPart> _pairs { get { return GetValue(_pairsField); } set { SetValue(_pairsField, value); } }

		public HrefQuery(string text) : base(@this => @this.InitializeAndGetText(text))
		{}

		private HrefQuery(ILookup<HrefPart, HrefPart> pairs) : base(@this => @this.InitializeAndGetText(pairs))
		{}

		public bool IsTemplate
		{
			get { return Href.HasParameters(Value); }
		}

		public int Count
		{
			get { return _pairs.Count; }
		}

		public IEnumerable<HrefPart> this[HrefPart key]
		{
			get { return _pairs[key]; }
		}


		public bool Contains(HrefPart key)
		{
			return _pairs.Contains(key);
		}

		public IEnumerator<IGrouping<HrefPart, HrefPart>> GetEnumerator()
		{
			return _pairs.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public string ToStringWithSeparator(bool hasBaseQuery = false)
		{
			return (hasBaseQuery ? "&" : "?") + ToString();
		}

		public HrefQuery BindParameter(string parameterName, string value)
		{
			var newQueryParts =
				from pair in this
				from valuePart in pair
				orderby pair.Key, value
				select new
				{
					Key = pair.Key,
					Value = valuePart.BindParameter(parameterName, value)
				};

			return new HrefQuery(newQueryParts.ToLookup(part => part.Key, part => part.Value));
		}

		private string InitializeAndGetText(ILookup<HrefPart, HrefPart> pairs)
		{
			Contract.Requires(pairs != null);

			_pairs = pairs;

			return GetQueryText();
		}

		private string InitializeAndGetText(string text)
		{
			Contract.Requires(text != null);

			_pairs = ParsePairs(text);

			return GetQueryText();
		}

		private static ILookup<HrefPart, HrefPart> ParsePairs(string text)
		{
			var elements = ParseElements(text);

			var pairs =
				from key in elements.Keys.Cast<string>()
				from value in elements.GetValues(key)
				select new
				{
					Key = new HrefPart(key),
					Value = new HrefPart(value)
				};

			return pairs.ToLookup(pair => pair.Key, pair => pair.Value);
		}

		private static NameValueCollection ParseElements(string text)
		{
			var elements = new NameValueCollection();

			foreach(var pair in TruncateToQuery(text).Split('&'))
			{
				var pairElements = pair.Split('=');

				switch(pairElements.Length)
				{
					case 1:
						if(pairElements[0] != "")
						{
							elements.Add(pairElements[0], "");
						}
						break;
					case 2:
						elements.Add(pairElements[0], Href.Unescape(pairElements[1]));
						break;
					default:
						throw new FormatException(Resources.InvalidQueryPair.FormatCurrent(pair));
				}
			}

			return elements;
		}

		private static string TruncateToQuery(string text)
		{
			var separatorIndex = text.IndexOf('?');

			if(separatorIndex != -1)
			{
				var queryText = text.Substring(separatorIndex + 1);

				if(queryText.Contains('?'))
				{
					throw new FormatException(Resources.QuerySeparatorRepeated.FormatCurrent(text));
				}

				text = queryText;
			}

			return text;
		}

		private string GetQueryText()
		{
			var elementValues =
				from element in _pairs
				from value in element
				orderby element.Key, value
				select new { Element = element.Key, Value = value.Escape() };

			var queryText = new StringBuilder();

			foreach(var elementValue in elementValues)
			{
				if(queryText.Length > 0)
				{
					queryText.Append('&');
				}

				queryText.Append(elementValue.Element);

				if(elementValue.Value != "")
				{
					queryText.Append('=').Append(elementValue.Value);
				}
			}

			return queryText.ToString();
		}
	}
}