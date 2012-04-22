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
	public sealed class ConditionSchema : IConditionSchema
	{
		private readonly ISpecificationProvider _specificationProvider = new ConditionSchemaSpecificationProvider();
		private readonly IConditionRepository _conditionRepository;

		/// <summary>
		/// Initializes a schema with the specified condition repository
		/// </summary>
		/// <param name="conditionRepository">The repository which indexes conditions by key</param>
		public ConditionSchema(IConditionRepository conditionRepository)
		{
			Contract.Requires(conditionRepository != null);

			_conditionRepository = conditionRepository;
		}

		#region IConditionSchema
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
		#endregion

		private Specification<T> GetSpecification<T>(T target, ConditionKey conditionKey)
		{
			return new Specification<T>(target, _specificationProvider, GetRule(conditionKey));
		}

		private Rule GetRule(ConditionKey conditionKey)
		{
			var condition = _conditionRepository.GetCondition(conditionKey);

			return condition == null ? Rule.Constant(true) : condition.Rule;
		}
	}
}