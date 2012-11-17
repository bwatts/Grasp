using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Grasp.Hypermedia;

namespace Slate.Web.Site.Presentation.Work
{
	[ContractClass(typeof(IWorkServiceContract))]
	public interface IWorkService
	{
		Task<WorkItemResource> GetWorkItemAsync(Guid id);
	}

	[ContractClassFor(typeof(IWorkService))]
	internal abstract class IWorkServiceContract : IWorkService
	{
		Task<WorkItemResource> IWorkService.GetWorkItemAsync(Guid id)
		{
			Contract.Requires(id != Guid.Empty);
			Contract.Ensures(Contract.Result<Task<WorkItemResource>>() != null);

			return null;
		}
	}
}