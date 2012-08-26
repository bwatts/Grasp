using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dash.Context
{
	[ContractClass(typeof(IHostableContract))]
	public interface IHostable
	{
		Task<ContextResult> StartAsync(CancellationToken cancellationToken);
	}

	[ContractClassFor(typeof(IHostable))]
	internal abstract class IHostableContract : IHostable
	{
		Task<ContextResult> IHostable.StartAsync(CancellationToken cancellationToken)
		{
			Contract.Ensures(Contract.Result<Task<ContextResult>>() != null);

			return null;
		}
	}
}