using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;
using Grasp.Work;

namespace Grasp.Persistence
{
	public sealed class ExcludedFieldSet : IExcludedFieldSet
	{
		public bool IsExcluded(Field field)
		{
			return field == PersistentId.ValueField
				|| field == Lifetime.ReconstitutedEventField
				|| field == Topic._newEventsField
				|| field == Message.ChannelField;
		}
	}
}