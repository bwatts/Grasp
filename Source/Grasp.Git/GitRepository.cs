using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;
using Grasp.Persistence;
using Grasp.Work;
using LibGit2Sharp;

namespace Grasp.Git
{
	public sealed class GitRepository : Publisher, IRepository
	{
		public static readonly Field<IWorkspace> _workspaceField = Field.On<GitRepository>.For(x => x._workspace);
		public static readonly Field<INotionActivator> _activatorField = Field.On<GitRepository>.For(x => x._activator);

		private IWorkspace _workspace { get { return GetValue(_workspaceField); } set { SetValue(_workspaceField, value); } }
		private INotionActivator _activator { get { return GetValue(_activatorField); } set { SetValue(_activatorField, value); } }

		public GitRepository(IWorkspace workspace, INotionActivator activator)
		{
			Contract.Requires(workspace != null);
			Contract.Requires(activator != null);

			_workspace = workspace;
			_activator = activator;
		}

		public async Task SaveAggregateAsync(IAggregate aggregate)
		{
			var newEvents = await CommitRevisionAsync(aggregate);

			foreach(var newEvent in newEvents)
			{
				await AnnounceAsync(newEvent);
			}
		}

		public async Task<IAggregate> LoadAggregateAsync(Type type, FullName name)
		{
			var file = _workspace.OpenAggregateTimeline(name, type);

			var pastEvents = await file.ReadEventsAsync();

			var aggregate = (IAggregate) _activator.Activate(type);

			foreach(var pastEvent in pastEvents)
			{
				aggregate.ObserveEvent(pastEvent);
			}

			return aggregate;
		}

		public async Task<T> LoadAggregateAsync<T>(FullName name) where T : Notion, IAggregate
		{
			var file = _workspace.OpenAggregateTimeline(name, typeof(T));

			var pastEvents = await file.ReadEventsAsync();

			var aggregate = _activator.Activate<T>();

			foreach(var pastEvent in pastEvents)
			{
				aggregate.ObserveEvent(pastEvent);
			}

			return aggregate;
		}

		private async Task<IList<Event>> CommitRevisionAsync(IAggregate aggregate)
		{
			var newEvents = await RetrieveAndStoreNewEventsAsync(aggregate);

			await _workspace.CommitToRepositoryAsync(String.Format(
				"Saved {0} event{1} to aggregate {2}",
				newEvents.Count,
				(newEvents.Count == 1 ? "" : "s"),
				aggregate.Name));

			return newEvents;
		}

		private async Task<IList<Event>> RetrieveAndStoreNewEventsAsync(IAggregate aggregate)
		{
			var file = _workspace.OpenAggregateTimeline(aggregate.Name, aggregate.GetType());

			var newEvents = aggregate.RetrieveNewEvents();

			await file.AppendEventsAsync(newEvents);

			return newEvents;
		}
	}
}