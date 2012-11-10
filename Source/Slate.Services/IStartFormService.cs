using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slate.Services
{
	[ContractClass(typeof(IStartFormServiceContract))]
	public interface IStartFormService
	{
		Task<Guid> StartFormAsync(string name);
	}

	[ContractClassFor(typeof(IStartFormService))]
	internal abstract class IStartFormServiceContract : IStartFormService
	{
		Task<Guid> IStartFormService.StartFormAsync(string name)
		{
			Contract.Requires(!String.IsNullOrEmpty(name));
			Contract.Ensures(Contract.Result<Task<Guid>>() != null);

			return null;
		}
	}
}