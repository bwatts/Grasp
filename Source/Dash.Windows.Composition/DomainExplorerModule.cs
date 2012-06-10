using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.Autofac;
using Dash.Windows.Presentation;
using Grasp.Semantics;
using Grasp.Semantics.Discovery;
using Grasp.Semantics.Discovery.Reflection;

namespace Dash.Windows.Composition
{
	public class DomainExplorerModule : BuilderModule
	{
		public DomainExplorerModule()
		{
			Register(c => new AssemblySource(new[] { typeof(DomainModel).Assembly })).As<IDomainModelSource>().SingleInstance();

			RegisterType<DomainExplorerView>().InstancePerDependency();

			RegisterType<DomainView>().InstancePerDependency();

			RegisterType<NamespaceView>().InstancePerDependency();
		}
	}
}