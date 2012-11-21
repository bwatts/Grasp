using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp
{
	/// <summary>
	/// Extends sequences with the ability to initialize instances of <see cref="Many{T}"/>, <see cref="ManyInOrder{T}"/>, and
	/// <see cref="ManyKeyed{TKey, TValue}"/>
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class ManyConversion
	{
		/// <summary>
		/// Creates a <see cref="Many{T}"/> with the items in the specified sequence
		/// </summary>
		/// <typeparam name="T">The type of items in the sequence</typeparam>
		/// <param name="source">The initial items in the <see cref="Many{T}"/></param>
		/// <returns>A <see cref="Many{T}"/> with the items from specified sequence</returns>
		public static Many<T> ToMany<T>(this IEnumerable<T> source)
		{
			return new Many<T>(source);
		}

		/// <summary>
		/// Creates a <see cref="ManyInOrder{T}"/> with the items in the specified sequence
		/// </summary>
		/// <typeparam name="T">The type of items in the sequence</typeparam>
		/// <param name="source">The initial items in the <see cref="ManyInOrder{T}"/></param>
		/// <returns>A <see cref="ManyInOrder{T}"/> with the items from specified sequence</returns>
		public static ManyInOrder<T> ToManyInOrder<T>(this IEnumerable<T> source)
		{
			return new ManyInOrder<T>(source);
		}

		/// <summary>
		/// Creates a <see cref="ManyKeyed{TKey, TValue}"/> with the pairs in the specified sequence
		/// </summary>
		/// <typeparam name="TKey">The type of keys of pairs in the sequence</typeparam>
		/// <typeparam name="TValue">The type of values of pairs in the sequence</typeparam>
		/// <param name="source">The initial pairs in the <see cref="ManyKeyed{TKey, TValue}"/></param>
		/// <returns>A <see cref="ManyKeyed{TKey, TValue}"/> with the pairs from specified sequence</returns>
		public static ManyKeyed<TKey, TValue> ToManyKeyed<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
		{
			return new ManyKeyed<TKey, TValue>(source);
		}
	}
}