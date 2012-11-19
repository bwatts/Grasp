using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Grasp;
using Grasp.Hypermedia;

namespace Slate.Web.Site.Presentation.Work
{
	[ContractClass(typeof(IWorkServiceContract))]
	public interface IWorkService
	{
		Task<WorkItemResource> GetWorkItemAsync(EntityId id);
	}

	[ContractClassFor(typeof(IWorkService))]
	internal abstract class IWorkServiceContract : IWorkService
	{
		Task<WorkItemResource> IWorkService.GetWorkItemAsync(EntityId id)
		{
			Contract.Requires(id != EntityId.Unassigned);
			Contract.Ensures(Contract.Result<Task<WorkItemResource>>() != null);

			return null;
		}
	}
}