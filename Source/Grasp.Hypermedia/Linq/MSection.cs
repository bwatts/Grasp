using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Grasp.Hypermedia.Linq
{
	public sealed class MSection : MContent
	{
		public static readonly Field<Many<MContent>> ItemsField = Field.On<MSection>.For(x => x.Items);

		public MSection(MClass @class, IEnumerable<MContent> items) : base(@class)
		{
			Contract.Requires(items != null);

			Items = items.ToMany();
		}

		public MSection(MClass @class, params MContent[] items) : this(@class, items as IEnumerable<MContent>)
		{}

		public MSection(IEnumerable<MContent> items) : this(MClass.Empty, items)
		{}

		public MSection(params MContent[] items) : this(items as IEnumerable<MContent>)
		{}

		public Many<MContent> Items { get { return GetValue(ItemsField); } private set { SetValue(ItemsField, value); } }

		protected override object GetHtmlWithoutClass()
		{
			return new XElement("section", GetContent());
		}

		protected override object GetHtmlWithClass(string classStack)
		{
			return new XElement("section", new XAttribute("class", classStack), GetContent());
		}

		private IEnumerable<object> GetContent()
		{
			return Items.Select(item => item.GetHtml());
		}
	}
}