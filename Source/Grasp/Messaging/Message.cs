using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Messaging
{
	/// <summary>
	/// A message which travels along channels in a reactive system
	/// </summary>
	public abstract class Message : UniqueNotion
	{
		public static IMessageChannel Channel(Notion publisher)
		{
			Contract.Requires(publisher != null);

			// TODO: Remove default value in favor of metadata approach below
			return ChannelField.Get(publisher) ?? new DebugChannel();
		}

		// TODO: Metadata specifying a default value of a channel that writes to Debug
		public static readonly Field<IMessageChannel> ChannelField = Field.AttachedTo<Notion>.By<Message>.For(() => ChannelField);

		protected Message(Guid? id = null) : base(id)
		{}

		private sealed class DebugChannel : IMessageChannel
		{
			public Task SendAsync(Message message)
			{
				return Task.Run(() => Debug.WriteLine(
					"Published message of type {0} with ID {1} and field bindings [{2}]",
					message.GetType(),
					message.Id,
					String.Join("] [", message.GetBindings())));
			}

			public Task<DateTime> SendAsync(Func<DateTime, Message> messageSelector)
			{
				return Task.Run(() =>
				{
					var when = DateTime.Now;

					SendAsync(messageSelector(when));

					return when;
				});
			}
		}
	}
}