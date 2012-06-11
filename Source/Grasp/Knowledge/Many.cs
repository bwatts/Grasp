using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Grasp.Semantics.Relationships;

namespace Grasp.Knowledge
{
	public sealed class Many<T> : ISet<T>
	{
		private readonly ISet<T> _values;

		public Many()
		{
			_values = new HashSet<T>();
		}

		public Many(IEnumerable<T> values)
		{
			Contract.Requires(values != null);

			_values = new HashSet<T>(values);
		}

		public Many(params T[] values) : this(values as IEnumerable<T>)
		{}

		public Many(IEqualityComparer<T> comparer)
		{
			_values = new HashSet<T>(comparer);
		}

		public Many(IEqualityComparer<T> comparer, IEnumerable<T> values)
		{
			_values = new HashSet<T>(values, comparer);
		}

		public Many(IEqualityComparer<T> comparer, params T[] values) : this(comparer, values as IEnumerable<T>)
		{}

		#region IEnumerable

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return _values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<T>) this).GetEnumerator();
		}
		#endregion

		#region ICollection

		void ICollection<T>.Add(T item)
		{
			((ICollection<T>) _values).Add(item);
		}

		bool ICollection<T>.Remove(T item)
		{
			return _values.Remove(item);
		}

		void ICollection<T>.Clear()
		{
			_values.Clear();
		}

		bool ICollection<T>.Contains(T item)
		{
			return _values.Contains(item);
		}

		void ICollection<T>.CopyTo(T[] array, int arrayIndex)
		{
			_values.CopyTo(array, arrayIndex);
		}

		int ICollection<T>.Count
		{
			get { return _values.Count; }
		}

		bool ICollection<T>.IsReadOnly
		{
			get { return _values.IsReadOnly; }
		}
		#endregion

		#region ISet

		bool ISet<T>.Add(T item)
		{
			return _values.Add(item);
		}

		void ISet<T>.ExceptWith(IEnumerable<T> other)
		{
			_values.ExceptWith(other);
		}

		void ISet<T>.IntersectWith(IEnumerable<T> other)
		{
			_values.IntersectWith(other);
		}

		bool ISet<T>.IsProperSubsetOf(IEnumerable<T> other)
		{
			return _values.IsProperSubsetOf(other);
		}

		bool ISet<T>.IsProperSupersetOf(IEnumerable<T> other)
		{
			return _values.IsProperSupersetOf(other);
		}

		bool ISet<T>.IsSubsetOf(IEnumerable<T> other)
		{
			return _values.IsSubsetOf(other);
		}

		bool ISet<T>.IsSupersetOf(IEnumerable<T> other)
		{
			return _values.IsSupersetOf(other);
		}

		bool ISet<T>.Overlaps(IEnumerable<T> other)
		{
			return _values.Overlaps(other);
		}

		bool ISet<T>.SetEquals(IEnumerable<T> other)
		{
			return _values.SetEquals(other);
		}

		void ISet<T>.SymmetricExceptWith(IEnumerable<T> other)
		{
			_values.SymmetricExceptWith(other);
		}

		void ISet<T>.UnionWith(IEnumerable<T> other)
		{
			_values.UnionWith(other);
		}
		#endregion
	}
}