using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Cloak;
using Cloak.Linq;
using Grasp.Messaging;
using Grasp.Work;
using Raven.Client;
using Raven.Client.Linq;

namespace Grasp.Raven
{
	public sealed class RavenRepository<TAggregate> : RavenContext, IRepository<TAggregate> where TAggregate : Aggregate, new()
	{
		public RavenRepository(IDocumentStore documentStore) : base(documentStore)
		{}

		public Task SaveAsync(TAggregate aggregate, int expectedVersion)
		{
			return ExecuteWriteAsync(async session => await new EventStorage(session, aggregate, expectedVersion).StoreEventsAsync());
		}

		public Task<TAggregate> LoadAsync(Guid aggregateId)
		{
			return ExecuteReadAsync(session =>
			{
				var aggregate = new TAggregate();

				aggregate.SetVersion(LoadLatestVersion(aggregateId, session));

				return aggregate;
			});
		}

		private static AggregateVersion LoadLatestVersion(Guid aggregateId, IDocumentSession session)
		{
			AggregateVersion version = null;

			foreach(var versionDocument in LoadVersionDocuments(aggregateId, session))
			{
				version = version == null
					? new AggregateVersion(versionDocument.Number, versionDocument.Events)
					: new AggregateVersion(version, versionDocument.Number, versionDocument.Events);
			}

			return version ?? new AggregateVersion(1, Enumerable.Empty<Event>());
		}

		private static List<AggregateVersionDocument> LoadVersionDocuments(Guid aggregateId, IDocumentSession session)
		{
			var aggregateDocumentId = GetAggregateDocumentId(aggregateId);

			return session
				.Query<AggregateVersionDocument, AggregateVersionDocuments_ByAggregateDocumentId>()
				.Where(version => version.AggregateDocumentId == aggregateDocumentId)
				.ToList();
		}

		private static string GetAggregateDocumentId(Guid aggregateId)
		{
			return "aggregates/{0}".FormatInvariant(aggregateId);
		}

		private sealed class EventStorage
		{
			private readonly IDocumentSession _session;
			private readonly TAggregate _aggregate;
			private readonly int _expectedVersion;
			private string _aggregateDocumentId;
			private AggregateDocument _aggregateDocument;
			private int _newVersion;

			internal EventStorage(IDocumentSession session, TAggregate aggregate, int expectedVersion)
			{
				_session = session;
				_aggregate = aggregate;
				_expectedVersion = expectedVersion;
			}

			internal async Task StoreEventsAsync()
			{
				await InitializeAsync();

				StoreAggregateDocument();

				StoreNewVersion();
			}

			private async Task InitializeAsync()
			{
				_aggregateDocumentId = GetAggregateDocumentId(_aggregate.Id);

				_aggregateDocument = await LoadAggregateDocumentAsync();

				_newVersion = _aggregate.Version + 1;
			}

			private Task<AggregateDocument> LoadAggregateDocumentAsync()
			{
				return Task.Run(() =>
				{
					_aggregateDocument = _session.Load<AggregateDocument>(_aggregateDocumentId);

					if(_aggregateDocument.LatestVersion != _expectedVersion)
					{
						throw new ConcurrencyException(Resources.ConcurrencyViolation.FormatInvariant(typeof(TAggregate), _aggregate.Id, _expectedVersion, _aggregate.Version))
						{
							AggregateType = typeof(TAggregate),
							AggregateId = _aggregate.Id,
							ExpectedVersion = _expectedVersion,
							ActualVersion = _aggregate.Version
						};
					}

					return _aggregateDocument;
				});
			}

			private void StoreAggregateDocument()
			{
				_session.Store(new AggregateDocument
				{
					Id = GetAggregateDocumentId(_aggregate.Id),
					LatestVersion = _newVersion,
					VersionDocumentIds = GetVersionDocumentIds(_newVersion).ToList()
				});
			}

			private void StoreNewVersion()
			{
				_session.Store(_aggregate);

				_session.Store(new AggregateVersionDocument
				{
					Id = GetVersionDocumentId(_newVersion),
					AggregateDocumentId = _aggregateDocumentId,
					Number = _newVersion,
					Events = _aggregate.ObserveEvents().ToList()
				});
			}

			private IEnumerable<string> GetVersionDocumentIds(int newVersionNumber)
			{
				return _aggregateDocument.VersionDocumentIds.Concat(GetVersionDocumentId(newVersionNumber));
			}

			private string GetVersionDocumentId(int newVersionNumber)
			{
				return "aggregates/{0}/versions/{1}".FormatInvariant(_aggregate.Id, newVersionNumber);
			}
		}
	}
}