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
	public sealed class WorkHandler<TAggregate, TCommand> : Notion, IHandler<TCommand>
		where TAggregate : Notion, IAggregate
		where TCommand : Command
	{
		public static readonly Field<IRepository> _repositoryField = Field.On<WorkHandler<TAggregate, TCommand>>.For(x => x._repository);
		public static readonly Field<Func<TCommand, FullName>> _aggregateNameSelectorField = Field.On<WorkHandler<TAggregate, TCommand>>.For(x => x._aggregateNameSelector);

		private IRepository _repository { get { return GetValue(_repositoryField); } set { SetValue(_repositoryField, value); } }
		private Func<TCommand, FullName> _aggregateNameSelector { get { return GetValue(_aggregateNameSelectorField); } set { SetValue(_aggregateNameSelectorField, value); } }

		public WorkHandler(IRepository repository, Func<TCommand, FullName> aggregateNameSelector)
		{
			Contract.Requires(repository != null);
			Contract.Requires(aggregateNameSelector != null);

			_repository = repository;
			_aggregateNameSelector = aggregateNameSelector;
		}

		public async Task HandleAsync(TCommand command)
		{
			var name = _aggregateNameSelector(command);

			var aggregate = await _repository.LoadAggregateAsync<TAggregate>(name);

			if(aggregate == null)
			{
				throw new WorkException(Resources.NoAggregateWithName.FormatInvariant(name));
			}

			aggregate.HandleCommand(command);

			await _repository.SaveAggregateAsync(aggregate);
		}
	}
}