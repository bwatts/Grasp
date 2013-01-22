using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Work
{
	/// <summary>
	/// Describes storage and retrieval of aggregate instances
	/// </summary>
	[ContractClass(typeof(IRepositoryContract))]
	public interface IRepository
	{
		/// <summary>
		/// Persists the specified aggregate to this repository
		/// </summary>
		/// <param name="aggregate">The aggregate to persist</param>
		/// <returns>The work of persisting the aggregate</returns>
		Task SaveAggregateAsync(IAggregate aggregate);

		/// <summary>
		/// Reconstitutes the aggregate with the specified name from this repository
		/// </summary>
		/// <param name="type">The type of aggregate to load</param>
		/// <param name="name">The name of the aggregate to load</param>
		/// <returns>The work of loading the aggregate with the specified name</returns>
		Task<IAggregate> LoadAggregateAsync(Type type, FullName name);

		/// <summary>
		/// Reconstitutes the aggregate with the specified name from this repository
		/// </summary>
		/// <typeparam name="T">The type of aggregate to load</typeparam>
		/// <param name="name">The name of the aggregate to load</param>
		/// <returns>The work of loading the aggregate with the specified name</returns>
		Task<T> LoadAggregateAsync<T>(FullName name) where T : Notion, IAggregate;
	}

	[ContractClassFor(typeof(IRepository))]
	internal abstract class IRepositoryContract : IRepository
	{
		Task IRepository.SaveAggregateAsync(IAggregate aggregate)
		{
			Contract.Requires(aggregate != null);
			Contract.Ensures(Contract.Result<Task>() != null);

			return null;
		}

		Task<IAggregate> IRepository.LoadAggregateAsync(Type type, FullName name)
		{
			Contract.Requires(type != null);
			Contract.Requires(typeof(IAggregate).IsAssignableFrom(type));
			Contract.Requires(name != null);
			Contract.Requires(name != FullName.Anonymous);
			Contract.Ensures(Contract.Result<Task<IAggregate>>() != null);

			return null;
		}

		Task<T> IRepository.LoadAggregateAsync<T>(FullName name)
		{
			Contract.Requires(name != null);
			Contract.Requires(name != FullName.Anonymous);
			Contract.Ensures(Contract.Result<Task<T>>() != null);

			return null;
		}
	}
}