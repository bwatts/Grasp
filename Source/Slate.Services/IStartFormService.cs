using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Work;
using Grasp.Work.Items;

namespace Slate.Services
{
	[ContractClass(typeof(IStartFormServiceContract))]
	public interface IStartFormService
	{
		Task<WorkItem> StartFormAsync(string name);
	}

	[ContractClassFor(typeof(IStartFormService))]
	internal abstract class IStartFormServiceContract : IStartFormService
	{
		Task<WorkItem> IStartFormService.StartFormAsync(string name)
		{
			Contract.Requires(!String.IsNullOrEmpty(name));
			Contract.Ensures(Contract.Result<Task<WorkItem>>() != null);

			return null;
		}
	}
}