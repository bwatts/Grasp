using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Cloak.Linq;

namespace Grasp.Lists
{
	public class ListItem : Notion
	{
		public static readonly Field<Count> NumberField = Field.On<ListItem>.For(x => x.Number);
		public static readonly Field<ListItemBindings> BindingsField = Field.On<ListItem>.For(x => x.Bindings);

		public ListItem(Count number, ListItemBindings bindings)
		{
			Contract.Requires(bindings != null);

			Number = number;
			Bindings = bindings;
		}

		public ListItem(Count number, IEnumerable<KeyValuePair<string, object>> bindings) : this(number, new ListItemBindings(bindings))
		{}

		public Count Number { get { return GetValue(NumberField); } private set { SetValue(NumberField, value); } }
		public ListItemBindings Bindings { get { return GetValue(BindingsField); } private set { SetValue(BindingsField, value); } }

		public object this[string field]
		{
			get { return Bindings[field]; }
		}
	}
}