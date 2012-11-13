using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Cloak.Autofac;
using Cloak.Time;
using Grasp;
using Grasp.Semantics;
using Grasp.Semantics.Discovery.Reflection;
using Grasp.Work.Persistence;

namespace Slate.Http.Server.Composition
{
	public class PersistenceModule : BuilderModule
	{
		public PersistenceModule(IEnumerable<Assembly> assemblies)
		{
			Contract.Requires(assemblies != null);

			Register(c => assemblies.BindDomainModel("Slate").BindDomainModel()).Named<DomainModel>("Slate").SingleInstance();

			RegisterType<IsolatedNotionContext>().As<INotionContext>().InstancePerDependency();

			Register(c => new NotionActivator(c.Resolve<ITimeContext>(), c.ResolveNamed<DomainModel>("Slate"), c.Resolve<Func<INotionContext>>()))
			.As<INotionActivator>()
			.SingleInstance();

			RegisterType<FieldValueConverter>().As<IFieldValueConverter>().SingleInstance();
		}

		public PersistenceModule(params Assembly[] assemblies) : this(assemblies as IEnumerable<Assembly>)
		{}

		private sealed class IsolatedNotionContext : INotionContext
		{
			private readonly IDictionary<Field, object> _values = new Dictionary<Field, object>();

			public IEnumerable<Grasp.FieldBinding> GetBindings(Notion notion)
			{
				return _values.Select(pair => pair.Key.Bind(pair.Value));
			}

			public object GetValue(Notion notion, Field field)
			{
				object value;

				_values.TryGetValue(field, out value);

				return value;
			}

			public bool TryGetValue(Notion notion, Field field, out object value)
			{
				return _values.TryGetValue(field, out value);
			}

			public void SetValue(Notion notion, Field field, object value)
			{
				_values[field] = value;
			}
		}
	}
}