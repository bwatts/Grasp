using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Grasp.Hypermedia;
using Slate.Http;

namespace Slate.Web.Site.Presentation.Explore.Forms
{
	[ContractClass(typeof(IFormServiceContract))]
	public interface IFormService
	{
		Task<FormResource> GetFormAsync(Guid id);

		Task<WorkItemResource> StartFormAsync(string name);
	}

	[ContractClassFor(typeof(IFormService))]
	internal abstract class IFormServiceContract : IFormService
	{
		Task<FormResource> IFormService.GetFormAsync(Guid id)
		{
			Contract.Requires(id != Guid.Empty);
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