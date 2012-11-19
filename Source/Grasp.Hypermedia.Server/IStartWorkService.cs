using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;

namespace Grasp.Hypermedia.Server
{
	/// <summary>
	/// Describes an application service which starts work items that track command status
	/// </summary>
	[ContractClass(typeof(IStartWorkServiceContract))]
	public interface IStartWorkService
	{
		Task<EntityId> StartWorkAsync(string description, TimeSpan retryInterval, Func<EntityId, IMessageChannel, Task> issueCommandAsync);
	}

	[ContractClassFor(typeof(IStartWorkService))]
	internal abstract class IStartWorkServiceContract : IStartWorkService
	{
		Task<EntityId> IStartWorkService.StartWorkAsync(string description, TimeSpan retryInterval, Func<EntityId, IMessageChannel, Task> issueCommandAsync)
		{
			Contract.Requires(description != null);
			Contract.Requires(retryInterval >= TimeSpan.Zero);
			Contract.Requires(issueCommandAsync != null);
			Contract.Ensures(Contract.Result<Task<Guid>>() != null);

			return null;
		}
	}
}