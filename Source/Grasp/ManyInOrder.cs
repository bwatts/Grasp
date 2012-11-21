using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp
{
	public sealed class ManyInOrder<T> : IList<T>, IReadOnlyList<T>
	{
		private readonly OrderedDictionary _items;

		public ManyInOrder()
		{
			_items = new OrderedDictionary();
		}

		public ManyInOrder(IEnumerable<T> items)
		{
			Contract.Requires(items != null);

			_items = new OrderedDictionary();

			foreach(var item in items)
			{
				_items.Add(item, null);
			}
		}

		public ManyInOrder(params T[] items) : this(items as IEnumerable<T>)
		{}

		public IEnumerator<T> GetEnumerator()
		{
			return _items.Keys.Cast<T>().GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		void ICollection<T>.Add(T item)
		{
			_items.Add(item, null);
		}

		void ICollection<T>.Clear()
		{
			_items.Clear();
		}

		bool ICollection<T>.IsReadOnly
		{
			get { return _items.IsReadOnly; }
		}

		bool ICollection<T>.Remove(T item)
		{
			var exists = Contains(item);

			if(exists)
			{
				_items.Remove(item);
			}

			return exists;
		}

		public int IndexOf(T item)
		{
			for(var index = 0; index < _items.Count; index++)
			{
				var currentItem = (T) _items[index];

				if(EqualityComparer<T>.Default.Equals(currentItem, item))
				{
					return index;
				}
			}

			return -1;
		}

		void IList<T>.Insert(int index, T item)
		{
			_items.Insert(index, item, null);
		}

		void IList<T>.RemoveAt(int index)
		{
			_items.RemoveAt(index);
		}

		public T this[int index]
		{
			get { return (T) _items[index]; }
			set { _items[index] = value; }
		}

		public bool Contains(T item)
		{
			return _items.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			_items.Keys.CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get { return _items.Count; }
		}
	}
}