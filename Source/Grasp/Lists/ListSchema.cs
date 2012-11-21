using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak.Linq;

namespace Grasp.Lists
{
	public sealed class ListSchema : Notion, IReadOnlyDictionary<string, Type>
	{
		public static readonly Field<ManyKeyed<string, Type>> _fieldsField = Field.On<ListSchema>.For(x => x._fields);

		private ManyKeyed<string, Type> _fields { get { return GetValue(_fieldsField); } set { SetValue(_fieldsField, value); } }

		public ListSchema(IEnumerable<KeyValuePair<string, Type>> fields)
		{
			Contract.Requires(fields != null);

			_fields = fields.ToManyKeyed();
		}

		public IEnumerable<string> FieldNames
		{
			get { return ((IReadOnlyDictionary<string, Type>) _fields).Keys; }
		}

		public IEnumerable<Type> Types
		{
			get { return ((IReadOnlyDictionary<string, Type>) _fields).Values; }
		}

		public Type this[string fieldName]
		{
			get { return _fields[fieldName]; }
		}

		public int FieldCount
		{
			get { return _fields.Count; }
		}

		public bool TryGetType(string field, out Type type)
		{
			return _fields.TryGetValue(field, out type);
		}

		public bool ContainsField(string name)
		{
			return _fields.ContainsKey(name);
		}

		public IEnumerator<KeyValuePair<string, Type>> GetEnumerator()
		{
			return _fields.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		bool IReadOnlyDictionary<string, Type>.ContainsKey(string key)
		{
			return ContainsField(key);
		}

		IEnumerable<string> IReadOnlyDictionary<string, Type>.Keys
		{
			get { return FieldNames; }
		}

		bool IReadOnlyDictionary<string, Type>.TryGetValue(string key, out Type value)
		{
			return TryGetType(key, out value);
		}

		IEnumerable<Type> IReadOnlyDictionary<string, Type>.Values
		{
			get { return Types; }
		}

		Type IReadOnlyDictionary<string, Type>.this[string key]
		{
			get { return this[key]; }
		}

		int IReadOnlyCollection<KeyValuePair<string, Type>>.Count
		{
			get { return FieldCount; }
		}

		IEnumerator<KeyValuePair<string, Type>> IEnumerable<KeyValuePair<string, Type>>.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}