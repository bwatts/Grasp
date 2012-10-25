using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Messaging
{
	/// <summary>
	/// Sends event and command messages along the ambient message channel
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class Publishing
	{
		public static Task AnnounceAsync(this IPublisher publisher, Event @event)
		{
			Contract.Requires(publisher != null);
			Contract.Requires(@event != null);

			return Message.Channel(@event).SendAsync(@event);
		}

		public static Task IssueAsync(this IPublisher publisher, Command command)
		{
			Contract.Requires(publisher != null);
			Contract.Requires(command != null);

			return Message.Channel(command).SendAsync(command);
		}
	}
}