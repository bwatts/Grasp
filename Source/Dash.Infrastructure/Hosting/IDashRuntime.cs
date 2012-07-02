using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dash.Infrastructure.Hosting
{
	[ContractClass(typeof(IDashRuntimeContract))]
	public interface IDashRuntime
	{
		Task<RuntimeResult> RunAsync(CancellationToken cancellationToken);
	}

	[ContractClassFor(typeof(IDashRuntime))]
	internal abstract class IDashRuntimeContract : IDashRuntime
	{
		Task<RuntimeResult> IDashRuntime.RunAsync(CancellationToken cancellationToken)
		{
			Contract.Ensures(Contract.Result<Task<RuntimeResult>>() != null);

			return null;
		}
	}
}