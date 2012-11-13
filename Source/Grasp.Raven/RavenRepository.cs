﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Cloak;
using Cloak.Linq;
using Grasp.Messaging;
using Grasp.Work;
using Grasp.Work.Persistence;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Linq;

namespace Grasp.Raven
{
	public sealed class RavenRepository<TAggregate> : RavenContext, IRepository<TAggregate> where TAggregate : Aggregate
	{
		public static readonly Field<INotionActivator> _activatorField = Field.On<RavenRepository<TAggregate>>.For(x => x._activator);

		private INotionActivator _activator { get { return GetValue(_activatorField); } set { SetValue(_activatorField, value); } }

		public RavenRepository(IDocumentStore documentStore, INotionActivator activator) : base(documentStore)
		{
			Contract.Requires(activator != null);

			_activator = activator;
		}

		public Task SaveAsync(TAggregate aggregate)
		{
			return ExecuteWriteAsync(session => new EventStorage(aggregate, session).StoreEvents());
		}

		public Task<TAggregate> LoadAsync(Guid aggregateId)
		{
			return ExecuteReadAsync(session =>
			{
				var aggregate = _activator.Activate<TAggregate>();

				aggregate.SetVersion(LoadHeadVersion(aggregateId, session));

				return aggregate;
			});
		}

		private static AggregateVersion LoadHeadVersion(Guid aggregateId, IDocumentSession session)
		{
			// TODO: Is there a more preferred way to find the aggregate document ID?
			var aggregateDocumentId = DocumentConvention.DefaultTypeTagName(typeof(TAggregate)) + "/" + aggregateId.ToString("N").ToUpper();

			AggregateVersion currentVersion = null;

			foreach(var revision in LoadRevisions(aggregateDocumentId, session))
			{
				var revisionId = GetRevisionId(revision.Id);

				currentVersion = currentVersion == null
					? new AggregateVersion(revisionId, revision.Events)
					: new AggregateVersion(currentVersion, revisionId, revision.Events);
			}

			if(currentVersion == null)
			{
				throw new GraspException(Resources.AggregateHasNoHistory.FormatInvariant(aggregateDocumentId));
			}

			return currentVersion;
		}

		private static IEnumerable<Revision> LoadRevisions(string aggregateId, IDocumentSession session)
		{
			return
				from revision in session.Query<Revision, Revisions_ByAggregateId>()
				where revision.AggregateId == aggregateId
				orderby revision.Number
				select revision;
		}

		private static Guid GetRevisionId(string revisionDocumentId)
		{
			var idText = revisionDocumentId.Substring("Revisions/".Length);

			return Guid.ParseExact(idText, "N");
		}

		private static string GetRevisionDocumentId(Guid revisionId)
		{
			return "Revisions/" + revisionId.ToString("N").ToUpper();
		}

		private sealed class EventStorage
		{
			private readonly TAggregate _aggregate;
			private readonly IDocumentSession _session;
			private string _aggregateId;
			private TAggregate _existingAggregate;
			private Revision _existingRevision;
			private Revision _newRevision;

			internal EventStorage(TAggregate aggregate, IDocumentSession session)
			{
				_aggregate = aggregate;
				_session = session;
			}

			internal void StoreEvents()
			{
				LoadExistingAggregate();

				CheckConcurrency();

				CreateRevisionAndAssignToAggregate();

				StoreAggregateAndRevision();
			}

			private void LoadExistingAggregate()
			{
				_aggregateId = _session.Advanced.GetDocumentId(_aggregate);

				_existingAggregate = _session.Load<TAggregate>(_aggregateId);

				_existingRevision = _existingAggregate == null ? null : _session.Load<Revision>(_existingAggregate.RevisionId);
			}

			private void CheckConcurrency()
			{
				if(_existingAggregate != null && _existingAggregate.RevisionId != _aggregate.RevisionId)
				{
					throw new ConcurrencyException(Resources.ConcurrencyViolation.FormatInvariant(typeof(TAggregate), _aggregate.Id, _aggregate.RevisionId, _existingAggregate.RevisionId))
					{
						AggregateType = typeof(TAggregate),
						AggregateId = _aggregate.Id,
						ExpectedRevisionId = _aggregate.RevisionId,
						ActualRevisionId = _existingAggregate.RevisionId
					};
				}
			}

			private void CreateRevisionAndAssignToAggregate()
			{
				var newRevisionId = Guid.NewGuid();

				_newRevision = new Revision(
					GetRevisionDocumentId(newRevisionId),
					_aggregateId,
					GetRevisionDocumentId(_aggregate.RevisionId),
					_existingRevision.Number + 1,
					_aggregate.ObserveEvents());

				Aggregate.RevisionIdField.Set(_aggregate, newRevisionId);
			}

			private void StoreAggregateAndRevision()
			{
				_session.Store(_aggregate);

				_session.Store(_newRevision);
			}
		}
	}
}