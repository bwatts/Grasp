using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Grasp.Semantics.Relationships;

namespace Grasp.Knowledge
{
	public class Many<T> : Notion, ISet<T>
	{
		public Many()
		{
			// TODO: Initialize members for ISet implementation
		}

		public Many(IEnumerable<T> values)
		{
			Contract.Requires(values != null);

			// TODO: Use values
		}

		#region ISet<T> Members

		bool ISet<T>.Add(T item)
		{
			throw new NotImplementedException();
		}

		void ISet<T>.ExceptWith(IEnumerable<T> other)
		{
			throw new NotImplementedException();
		}

		void ISet<T>.IntersectWith(IEnumerable<T> other)
		{
			throw new NotImplementedException();
		}

		bool ISet<T>.IsProperSubsetOf(IEnumerable<T> other)
		{
			throw new NotImplementedException();
		}

		bool ISet<T>.IsProperSupersetOf(IEnumerable<T> other)
		{
			throw new NotImplementedException();
		}

		bool ISet<T>.IsSubsetOf(IEnumerable<T> other)
		{
			throw new NotImplementedException();
		}

		bool ISet<T>.IsSupersetOf(IEnumerable<T> other)
		{
			throw new NotImplementedException();
		}

		bool ISet<T>.Overlaps(IEnumerable<T> other)
		{
			throw new NotImplementedException();
		}

		bool ISet<T>.SetEquals(IEnumerable<T> other)
		{
			throw new NotImplementedException();
		}

		void ISet<T>.SymmetricExceptWith(IEnumerable<T> other)
		{
			throw new NotImplementedException();
		}

		void ISet<T>.UnionWith(IEnumerable<T> other)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region ICollection<T> Members

		void ICollection<T>.Add(T item)
		{
			throw new NotImplementedException();
		}

		void ICollection<T>.Clear()
		{
			throw new NotImplementedException();
		}

		bool ICollection<T>.Contains(T item)
		{
			throw new NotImplementedException();
		}

		void ICollection<T>.CopyTo(T[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		int ICollection<T>.Count
		{
			get { throw new NotImplementedException(); }
		}

		bool ICollection<T>.IsReadOnly
		{
			get { throw new NotImplementedException(); }
		}

		bool ICollection<T>.Remove(T item)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IEnumerable<T> Members

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}