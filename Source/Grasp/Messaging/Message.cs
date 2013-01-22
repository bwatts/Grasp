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
	public abstract class Message : NamedNotion
	{
		public static readonly Field<IMessageChannel> ChannelField = Field.AttachedTo<Notion>.By<Message>.For(() => ChannelField);

		/// <summary>
		/// Gets the message channel specified by <see cref="ChannelField"/>. If unset, resolves the ambient message channel.
		/// </summary>
		/// <typeparam name="TPublisher">The type of object publising to the channel</typeparam>
		/// <param name="publisher">The object publishing to the channel</param>
		/// <returns>The message channel specified by <see cref="ChannelField"/>. If unset, resolves the ambient message channel.</returns>
		public static IMessageChannel Channel<TPublisher>(TPublisher publisher) where TPublisher : Notion, IPublisher
		{
			Contract.Requires(publisher != null);

			return ChannelField.Get(publisher) ?? AmbientMessageChannel.Resolve();
		}

		/// <summary>
		/// Initializes a message with the specified name
		/// </summary>
		/// <param name="name">The unique hierarchical name of the message. If null or <see cref="FullName.Anonymous"/>, defaults to the full name of the type.</param>
		protected Message(FullName name = default(FullName)) : base(name)
		{}
	}
}