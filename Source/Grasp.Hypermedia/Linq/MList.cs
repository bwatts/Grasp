﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Grasp.Hypermedia.Linq
{
	public sealed class MList : MContent
	{
		public static readonly Trait<MContent> OrderTrait = typeof(MList).Trait(() => OrderTrait);

		public static readonly Field<int> OrderField = OrderTrait.Field(() => OrderField);

		public static readonly Field<ManyInOrder<MContent>> ItemsField = Field.On<MList>.For(x => x.Items);

		public MList(MClass @class, IEnumerable<MContent> items) : base(@class)
		{
			Contract.Requires(items != null);

			Items = items.ToManyInOrder();
		}

		public MList(MClass @class, params MContent[] items) : this(@class, items as IEnumerable<MContent>)
		{}

		public MList(IEnumerable<MContent> items) : this(MClass.Empty, items)
		{}

		public MList(params MContent[] items) : this(items as IEnumerable<MContent>)
		{}

		public ManyInOrder<MContent> Items { get { return GetValue(ItemsField); } private set { SetValue(ItemsField, value); } }

		protected override object GetHtmlWithoutClass()
		{
			return GetListElement();
		}

		protected override object GetHtmlWithClass(string classStack)
		{
			var listElement = GetListElement();

			listElement.Add(new XAttribute("class", classStack));

			return listElement;
		}

		private XElement GetListElement()
		{
			// TODO: Reinstate ordering when IsUnset is ready for prime time

			var orderedItems =
				from item in Items
				select new { Value = item, HasOrder = false };
				//let order = OrderField.IsUnset(item) ? null : (int?) OrderField.Get(item)
				//orderby order
				//select new { Value = item, HasOrder = order != null };

			var listElement = new XElement("ul");

			var itemHasOrder = false;

			foreach(var item in orderedItems)
			{
				itemHasOrder |= item.HasOrder;

				listElement.Add(new XElement("li", item.Value.GetHtml()));
			}

			if(itemHasOrder)
			{
				listElement.Name = "ol";
			}

			return listElement;
		}
	}
}