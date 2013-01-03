using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Grasp.Messaging;

namespace Grasp.Work
{
	public sealed class WorkHandler<TCommand, TAggregate> : Notion, IHandler<TCommand>
		where TCommand : Command
		where TAggregate : Aggregate
	{
		public static readonly Field<IRepository<TAggregate>> _repositoryField = Field.On<WorkHandler<TCommand, TAggregate>>.For(x => x._repository);
		public static readonly Field<Func<TCommand, EntityId>> _aggregateIdSelectorField = Field.On<WorkHandler<TCommand, TAggregate>>.For(x => x._aggregateIdSelector);

		private IRepository<TAggregate> _repository { get { return GetValue(_repositoryField); } set { SetValue(_repositoryField, value); } }
		private Func<TCommand, EntityId> _aggregateIdSelector { get { return GetValue(_aggregateIdSelectorField); } set { SetValue(_aggregateIdSelectorField, value); } }

		public WorkHandler(IRepository<TAggregate> repository, Func<TCommand, EntityId> aggregateIdSelector)
		{
			Contract.Requires(repository != null);
			Contract.Requires(aggregateIdSelector != null);

			_repository = repository;
			_aggregateIdSelector = aggregateIdSelector;
		}

		public async Task HandleAsync(TCommand c)
		{
			var id = _aggregateIdSelector(c);

			var aggregate = await _repository.LoadAsync(id);

			if(aggregate == null)
			{
				throw new WorkException(Resources.NoAggregateWithId.FormatInvariant(typeof(TAggregate), id));
			}

			aggregate.HandleCommand(c);

			await _repository.SaveAsync(aggregate);
		}
	}
}