using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;

namespace Grasp.Work
{
	public sealed class CreateHandler<TCommand, TAggregate> : Notion, IHandler<TCommand>
		where TCommand : Command
		where TAggregate : Aggregate
	{
		public static readonly Field<IRepository<TAggregate>> _repositoryField = Field.On<CreateHandler<TCommand, TAggregate>>.For(x => x._repository);
		public static readonly Field<Func<TCommand, TAggregate>> _aggregateSelectorField = Field.On<CreateHandler<TCommand, TAggregate>>.For(x => x._aggregateSelector);

		private IRepository<TAggregate> _repository { get { return GetValue(_repositoryField); } set { SetValue(_repositoryField, value); } }
		private Func<TCommand, TAggregate> _aggregateSelector { get { return GetValue(_aggregateSelectorField); } set { SetValue(_aggregateSelectorField, value); } }

		public CreateHandler(IRepository<TAggregate> repository, Func<TCommand, TAggregate> aggregateSelector)
		{
			Contract.Requires(repository != null);
			Contract.Requires(aggregateSelector != null);

			_repository = repository;
			_aggregateSelector = aggregateSelector;
		}

		public async Task HandleAsync(TCommand c)
		{
			var aggregate = _aggregateSelector(c);

			await _repository.SaveAsync(aggregate);
		}
	}
}