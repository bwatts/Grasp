using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Grasp;
using Grasp.Hypermedia.Lists;
using Grasp.Lists;

namespace Slate.Web.Site.Presentation.Lists
{
	public class ItemModel : ViewModel
	{
		public static readonly Field<CountModel> NumberField = Field.On<ItemModel>.For(x => x.Number);
		public static readonly Field<ListItemBindings> BindingsField = Field.On<ItemModel>.For(x => x.Bindings);

		public ItemModel(CountModel number, ListItemBindings bindings)
		{
			Contract.Requires(number != null);
			Contract.Requires(bindings != null);

			Number = number;
			Bindings = bindings;
		}

		public CountModel Number { get { return GetValue(NumberField); } private set { SetValue(NumberField, value); } }
		public ListItemBindings Bindings { get { return GetValue(BindingsField); } private set { SetValue(BindingsField, value); } }
	}
}