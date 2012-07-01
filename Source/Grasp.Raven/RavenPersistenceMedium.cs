using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak;
using Cloak.Linq;
using Grasp.Knowledge;
using Grasp.Knowledge.Persistence;
using Grasp.Knowledge.Work;
using Grasp.Semantics;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Linq;

namespace Grasp.Raven
{
	public class RavenPersistenceMedium : PersistenceMedium
	{
		public static readonly Field<DocumentStore> DocumentStoreField = Field.On<RavenPersistenceMedium>.Backing(x => x.DocumentStore);
		public static readonly Field<IDocumentSession> SessionField = Field.On<RavenPersistenceMedium>.Backing(x => x.Session);

		public DocumentStore DocumentStore { get { return GetValue(DocumentStoreField); } }
		public IDocumentSession Session { get { return GetValue(SessionField); } }

		public override IEntitySet<T> GetEntities<T>(DomainModel model)
		{
			var entities = new RavenEntitySet<T>();

			entities.SetValue(RavenEntitySet<T>.SessionField, Session);
			entities.SetValue(EntitySet<T>.QueryProviderField, GetQueryProvider<T>());

			return entities;
		}

		public override void CommitChanges(DomainModel model, ChangeSet changeSet)
		{
			Session.SaveChanges();
		}

		private IQueryProvider GetQueryProvider<TEntity>() where TEntity : Notion
		{
			return new EntitySetQueryProvider<TEntity>(Session.Query<TEntity>());
		}

		private sealed class RavenEntitySet<T> : EntitySet<T> where T : Notion
		{
			internal static readonly Field<IDocumentSession> SessionField = Field.On<RavenEntitySet<T>>.Backing(x => x.Session);

			internal IDocumentSession Session { get { return GetValue(SessionField); } }

			public override void AddOnCommit(T entity)
			{
				Session.Store(entity);
			}

			public override void RemoveOnCommit(T entity)
			{
				Session.Delete(entity);
			}
		}

		private sealed class EntitySetQueryProvider<TEntity> : QueryProvider where TEntity : Notion
		{
			private readonly IRavenQueryable<TEntity> _ravenQuery;

			internal EntitySetQueryProvider(IRavenQueryable<TEntity> ravenQuery)
			{
				_ravenQuery = ravenQuery;
			}

			// Expression is the entity set - skip and use query directly

			public override T Execute<T>(Expression expression)
			{
				return _ravenQuery.Provider.Execute<T>(_ravenQuery.Expression);
			}

			public override object Execute(Expression expression)
			{
				return _ravenQuery.Provider.Execute(_ravenQuery.Expression);
			}

			public override string GetQueryText(Expression expression)
			{
				return _ravenQuery.ToString();
			}
		}
	}
}