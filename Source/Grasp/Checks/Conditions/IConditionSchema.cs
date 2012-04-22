using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Checks.Conditions
{
	/// <summary>
	/// Describes a context in which a set of conditions are in effect
	/// </summary>
	[ContractClass(typeof(IConditionSchemaContract))]
	public interface IConditionSchema
	{
		/// <summary>
		/// Gets a specification that applies the condition with the specified name to the specified target object
		/// </summary>
		/// <typeparam name="T">The type of the target object</typeparam>
		/// <param name="target">The object to which the condition is applied</param>
		/// <param name="conditionName">The name of the condition to apply to the specified target object</param>
		/// <returns>A specification which applies the condition with the specified name to the specified target object</returns>
		Specification<T> GetSpecification<T>(T target, string conditionName);
	}

	[ContractClassFor(typeof(IConditionSchema))]
	internal abstract class IConditionSchemaContract : IConditionSchema
	{
		Specification<T> IConditionSchema.GetSpecification<T>(T target, string conditionName)
		{
			Contract.Requires(conditionName != null);
			Contract.Ensures(Contract.Result<Specification<T>>() != null);

			return null;
		}
	}
}