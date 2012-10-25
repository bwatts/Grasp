using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak.Linq;

namespace Grasp.Hypermedia.Lists
{
	public class ListItem : Notion
	{
		public static readonly Field<Number> NumberField = Field.On<ListItem>.For(x => x.Number);
		public static readonly Field<IReadOnlyDictionary<string, object>> ValuesField = Field.On<ListItem>.For(x => x.Values);

		public ListItem(Number number, IReadOnlyDictionary<string, object> values)
		{
			Contract.Requires(number != Number.None);
			Contract.Requires(values != null);

			Number = number;
			Values = values;
		}

		public ListItem(Number number, IEnumerable<KeyValuePair<string, object>> values) : this(number, values.ToReadOnlyDictionary())
		{}

		public Number Number { get { return GetValue(NumberField); } private set { SetValue(NumberField, value); } }
		public IReadOnlyDictionary<string, object> Values { get { return GetValue(ValuesField); } private set { SetValue(ValuesField, value); } }

		public object this[string valueKey]
		{
			get { return Values[valueKey]; }
		}
	}
}