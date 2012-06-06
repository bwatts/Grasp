using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Grasp.Knowledge.Work
{
	/// <summary>
	/// Describes a set of entities of a particular type
	/// </summary>
	[ContractClass(typeof(IEntitySetContract))]
	public interface IEntitySet : IQueryable
	{
		/// <summary>
		/// Gets the type of entity in this set
		/// </summary>
		Type EntityType { get; }

		/// <summary>
		/// Marks the specified entity to be added to the set when the corresponding work context commits its changes
		/// </summary>
		/// <param name="entity">The entity to be added when the corresponding work context commits its changes</param>
		void AddOnCommit(Notion entity);

		/// <summary>
		/// Marks the specified entity to be removed from the set when the corresponding work context commits its changes
		/// </summary>
		/// <param name="entity">The entity to be removed the corresponding work context commits its changes</param>
		void RemoveOnCommit(Notion entity);
	}

	/// <summary>
	/// Describes a set of entities of a particular type
	/// </summary>
	/// <typeparam name="TEntity">The type of entity in the set</typeparam>
	[ContractClass(typeof(IEntitySetContract<>))]
	public interface IEntitySet<TEntity> : IEntitySet, IQueryable<TEntity> where TEntity : Notion
	{
		/// <summary>
		/// Marks the specified entity to be added to the set when the corresponding work context commits its changes
		/// </summary>
		/// <param name="entity">The entity to be added when the corresponding work context commits its changes</param>
		void AddOnCommit(TEntity entity);

		/// <summary>
		/// Marks the specified entity to be removed from the set when the corresponding work context commits its changes
		/// </summary>
		/// <param name="entity">The entity to be removed the corresponding work context commits its changes</param>
		void RemoveOnCommit(TEntity entity);
	}

	[ContractClassFor(typeof(IEntitySet))]
	internal abstract class IEntitySetContract : IEntitySet
	{
		#region IEnumerable

		IEnumerator IEnumerable.GetEnumerator()
		{
			return null;
		}
		#endregion

		#region IQueryable

		Type IQueryable.ElementType
		{
			get { return null; ; }
		}

		Expression IQueryable.Expression
		{
			get { return null; }
		}

		IQueryProvider IQueryable.Provider
		{
			get { return null; }
		}
		#endregion

		#region IEntitySet

		Type IEntitySet.EntityType
		{
			get
			{
				Contract.Ensures(Contract.Result<Type>() != null);

				return null;
			}
		}

		void IEntitySet.AddOnCommit(Notion entity)
		{
			Contract.Requires(entity != null);
		}

		void IEntitySet.RemoveOnCommit(Notion entity)
		{
			Contract.Requires(entity != null);
		}
		#endregion
	}

	[ContractClassFor(typeof(IEntitySet<>))]
	internal abstract class IEntitySetContract<TEntity> : IEntitySet<TEntity> where TEntity : Notion
	{
		#region IEnumerable

		IEnumerator IEnumerable.GetEnumerator()
		{
			return null;
		}

		IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
		{
			return null;
		}
		#endregion

		#region IQueryable

		Type IQueryable.ElementType
		{
			get { return null; ; }
		}

		Expression IQueryable.Expression
		{
			get { return null; ; }
		}

		IQueryProvider IQueryable.Provider
		{
			get { return null; ; }
		}
		#endregion

		#region IEntitySet

		Type IEntitySet.EntityType
		{
			get { return null; }
		}

		void IEntitySet.AddOnCommit(Notion entity)
		{}

		void IEntitySet.RemoveOnCommit(Notion entity)
		{}

		void IEntitySet<TEntity>.AddOnCommit(TEntity entity)
		{
			Contract.Requires(entity != null);
		}

		void IEntitySet<TEntity>.RemoveOnCommit(TEntity entity)
		{
			Contract.Requires(entity != null);
		}
		#endregion
	}
}