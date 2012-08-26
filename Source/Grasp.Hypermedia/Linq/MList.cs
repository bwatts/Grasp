using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Grasp.Knowledge;

namespace Grasp.Hypermedia.Linq
{
	public sealed class MList : MContent
	{
		public static readonly Field<int> OrderField = Field.AttachedTo<MContent>.By<MList>.Backing(() => OrderField);
		public static readonly Field<Many<MContent>> ItemsField = Field.On<MList>.Backing(x => x.Items);

		public MList(MClass @class, IEnumerable<MContent> items) : base(@class)
		{
			Contract.Requires(items != null);

			Items = new Many<MContent>(items);
		}

		public Many<MContent> Items { get { return GetValue(ItemsField); } private set { SetValue(ItemsField, value); } }

		internal override object GetHtmlContent()
		{
			// TODO: Should null sort first or last?

			var orderedItems =
				from item in Items
				let order = OrderField.IsUnset(item) ? null : (int?) OrderField.Get(item)
				orderby order
				select new { Value = item, HasOrder = order != null };

			var listElement = new XElement("ul");

			var itemHasOrder = false;

			foreach(var item in orderedItems)
			{
				itemHasOrder |= item.HasOrder;

				listElement.Add(new XElement("li", item.Value.GetHtmlContent()));
			}

			if(itemHasOrder)
			{
				listElement.Name = "ol";
			}

			return listElement;
		}
	}
}