using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;

namespace Grasp.Work.Items
{
	[ContractClass(typeof(IStartWorkServiceContract))]
	public interface IStartWorkService
	{
		Task<Uri> StartWorkAsync(string description, TimeSpan retryInterval, Func<Guid, IMessageChannel, Task> issueWorkCommandAsync);
	}

	[ContractClassFor(typeof(IStartWorkService))]
	internal abstract class IStartWorkServiceContract : IStartWorkService
	{
		Task<Uri> IStartWorkService.StartWorkAsync(string description, TimeSpan retryInterval, Func<Guid, IMessageChannel, Task> issueWorkCommandAsync)
		{
			Contract.Requires(description != null);
			Contract.Requires(retryInterval >= TimeSpan.Zero);
			Contract.Requires(issueWorkCommandAsync != null);
			Contract.Ensures(Contract.Result<Task<Uri>>() != null);

			return null;
		}
	}
}