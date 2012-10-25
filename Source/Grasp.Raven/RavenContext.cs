using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client;

namespace Grasp.Raven
{
	public abstract class RavenContext : Notion
	{
		public static readonly Field<IDocumentStore> DocumentStoreField = Field.On<RavenContext>.For(x => x.DocumentStore);

		protected RavenContext(IDocumentStore documentStore)
		{
			Contract.Requires(documentStore != null);

			DocumentStore = documentStore;
		}

		protected IDocumentStore DocumentStore { get { return GetValue(DocumentStoreField); } private set { SetValue(DocumentStoreField, value); } }

		// I would prefer to use IAsyncDocumentSession here, but embedded mode does not support it yet:
		//
		// https://groups.google.com/d/topic/ravendb/gHaHQUb-rG8/discussion

		protected Task<TDocument> ExecuteReadAsync<TDocument>(Func<IDocumentSession, TDocument> read)
		{
			Contract.Requires(read != null);

			return Task.Run(() =>
			{
				var document = default(TDocument);

				using(var session = DocumentStore.OpenSession())
				{
					document = read(session);
				}

				return document;
			});
		}

		protected Task ExecuteWriteAsync(Action<IDocumentSession> write)
		{
			Contract.Requires(write != null);

			return Task.Run(() =>
			{
				using(var session = DocumentStore.OpenSession())
				{
					write(session);

					session.SaveChanges();
				}
			});
		}
	}
}