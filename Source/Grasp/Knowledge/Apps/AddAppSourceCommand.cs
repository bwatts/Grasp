using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Apps
{
	public class AddAppSourceCommand : EnvironmentCommand
	{
		public static readonly Field<AppSource> AppSourceField = Field.On<AddAppSourceCommand>.For(x => x.AppSource);

		public AddAppSourceCommand(FullName workItemName, FullName environmentName, AppSource appSource) : base(workItemName, environmentName)
		{
			Contract.Requires(appSource != null);

			AppSource = appSource;
		}

		public AppSource AppSource { get { return GetValue(AppSourceField); } private set { SetValue(AppSourceField, value); } }
	}
}