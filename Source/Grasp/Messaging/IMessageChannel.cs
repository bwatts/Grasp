using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Messaging
{
	/// <summary>
	/// Describes a channel along which messages travel in a publish/subscribe system
	/// </summary>
	[ContractClass(typeof(IMessageChannelContract))]
	public interface IMessageChannel
	{
		/// <summary>
		/// Publishes the specified message on this channel
		/// </summary>
		/// <param name="message">The message to publish on this channel</param>
		/// <returns>The work of publishing the message</returns>
		Task PublishAsync(Message message);
	}

	[ContractClassFor(typeof(IMessageChannel))]
	internal abstract class IMessageChannelContract : IMessageChannel
	{
		Task IMessageChannel.PublishAsync(Message message)
		{
			Contract.Requires(message != null);
			Contract.Ensures(Contract.Result<Task>() != null);

			return null;
		}
	}
}