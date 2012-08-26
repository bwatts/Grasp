using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Grasp.Knowledge;

namespace Grasp.Hypermedia.Linq
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

		public Many<MContent> Values { get { return GetValue(ValuesField); } private set { SetValue(ValuesField, value); } }

		internal override object GetHtmlContent()
		{
			return new XElement("dl", GetDefinitionListContent());
		}

		private IEnumerable<object> GetDefinitionListContent()
		{
			var classAttribute = Class.GetHtmlAttribute();

			if(classAttribute != null)
			{
				yield return classAttribute;
			}

			foreach(var value in Values)
			{
				var key = KeyField.Get(value);

				if(key == null)
				{
					throw new InvalidOperationException();	// TODO
				}

				yield return new XElement("dt", key);
				yield return new XElement("dd", value.GetHtmlContent());
			}
		}
	}
}