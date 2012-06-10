using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Dash.Infrastructure
{
	[ContractClass(typeof(IDashContextContract))]
	public interface IDashContext
	{
		void AddTopic(ITopicSource source, Topic topic);

		void RemoveTopic(Topic topic);
	}

	[ContractClassFor(typeof(IDashContext))]
	internal abstract class IDashContextContract : IDashContext
	{
		void IDashContext.AddTopic(ITopicSource source, Topic topic)
		{
			Contract.Requires(source != null);
			Contract.Requires(topic != null);
		}

		void IDashContext.RemoveTopic(Topic topic)
		{
			Contract.Requires(topic != null);
		}
	}
}