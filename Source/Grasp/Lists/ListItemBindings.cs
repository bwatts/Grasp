using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Cloak.Linq;

namespace Grasp.Lists
{
	public sealed class ListItemBindings : Notion, IReadOnlyDictionary<string, object>
	{
		public static readonly Field<ManyKeyed<string, object>> _bindingsField = Field.On<ListItemBindings>.For(x => x._bindings);

		private ManyKeyed<string, object> _bindings { get { return GetValue(_bindingsField); } set { SetValue(_bindingsField, value); } }

		public ListItemBindings(IEnumerable<KeyValuePair<string, object>> bindings)
		{
			Contract.Requires(bindings != null);

			_bindings = bindings.ToManyKeyed();
		}

		public IEnumerable<string> Fields
		{
			get { return ((IReadOnlyDictionary<string, object>)_bindings).Keys; }
		}

		public IEnumerable<object> Values
		{
			get { return ((IReadOnlyDictionary<string, object>) _bindings).Values; }
		}

		public object this[string field]
		{
			get { return _bindings[field]; }
		}

		public int Count
		{
			get { return _bindings.Count; }
		}

		public bool TryGetValue(string field, out object type)
		{
			return _bindings.TryGetValue(field, out type);
		}

		public bool ContainsField(string name)
		{
			return _bindings.ContainsKey(name);
		}

		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			return _bindings.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		bool IReadOnlyDictionary<string, object>.ContainsKey(string key)
		{
			return ContainsField(key);
		}

		IEnumerable<string> IReadOnlyDictionary<string, object>.Keys
		{
			get { return Fields; }
		}

		object IReadOnlyDictionary<string, object>.this[string key]
		{
			get { return this[key]; }
		}

		IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
		{
			return GetEnumerator();
		}

		public T Read<T>(string key)
		{
			return Conversion.To<T>(this[key]);
		}
	}
}