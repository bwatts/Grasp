using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp
{
	public sealed class Many<T> : ICollection<T>, IReadOnlyCollection<T>
	{
		private readonly HashSet<T> _items;

		public Many()
		{
			_items = new HashSet<T>();
		}

		public Many(IEnumerable<T> items)
		{
			_items = new HashSet<T>(items);
		}

		public Many(params T[] items) : this(items as IEnumerable<T>)
		{}

		public IEnumerator<T> GetEnumerator()
		{
			return _items.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		void ICollection<T>.Add(T item)
		{
			_items.Add(item);
		}

		bool ICollection<T>.Remove(T item)
		{
			return _items.Remove(item);
		}

		void ICollection<T>.Clear()
		{
			_items.Clear();
		}

		public bool Contains(T item)
		{
			return _items.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			_items.CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get { return _items.Count; }
		}

		bool ICollection<T>.IsReadOnly
		{
			get { return ((ICollection<T>) _items).IsReadOnly; }
		}
	}
}