using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Cloak;
using Cloak.Linq;

namespace Grasp.Hypermedia.Linq
{
	public sealed class MDefinitionList : MContent
	{
		public static readonly Field<IReadOnlyDictionary<MValue, MContent>> TermsField = Field.On<MDefinitionList>.For(x => x.Terms);

		public MDefinitionList(MClass @class, IReadOnlyDictionary<MValue, MContent> terms) : base(@class)
		{
			Contract.Requires(terms != null);

			Terms = terms;
		}

		public MDefinitionList(MClass @class, IEnumerable<KeyValuePair<MValue, MContent>> terms) : this(@class, terms.ToReadOnlyDictionary())
		{}

		public IReadOnlyDictionary<MValue, MContent> Terms { get { return GetValue(TermsField); } private set { SetValue(TermsField, value); } }

		protected override object GetHtmlWithoutClass()
		{
			return new XElement("dl", GetTermsHtml());
		}

		protected override object GetHtmlWithClass(string classStack)
		{
			return new XElement("dl", new XAttribute("class", classStack), GetTermsHtml());
		}

		private IEnumerable<object> GetTermsHtml()
		{
			foreach(var term in Terms)
			{
				yield return new XElement("dt", term.Key.GetHtml());
				yield return new XElement("dd", term.Value.GetHtml());
			}
		}
	}
}