using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Hypermedia;
using Grasp.Hypermedia.Lists;
using Grasp.Lists;

namespace Grasp.Hypermedia.Server
{
	[ContractClass(typeof(IWorkItemStoreContract))]
	public interface IWorkItemStore
	{
		Task<Hyperlist> GetListAsync(ListViewKey viewKey = null);

		Task<WorkItemResource> GetWorkItemAsync(EntityId id);
	}

	[ContractClassFor(typeof(IWorkItemStore))]
	internal abstract class IWorkItemStoreContract : IWorkItemStore
	{
		Task<Hyperlist> IWorkItemStore.GetListAsync(ListViewKey viewKey)
		{
			Contract.Requires(viewKey != null);
			Contract.Ensures(Contract.Result<Task<Hyperlist>>() != null);

			return null;
		}

		Task<WorkItemResource> IWorkItemStore.GetWorkItemAsync(EntityId id)
		{
			Contract.Requires(id != EntityId.Unassigned);

			return null;
		}
	}
}