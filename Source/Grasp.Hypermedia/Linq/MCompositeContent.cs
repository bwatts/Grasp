using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Grasp.Hypermedia.Linq
{
	public sealed class MCompositeContent : MContent
	{
		public static readonly Field<ManyInOrder<MContent>> ItemsField = Field.On<MCompositeContent>.For(x => x.Items);

		public MCompositeContent(IEnumerable<MContent> items) : base(MClass.Empty)
		{
			Items = items.ToManyInOrder();
		}

		public MCompositeContent(params MContent[] items) : this(items as IEnumerable<MContent>)
		{}

		public ManyInOrder<MContent> Items { get { return GetValue(ItemsField); } private set { SetValue(ItemsField, value); } }

		protected override object GetHtmlWithoutClass()
		{
			return Items.Select(item => item.GetHtml());
		}

		protected override object GetHtmlWithClass(string classStack)
		{
			return Items.Select(item => item.GetHtml());
		}
	}
}