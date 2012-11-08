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
		public static readonly Field<IReadOnlyDictionary<MValue, MContent>> DefinitionsField = Field.On<MDefinitionList>.For(x => x.Definitions);

		public MDefinitionList(MClass @class, IReadOnlyDictionary<MValue, MContent> definitions) : base(@class)
		{
			Contract.Requires(definitions != null);

			Definitions = definitions;
		}

		public MDefinitionList(MClass @class, IEnumerable<KeyValuePair<MValue, MContent>> definitions) : this(@class, definitions.ToReadOnlyDictionary())
		{}

		public IReadOnlyDictionary<MValue, MContent> Definitions { get { return GetValue(DefinitionsField); } private set { SetValue(DefinitionsField, value); } }

		protected override object GetHtmlWithoutClass()
		{
			return new XElement("dl", GetDefinitionsHtml());
		}

		protected override object GetHtmlWithClass(string classStack)
		{
			return new XElement("dl", new XAttribute("class", classStack), GetDefinitionsHtml());
		}

		private IEnumerable<object> GetDefinitionsHtml()
		{
			foreach(var definition in Definitions)
			{
				yield return new XElement("dt", definition.Key.GetHtml());
				yield return new XElement("dd", definition.Value.GetHtml());
			}
		}
	}
}