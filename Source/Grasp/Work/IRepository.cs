using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Work
{
	/// <summary>
	/// Describes storage for aggregates of the specified type
	/// </summary>
	/// <typeparam name="TAggregate">The type of aggregate in the repository</typeparam>
	[ContractClass(typeof(IRepositoryContract<>))]
	public interface IRepository<TAggregate> where TAggregate : Aggregate
	{
		Task SaveAsync(TAggregate aggregate);

		Task<TAggregate> LoadAsync(EntityId aggregateId);
	}

	[ContractClassFor(typeof(IRepository<>))]
	internal abstract class IRepositoryContract<TAggregate> : IRepository<TAggregate> where TAggregate : Aggregate
	{
		Task IRepository<TAggregate>.SaveAsync(TAggregate aggregate)
		{
			Contract.Requires(aggregate != null);
			Contract.Ensures(Contract.Result<Task>() != null);

			return null;
		}

		Task<TAggregate> IRepository<TAggregate>.LoadAsync(EntityId aggregateId)
		{
			Contract.Requires(aggregateId != EntityId.Unassigned);
			Contract.Ensures(Contract.Result<Task<TAggregate>>() != null);

			return null;
		}
	}
}