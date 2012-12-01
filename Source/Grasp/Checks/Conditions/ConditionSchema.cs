using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Checks.Rules;

namespace Grasp.Checks.Conditions
{
	/// <summary>
	/// A context in which a set of conditions are in effect
	/// </summary>
	public sealed class ConditionSchema : Notion, IConditionSchema
	{
		public static readonly Field<ISpecificationProvider> _specificationProviderField = Field.On<ConditionSchema>.For(x => x._specificationProvider);
		public static readonly Field<IConditionRepository> _conditionRepositoryField = Field.On<ConditionSchema>.For(x => x._conditionRepository);

		private ISpecificationProvider _specificationProvider { get { return GetValue(_specificationProviderField); } set { SetValue(_specificationProviderField, value); } }
		private IConditionRepository _conditionRepository { get { return GetValue(_conditionRepositoryField); } set { SetValue(_conditionRepositoryField, value); } }

		/// <summary>
		/// Initializes a schema with the specified condition repository
		/// </summary>
		/// <param name="conditionRepository">The repository which indexes conditions by key</param>
		public ConditionSchema(IConditionRepository conditionRepository)
		{
			Contract.Requires(conditionRepository != null);

			_conditionRepository = conditionRepository;

			_specificationProvider = new ConditionSchemaSpecificationProvider();
		}

		/// <summary>
		/// Gets a specification that applies the condition with the specified name to the specified target object
		/// </summary>
		/// <typeparam name="T">The type of the target object</typeparam>
		/// <param name="target">The object to which the condition is applied</param>
		/// <param name="conditionName">The name of the condition to apply to the specified target object</param>
		/// <returns>A specification which applies the condition with the specified name to the specified target object</returns>
		public Specification<T> GetSpecification<T>(T target, string conditionName)
		{
			return GetSpecification(target, new ConditionKey(typeof(T), conditionName));
		}

		private Specification<T> GetSpecification<T>(T target, ConditionKey conditionKey)
		{
			return new Specification<T>(target, _specificationProvider, GetRule(conditionKey));
		}

		private Rule GetRule(ConditionKey conditionKey)
		{
			var condition = _conditionRepository.GetCondition(conditionKey);

			return condition == null ? Rule.True : condition.Rule;
		}
	}
}