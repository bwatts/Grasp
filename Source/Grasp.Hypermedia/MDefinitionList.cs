using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Cloak;
using Grasp.Knowledge;

namespace Grasp.Hypermedia
{
	public sealed class MDefinitionList : MContent
	{
		public static readonly Field<string> KeyField = Field.AttachedTo<MContent>.By<MDefinitionList>.Backing(() => KeyField);

		public static Field<Many<MContent>> ValuesField = Field.On<MDefinitionList>.Backing(x => x.Values);

		public MDefinitionList(MClass @class, IEnumerable<MContent> values) : base(@class)
		{
			Contract.Requires(values != null);

			Values = new Many<MContent>(values);
		}

		public MDefinitionList(MClass @class, params MContent[] values) : this(@class, values as IEnumerable<MContent>)
		{}

		public Many<MContent> Values { get { return GetValue(ValuesField); } private set { SetValue(ValuesField, value); } }

		protected override object GetHtmlContentWithoutClass()
		{
			return new XElement("dl", GetContent());
		}

		protected override object GetHtmlContentWithClass(string classStack)
		{
			return new XElement("dl", new XAttribute("class", classStack), GetContent());
		}

		private IEnumerable<object> GetContent()
		{
			foreach(var value in Values)
			{
				var key = KeyField.Get(value);

				if(key == null)
				{
					throw new HypermediaException(Resources.DefinitionListContentHasNoKeyField.FormatInvariant(KeyField));
				}

				yield return new XElement("dt", key);
				yield return new XElement("dd", value.GetHtmlContent());
			}
		}
	}
}