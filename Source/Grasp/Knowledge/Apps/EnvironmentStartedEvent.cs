using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Apps
{
	public class EnvironmentStartedEvent : EnvironmentEvent
	{
		public static readonly Field<string> TitleField = Field.On<EnvironmentStartedEvent>.For(x => x.Title);

		public EnvironmentStartedEvent(FullName workItemName, FullName environmentName, string title) : base(workItemName, environmentName)
		{
			Contract.Requires(title != null);

			Title = title;
		}

		public string Title { get { return GetValue(TitleField); } private set { SetValue(TitleField, value); } }
	}
}