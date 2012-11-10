using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Autofac;
using Cloak.Autofac;
using Cloak.Time;
using Grasp.Raven;
using Grasp.Semantics;
using Grasp.Work;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;

namespace Slate.Http.Server.Composition
{
	public class RavenModule : BuilderModule
	{
		public RavenModule(string connectionStringName, Assembly indexAssembly)
		{
			Register(c =>
			{
				var documentStore = CreateDocumentStore(
					connectionStringName,
					new NotionJsonConverter(
						c.Resolve<ITimeContext>(),
						c.ResolveNamed<DomainModel>("Slate"),
						c.Resolve<INotionActivator>(),
						c.Resolve<IFieldValueConverter>()));

				documentStore.Initialize();

				IndexCreation.CreateIndexes(indexAssembly, documentStore);

				return documentStore;
			})
			.As<IDocumentStore>()
			.SingleInstance();

			RegisterGeneric(typeof(RavenRepository<>)).As(typeof(IRepository<>)).InstancePerDependency();
		}

		private static IDocumentStore CreateDocumentStore(string connectionStringName, NotionJsonConverter notionJsonConverter)
		{
			return new DocumentStore
			{
				ConnectionStringName = connectionStringName,
				Conventions =
				{
					CustomizeJsonSerializer = serializer => serializer.Converters.Add(notionJsonConverter)
				}
			};
		}
	}
}