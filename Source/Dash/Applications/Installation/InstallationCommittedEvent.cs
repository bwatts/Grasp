﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Grasp.Messaging;

namespace Dash.Applications.Installation
{
	public class InstallationCommittedEvent : EnvironmentEvent
	{
		public static readonly Field<FullName> ApplicationNameField = Field.On<InstallationCommittedEvent>.For(x => x.ApplicationName);

		public InstallationCommittedEvent(EntityId workItemId, EntityId environmentId, FullName applicationName) : base(workItemId, environmentId)
		{
			Contract.Requires(applicationName != null);

			ApplicationName = applicationName;
		}

		public FullName ApplicationName { get { return GetValue(ApplicationNameField); } private set { SetValue(ApplicationNameField, value); } }
	}
}