using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Git
{
	[ContractClass(typeof(IWorkspaceContract))]
	public interface IWorkspace
	{
		ITimelineFile OpenTimeline();

		ITimelineFile OpenEnvironmentTimeline();

		ITimelineFile OpenAggregateTimeline(FullName name, Type type);

		Task CommitToRepositoryAsync(string message);
	}

	[ContractClassFor(typeof(IWorkspace))]
	internal abstract class IWorkspaceContract : IWorkspace
	{
		ITimelineFile IWorkspace.OpenTimeline()
		{
			Contract.Ensures(Contract.Result<ITimelineFile>() != null);

			return null;
		}

		ITimelineFile IWorkspace.OpenEnvironmentTimeline()
		{
			Contract.Ensures(Contract.Result<ITimelineFile>() != null);

			return null;
		}

		ITimelineFile IWorkspace.OpenAggregateTimeline(FullName name, Type type)
		{
			Contract.Requires(name != null);
			Contract.Requires(type != null);
			Contract.Ensures(Contract.Result<ITimelineFile>() != null);

			return null;
		}

		Task IWorkspace.CommitToRepositoryAsync(string message)
		{
			Contract.Requires(message != null);
			Contract.Ensures(Contract.Result<Task>() != null);

			return null;
		}
	}
}