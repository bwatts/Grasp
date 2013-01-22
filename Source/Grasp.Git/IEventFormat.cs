using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;

namespace Grasp.Git
{
	[ContractClass(typeof(IEventFormatContract))]
	public interface IEventFormat
	{
		Task WriteEventsAsync(Stream stream, bool hasEvents, IEnumerable<Event> events);

		Task<IEnumerable<Event>> ReadEventsAsync(Stream stream);
	}

	[ContractClassFor(typeof(IEventFormat))]
	internal abstract class IEventFormatContract : IEventFormat
	{
		Task IEventFormat.WriteEventsAsync(Stream stream, bool hasEvents, IEnumerable<Event> events)
		{
			Contract.Requires(stream != null);
			Contract.Requires(events != null);
			Contract.Ensures(Contract.Result<Task>() != null);

			return null;
		}

		Task<IEnumerable<Event>> IEventFormat.ReadEventsAsync(Stream stream)
		{
			Contract.Requires(stream != null);
			Contract.Ensures(Contract.Result<Task<IEnumerable<Event>>>() != null);

			return null;
		}
	}
}