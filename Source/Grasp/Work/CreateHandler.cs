using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;

namespace Grasp.Work
{
	public sealed class CreateHandler<TCommand> : Notion, IHandler<TCommand> where TCommand : Command
	{
		public static readonly Field<IRepository> _repositoryField = Field.On<CreateHandler<TCommand>>.For(x => x._repository);
		public static readonly Field<Func<TCommand, IAggregate>> _aggregateSelectorField = Field.On<CreateHandler<TCommand>>.For(x => x._aggregateSelector);

		private IRepository _repository { get { return GetValue(_repositoryField); } set { SetValue(_repositoryField, value); } }
		private Func<TCommand, IAggregate> _aggregateSelector { get { return GetValue(_aggregateSelectorField); } set { SetValue(_aggregateSelectorField, value); } }

		public CreateHandler(IRepository repository, Func<TCommand, IAggregate> aggregateSelector)
		{
			Contract.Requires(repository != null);
			Contract.Requires(aggregateSelector != null);

			_repository = repository;
			_aggregateSelector = aggregateSelector;
		}

		public async Task HandleAsync(TCommand command)
		{
			var aggregate = _aggregateSelector(command);

			await _repository.SaveAggregateAsync(aggregate);
		}
	}
}