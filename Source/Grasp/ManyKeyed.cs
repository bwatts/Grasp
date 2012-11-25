using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak.Linq;

namespace Grasp
{
	public sealed class ManyKeyed<TKey, TValue> : IDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>
	{
		private readonly IDictionary<TKey, TValue> _pairs;

		public ManyKeyed()
		{
			_pairs = new Dictionary<TKey, TValue>();
		}

		public ManyKeyed(IEnumerable<KeyValuePair<TKey, TValue>> pairs)
		{
			Contract.Requires(pairs != null);

			_pairs = pairs.ToDictionary();
		}

		public ManyKeyed(params KeyValuePair<TKey, TValue>[] pairs) : this(pairs as IEnumerable<KeyValuePair<TKey, TValue>>)
		{}

		void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
		{
			_pairs.Add(key, value);
		}

		public bool ContainsKey(TKey key)
		{
			return _pairs.ContainsKey(key);
		}

		ICollection<TKey> IDictionary<TKey, TValue>.Keys
		{
			get { return _pairs.Keys; }
		}

		bool IDictionary<TKey, TValue>.Remove(TKey key)
		{
			return _pairs.Remove(key);
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			return _pairs.TryGetValue(key, out value);
		}

		ICollection<TValue> IDictionary<TKey, TValue>.Values
		{
			get { return _pairs.Values; }
		}

		TValue IDictionary<TKey, TValue>.this[TKey key]
		{
			get { return _pairs[key]; }
			set { _pairs[key] = value; }
		}

		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
		{
			_pairs.Add(item.Key, item.Value);
		}

		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
		{
			return ((IDictionary<TKey, TValue>) _pairs).Contains(item);
		}

		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			((IDictionary<TKey, TValue>) _pairs).CopyTo(array, arrayIndex);
		}

		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
		{
			get { return ((IDictionary<TKey, TValue>) _pairs).IsReadOnly; }
		}

		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
		{
			return ((IDictionary<TKey, TValue>) _pairs).Remove(item);
		}

		void ICollection<KeyValuePair<TKey, TValue>>.Clear()
		{
			_pairs.Clear();
		}

		public int Count
		{
			get { return _pairs.Count; }
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return _pairs.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
		{
			get { return _pairs.Keys; }
		}

		IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
		{
			get { return _pairs.Values; }
		}

		public TValue this[TKey key]
		{
			get { return _pairs[key]; }
		}

		public IDictionary<TKey, TValue> AsWriteable()
		{
			return this;
		}
	}
}