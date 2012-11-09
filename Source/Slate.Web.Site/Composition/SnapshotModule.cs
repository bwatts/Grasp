using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Cloak.Autofac;
using Slate.Web.Site.Presentation.Home;

namespace Slate.Web.Site.Composition
{
	public class SnapshotModule : BuilderModule
	{
		public SnapshotModule()
		{
			RegisterType<SnapshotModelFactory>().As<ISnapshotModelFactory>().SingleInstance();
		}
	}
}