using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.Autofac;
using Grasp.Raven;
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
				var documentStore = new DocumentStore { ConnectionStringName = connectionStringName };

				documentStore.Initialize();

				IndexCreation.CreateIndexes(indexAssembly, documentStore);

				return documentStore;
			})
			.As<IDocumentStore>()
			.SingleInstance();

			RegisterGeneric(typeof(RavenRepository<>)).As(typeof(IRepository<>)).InstancePerDependency();
		}
	}
}