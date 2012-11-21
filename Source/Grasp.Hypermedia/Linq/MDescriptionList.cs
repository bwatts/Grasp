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
	public sealed class MDescriptionList : MContent
	{
		public static readonly Field<Many<MDescriptionItem>> ItemsField = Field.On<MDescriptionList>.For(x => x.Items);

		public MDescriptionList(MClass @class, IEnumerable<MDescriptionItem> items) : base(@class)
		{
			Contract.Requires(items != null);

			Items = items.ToMany();
		}

		public MDescriptionList(MClass @class, params MDescriptionItem[] items) : this(@class, items as IEnumerable<MDescriptionItem>)
		{}

		public MDescriptionList(IEnumerable<MDescriptionItem> items) : this(MClass.Empty, items)
		{}

		public MDescriptionList(params MDescriptionItem[] items) : this(items as IEnumerable<MDescriptionItem>)
		{}

		public Many<MDescriptionItem> Items { get { return GetValue(ItemsField); } private set { SetValue(ItemsField, value); } }

		protected override object GetHtmlWithoutClass()
		{
			return new XElement("dl", GetItemsHtml());
		}

		protected override object GetHtmlWithClass(string classStack)
		{
			return new XElement("dl", new XAttribute("class", classStack), GetItemsHtml());
		}

		private IEnumerable<object> GetItemsHtml()
		{
			return Items.Select(item => item.GetHtml());
		}
	}
}