using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Work;

namespace Grasp.Messaging
{
	/// <summary>
	/// A message which travels along channels in a reactive system
	/// </summary>
	public abstract class Message : PersistentNotion<Guid>
	{
		public static IMessageChannel Channel<TPublisher>(TPublisher publisher) where TPublisher : Notion, IPublisher
		{
			Contract.Requires(publisher != null);

			// TODO: Remove default value in favor of metadata approach below
			return ChannelField.Get(publisher) ?? new DebugChannel();
		}

		// TODO: Metadata specifying a default value of a channel read from the current thread context (how to propagate? set attached field on first access?)
		public static readonly Field<IMessageChannel> ChannelField = Field.AttachedTo<Notion>.By<Message>.For(() => ChannelField);

		private sealed class DebugChannel : IMessageChannel
		{
			public Task PublishAsync(Message message)
			{
				return Task.Run(() => Debug.WriteLine(
					"Published message of type {0} with ID {1} and field bindings [{2}]",
					message.GetType(),
					message.Id,
					String.Join("] [", message.GetBindings())));
			}
		}
	}
}