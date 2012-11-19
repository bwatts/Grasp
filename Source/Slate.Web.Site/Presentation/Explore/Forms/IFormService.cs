using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Grasp;
using Grasp.Hypermedia;
using Slate.Http;

namespace Slate.Web.Site.Presentation.Explore.Forms
{
	[ContractClass(typeof(IFormServiceContract))]
	public interface IFormService
	{
		Task<FormResource> GetFormAsync(EntityId id);

		Task<WorkItemResource> StartFormAsync(string name);
	}

	[ContractClassFor(typeof(IFormService))]
	internal abstract class IFormServiceContract : IFormService
	{
		Task<FormResource> IFormService.GetFormAsync(EntityId id)
		{
			Contract.Requires(id != EntityId.Unassigned);
			Contract.Ensures(Contract.Result<Task<FormResource>>() != null);

			return null;
		}

		Task<WorkItemResource> IFormService.StartFormAsync(string name)
		{
			Contract.Requires(name != null);
			Contract.Ensures(Contract.Result<Task<WorkItemResource>>() != null);

			return null;
		}
	}
}