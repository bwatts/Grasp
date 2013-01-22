using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Apps
{
	public class AppSourceAddedEvent : EnvironmentEvent
	{
		public static readonly Field<AppSource> AddSourceField = Field.On<AppSourceAddedEvent>.For(x => x.AppSource);

		public AppSourceAddedEvent(FullName workItemName, FullName environmentName, AppSource appSource) : base(workItemName, environmentName)
		{
			Contract.Requires(appSource != null);

			AppSource = appSource;
		}

		public AppSource AppSource { get { return GetValue(AddSourceField); } private set { SetValue(AddSourceField, value); } }
	}
}