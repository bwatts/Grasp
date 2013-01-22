using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;

namespace Grasp.Knowledge.Apps
{
	public abstract class EnvironmentEvent : Event
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