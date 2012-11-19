using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Work.Items;

namespace Grasp.Hypermedia.Server
{
	[ContractClass(typeof(IWorkItemByIdQueryContract))]
	public interface IWorkItemByIdQuery
	{
		Task<WorkItem> GetWorkItemAsync(EntityId id);
	}

	[ContractClassFor(typeof(IWorkItemByIdQuery))]
	internal abstract class IWorkItemByIdQueryContract : IWorkItemByIdQuery
	{
		Task<WorkItem> IWorkItemByIdQuery.GetWorkItemAsync(EntityId id)
		{
			Contract.Requires(id != EntityId.Unassigned);
			Contract.Ensures(Contract.Result<Task<WorkItem>>() != null);

			return null;
		}
	}
}