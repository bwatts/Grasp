using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Dash.Infrastructure
{
	[ContractClass(typeof(IBoardContextContract))]
	public interface IBoardContext
	{
		void AddTopic(ITopicSource source, Topic topic);

		void RemoveTopic(Topic topic);
	}

	[ContractClassFor(typeof(IBoardContext))]
	internal abstract class IBoardContextContract : IBoardContext
	{
		void IBoardContext.AddTopic(ITopicSource source, Topic topic)
		{
			Contract.Requires(source != null);
			Contract.Requires(topic != null);
		}

		void IBoardContext.RemoveTopic(Topic topic)
		{
			Contract.Requires(topic != null);
		}
	}
}