using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Work.Persistence;

namespace Grasp.Messaging
{
	/// <summary>
	/// A message which travels along channels in a reactive system
	/// </summary>
	public abstract class Message : PersistentNotion<Guid>
	{
		public static readonly Field<IMessageChannel> ChannelField = Field.AttachedTo<Notion>.By<Message>.For(() => ChannelField);

		public static IMessageChannel Channel<TPublisher>(TPublisher publisher) where TPublisher : Notion, IPublisher
		{
			Contract.Requires(publisher != null);

			return ChannelField.Get(publisher) ?? AmbientMessageChannel.Resolve();
		}
	}
}