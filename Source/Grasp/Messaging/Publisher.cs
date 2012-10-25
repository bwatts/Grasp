using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Messaging
{
	/// <summary>
	/// Grants derived classes access to the ambient message channel
	/// </summary>
	public abstract class Publisher : Notion, IPublisher
	{
		protected Task AnnounceAsync(Event @event)
		{
			return this.AnnounceAsync(@event);
		}

		protected Task IssueAsync(Command command)
		{
			return this.IssueAsync(command);
		}
	}
}