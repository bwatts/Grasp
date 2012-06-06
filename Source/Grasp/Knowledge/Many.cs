using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Semantics.Relationships;

namespace Grasp.Knowledge
{
	public class Many<T> : Notion, ISet<T>
	{
		public static readonly Field<CardinalityLimit> UpperLimitField = Field.On<Many<T>>.Backing(x => x.UpperLimit);

		public Many(CardinalityLimit upperLimit)
		{
			UpperLimit = upperLimit;
		}

		public CardinalityLimit UpperLimit { get { return GetValue(UpperLimitField); } private set { SetValue(UpperLimitField, value); } }

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