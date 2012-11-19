using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using Autofac;
using Cloak.Autofac;
using Cloak.Reflection;
using Cloak.Time;
using Grasp;
using Grasp.Raven;
using Grasp.Semantics;
using Grasp.Work;
using Grasp.Work.Persistence;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;

namespace Slate.Http.Server.Composition
{
	public class RavenModule : BuilderModule
	{
		public RavenModule(string connectionStringName, params Assembly[] indexAssemblies)
		{
			Contract.Requires(connectionStringName != null);
			Contract.Requires(indexAssemblies != null);

			RegisterGeneric(typeof(RavenRepository<>)).As(typeof(IRepository<>)).InstancePerDependency();

			Register(c =>
			{
				var documentStore = InitializeDocumentStore(connectionStringName, c.Resolve<IComponentContext>());

				foreach(var indexAssembly in indexAssemblies)
				{
					IndexCreation.CreateIndexes(indexAssembly, documentStore);
				}

				return documentStore;
			})
			.As<IDocumentStore>()
			.SingleInstance();
		}

		private static IDocumentStore InitializeDocumentStore(string connectionStringName, IComponentContext context)
		{
			var documentStore = CreateDocumentStore(connectionStringName, context);

			SetConventions(documentStore.Conventions);

			documentStore.Initialize();

			return documentStore;
		}

		private static IDocumentStore CreateDocumentStore(string connectionStringName, IComponentContext context)
		{
			var jsonConverter = new NotionJsonConverter(
				json => new JsonState(json, context.Resolve<IFieldValueConverter>()),
				context.Resolve<INotionActivator>());

			return new DocumentStore
			{
				ConnectionStringName = connectionStringName,
				Conventions =
				{
					CustomizeJsonSerializer = serializer => serializer.Converters.Add(jsonConverter)
				}
			};
		}

		private static void SetConventions(DocumentConvention conventions)
		{
			conventions.FindIdentityProperty =
				property => property.DeclaringType.IsAssignableFromGenericDefinition(typeof(PersistentNotion<>)) && property.Name == "Id";

			conventions.IdentityTypeConvertors = conventions.IdentityTypeConvertors.Concat(new[] { new RavenEntityIdConverter() }).ToList();
		}
	}
}