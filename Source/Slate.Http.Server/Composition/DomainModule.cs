using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Cloak.Autofac;
using Grasp;
using Grasp.Semantics;
using Grasp.Semantics.Discovery.Reflection;
using Grasp.Work;

namespace Slate.Http.Server.Composition
{
	public class DomainModule : BuilderModule
	{
		public DomainModule(IEnumerable<Assembly> assemblies)
		{
			Contract.Requires(assemblies != null);

			Register(c => assemblies.BindDomainModel("Slate").BindDomainModel()).Named<DomainModel>("Slate").SingleInstance();

			RegisterType<FakeNotionContext>().As<INotionContext>().InstancePerDependency();

			RegisterType<NotionActivator>().As<INotionActivator>().SingleInstance();

			RegisterType<FieldValueConverter>().As<IFieldValueConverter>().SingleInstance();
		}

		public DomainModule(params Assembly[] assemblies) : this(assemblies as IEnumerable<Assembly>)
		{}




		private sealed class FakeNotionContext : INotionContext
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