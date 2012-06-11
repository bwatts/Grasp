using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Cloak.Autofac;
using Dash.Infrastructure;
using Dash.Windows.Presentation;
using Grasp.Knowledge;
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

			RegisterType<NamespaceExplorerView>().InstancePerDependency();

			RegisterType<EntityView>().InstancePerDependency();

			RegisterType<TypeExplorerView>().InstancePerDependency();

			Register<Func<TypeModel, TypeView>>(resolutionContext =>
			{
				var effectiveContext = resolutionContext.Resolve<IComponentContext>();

				return type => effectiveContext.Resolve<EntityView>(new TypedParameter(typeof(EntityModel), type));
			})
			.InstancePerDependency();

			Register<Func<Field, FieldView>>(c => field => FieldView.SingularOrPlural(field)).InstancePerDependency();
		}
	}
}