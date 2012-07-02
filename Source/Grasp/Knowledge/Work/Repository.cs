using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Knowledge.Work
{
	/// <summary>
	/// Base implementation of a repository housing entities of a particular type
	/// </summary>
	/// <typeparam name="TEntity">The type of entity in the repository</typeparam>
	public abstract class Repository<TEntity> where TEntity : Notion
	{
		/// <summary>
		/// Initializes a repository within the specified work context
		/// </summary>
		/// <param name="context">The context in which this repository does its work</param>
		protected Repository(IWorkContext context)
		{
			Contract.Requires(context != null);

			Context = context;
		}

		/// <summary>
		/// Gets the context in which this repository does its work
		/// </summary>
		protected IWorkContext Context { get; private set; }

		/// <summary>
		/// Gets the set of entities housed by this repository
		/// </summary>
		protected IEntitySet<TEntity> Set
		{
			get { return Context.GetEntities<TEntity>(); }
		}

		/// <summary>
		/// Marks the specified entity to be added to the set when the work context of this repository is committed
		/// </summary>
		/// <param name="entity">The entity to be added when the corresponding work context commits its changes</param>
		protected void AddOnCommit(TEntity entity)
		{
			Set.AddOnCommit(entity);
		}

		/// <summary>
		/// Marks the specified entity to be removed from the set when the work context of this repository is committed
		/// </summary>
		/// <param name="entity">The entity to be removed the corresponding work context commits its changes</param>
		protected void RemoveOnCommit(TEntity entity)
		{
			Set.RemoveOnCommit(entity);
		}
	}
}