using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Knowledge.Work
{
	/// <summary>
	/// Describes a context in which work occurs
	/// </summary>
	[ContractClass(typeof(IWorkContextContract))]
	public interface IWorkContext
	{
		/// <summary>
		/// Gets the set of entities of the specified type
		/// </summary>
		/// <param name="type">The type of entities in the set</param>
		/// <returns>The set of entities of the specified type</returns>
		IEntitySet GetEntities(Type type);

		/// <summary>
		/// Gets the set of entities of the specified type
		/// </summary>
		/// <typeparam name="T">The type of entities in the set</typeparam>
		/// <returns>The set of entities of the specified type</returns>
		IEntitySet<T> GetEntities<T>() where T : Notion;

		/// <summary>
		/// Commits the changes that have occurred in this context
		/// </summary>
		void CommitChanges();
	}

	[ContractClassFor(typeof(IWorkContext))]
	internal abstract class IWorkContextContract : IWorkContext
	{
		IEntitySet IWorkContext.GetEntities(Type type)
		{
			Contract.Requires(type != null);
			Contract.Requires(typeof(Notion).IsAssignableFrom(type));
			Contract.Ensures(Contract.Result<IEntitySet>() != null);

			return null;
		}

		IEntitySet<T> IWorkContext.GetEntities<T>()
		{
			Contract.Ensures(Contract.Result<IEntitySet<T>>() != null);

			return null;
		}

		void IWorkContext.CommitChanges()
		{}
	}
}