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

namespace Slate.Web.Presentation.Lists
{
	public class ItemModel : ViewModel
	{
		public static readonly Field<NumberModel> NumberField = Field.On<ItemModel>.For(x => x.Number);
		public static readonly Field<ListItemBindings> BindingsField = Field.On<ItemModel>.For(x => x.Bindings);

		public ItemModel(NumberModel number, ListItemBindings bindings)
		{
			Contract.Requires(number != null);
			Contract.Requires(bindings != null);

			Number = number;
			Bindings = bindings;
		}

		public NumberModel Number { get { return GetValue(NumberField); } private set { SetValue(NumberField, value); } }
		public ListItemBindings Bindings { get { return GetValue(BindingsField); } private set { SetValue(BindingsField, value); } }
	}
}