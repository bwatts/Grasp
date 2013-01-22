using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;

namespace Grasp.Knowledge.Apps
{
	public abstract class EnvironmentCommand : Command
	{
		public static readonly Field<FullName> EnvironmentNameField = Field.On<EnvironmentCommand>.For(x => x.EnvironmentName);

		protected EnvironmentCommand(FullName workItemName, FullName environmentName) : base(workItemName)
		{
			Contract.Requires(environmentName != null);

			EnvironmentName = environmentName;
		}

		public FullName EnvironmentName { get { return GetValue(EnvironmentNameField); } private set { SetValue(EnvironmentNameField, value); } }
	}
}