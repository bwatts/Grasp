using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Messaging
{
	/// <summary>
	/// Describes a listener which observes messages of the specified type
	/// </summary>
	/// <typeparam name="TMessage">The type of messages observed by the subscriber</typeparam>
	[ContractClass(typeof(ISubscriberContract<>))]
	public interface ISubscriber<TMessage> where TMessage : Message
	{
		/// <summary>
		/// Observes the the specified message
		/// </summary>
		/// <param name="message">The message to observe</param>
		/// <returns>The work of observing the message</returns>
		Task ObserveAsync(TMessage message);
	}

	[ContractClassFor(typeof(ISubscriber<>))]
	internal abstract class ISubscriberContract<TMessage> : ISubscriber<TMessage> where TMessage : Message
	{
		Task ISubscriber<TMessage>.ObserveAsync(TMessage message)
		{
			Contract.Requires(message != null);
			Contract.Ensures(Contract.Result<Task>() != null);

			return null;
		}
	}
}