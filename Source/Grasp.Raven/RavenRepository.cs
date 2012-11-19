using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Cloak;
using Cloak.Linq;
using Grasp.Checks;
using Grasp.Messaging;
using Grasp.Work;
using Grasp.Work.Persistence;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Linq;

namespace Grasp.Raven
{
	public sealed class RavenRepository<TAggregate> : RavenContext, IRepository<TAggregate>, IPublisher where TAggregate : Aggregate
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
			return ExecuteWriteAsync(async session =>
			{
				foreach(var newEvent in ObserveAndStoreEvents(aggregate, session))
				{
					await this.AnnounceAsync(newEvent);
				}
			});
		}

		public Task<TAggregate> LoadAsync(EntityId aggregateId)
		{
			return ExecuteReadAsync(session =>
			{
				var aggregate = _activator.Activate<TAggregate>();

				var headVersion = LoadHeadVersion(aggregateId, session);

				aggregate.SetVersion(headVersion);

				return aggregate;
			});
		}

		private static IEnumerable<Event> ObserveAndStoreEvents(TAggregate aggregate, IDocumentSession session)
		{
			return new EventStorage(aggregate, session).StoreEvents();
		}

		private static AggregateVersion LoadHeadVersion(EntityId aggregateId, IDocumentSession session)
		{
			var aggregateDocumentId = GetAggregateDocumentId(aggregateId);

			AggregateVersion currentVersion = null;

			foreach(var revision in LoadRevisions(aggregateDocumentId, session))
			{
				currentVersion = currentVersion == null
					? new AggregateVersion(revision.Id, revision.Events)
					: new AggregateVersion(currentVersion, revision.Id, revision.Events);
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
				from revision in session.Query<Revision, Revisions_ByAggregateId>().Customize(c => c.WaitForNonStaleResults())
				where revision.AggregateId == aggregateId
				orderby revision.Number
				select revision;
		}

		private static string GetAggregateDocumentId(EntityId aggregateId)
		{
			// TODO: Is there a more preferred way to find the aggregate document ID?
			return DocumentConvention.DefaultTypeTagName(typeof(TAggregate)) + "/" + aggregateId.ToString();
		}

		private sealed class EventStorage
		{
			private readonly TAggregate _aggregate;
			private readonly IDocumentSession _session;
			private string _aggregateDocumentId;
			private Revision _currentRevision;
			private Revision _newRevision;

			internal EventStorage(TAggregate aggregate, IDocumentSession session)
			{
				_aggregate = aggregate;
				_session = session;
			}

			internal IEnumerable<Event> StoreEvents()
			{
				LoadCurrentRevision();

				CheckConcurrency();

				CreateRevisionAndAssignToAggregate();

				StoreAggregateAndRevision();

				return _newRevision.Events;
			}

			private void LoadCurrentRevision()
			{
				_aggregateDocumentId = GetAggregateDocumentId(_aggregate.Id);

				if(Check.That(_aggregateDocumentId).IsNotNullOrEmpty())
				{
					var revisions =
					from revision in _session.Query<Revision, Revisions_ByAggregateId>().Customize(c => c.WaitForNonStaleResults())
					where revision.AggregateId == _aggregateDocumentId
					orderby revision.Number descending
					select revision;

					_currentRevision = revisions.FirstOrDefault();
				}
			}

			private void CheckConcurrency()
			{
				if(_currentRevision != null && _currentRevision.Id != _aggregate.RevisionId)
				{
					throw new ConcurrencyException(Resources.ConcurrencyViolation.FormatInvariant(typeof(TAggregate), _aggregate.Id, _aggregate.RevisionId, _currentRevision.Id))
					{
						AggregateType = typeof(TAggregate),
						AggregateId = _aggregate.Id,
						ExpectedRevisionId = _aggregate.RevisionId,
						ActualRevisionId = _currentRevision.Id
					};
				}
			}

			private void CreateRevisionAndAssignToAggregate()
			{
				_newRevision = new Revision(
					_aggregateDocumentId,
					baseRevisionId: _currentRevision == null ? null : (EntityId?) _currentRevision.Id,
					number: 1 + (_currentRevision == null ? 0 : _currentRevision.Number),
					events: _aggregate.ObserveEvents());

				Aggregate.RevisionIdField.Set(_aggregate, _newRevision.Id);
			}

			private void StoreAggregateAndRevision()
			{
				_session.Store(_aggregate);

				_session.Store(_newRevision);
			}
		}
	}
}