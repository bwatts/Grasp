using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;
using Grasp.Work;

namespace Grasp.Knowledge.Apps
{
	public abstract class EnvironmentEvent : WorkItemEvent
	{
		public static readonly Field<FullName> EnvironmentNameField = Field.On<EnvironmentEvent>.For(x => x.EnvironmentName);

		protected EnvironmentEvent(FullName workItemName, FullName environmentName) : base(workItemName)
		{
			Contract.Requires(environmentName != null);

			EnvironmentName = environmentName;
		}

		public FullName EnvironmentName { get { return GetValue(EnvironmentNameField); } private set { SetValue(EnvironmentNameField, value); } }
	}
}