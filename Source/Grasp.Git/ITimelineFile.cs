using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;
using Grasp.Work;

namespace Grasp.Git
{
	[ContractClass(typeof(ITimelineFileContract))]
	public interface ITimelineFile
	{
		Task AppendEventsAsync(IEnumerable<Event> events);

		Task<List<Event>> ReadEventsAsync();
	}

	[ContractClassFor(typeof(ITimelineFile))]
	internal abstract class ITimelineFileContract : ITimelineFile
	{
		Task ITimelineFile.AppendEventsAsync(IEnumerable<Event> events)
		{
			Contract.Requires(events != null);
			Contract.Ensures(Contract.Result<Task>() != null);

			return null;
		}

		Task<List<Event>> ITimelineFile.ReadEventsAsync()
		{
			Contract.Ensures(Contract.Result<Task<List<Event>>>() != null);

			return null;
		}
	}
}