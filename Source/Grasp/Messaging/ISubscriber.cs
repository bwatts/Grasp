using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Messaging
{
	/// <summary>
	/// Describes a listener which observes events of the specified type
	/// </summary>
	/// <typeparam name="TEvent">The type of observed events</typeparam>
	[ContractClass(typeof(ISubscriberContract<>))]
	public interface ISubscriber<TEvent> where TEvent : Event
	{
		/// <summary>
		/// Observes the the specified event
		/// </summary>
		/// <param name="e">The event to observe</param>
		/// <returns>The work of observing the event</returns>
		Task ObserveAsync(TEvent e);
	}

	[ContractClassFor(typeof(ISubscriber<>))]
	internal abstract class ISubscriberContract<TEvent> : ISubscriber<TEvent> where TEvent : Event
	{
		Task ISubscriber<TEvent>.ObserveAsync(TEvent e)
		{
			Contract.Requires(e != null);
			Contract.Ensures(Contract.Result<Task>() != null);

			return null;
		}
	}
}