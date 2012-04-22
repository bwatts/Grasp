using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Checks.Conditions
{
	/// <summary>
	/// Describes a set of conditions indexed by key
	/// </summary>
	[ContractClass(typeof(IConditionRepositoryContract))]
	public interface IConditionRepository
	{
		/// <summary>
		/// Gets the condition with the specified key, or null if there is no condition with a matching key
		/// </summary>
		/// <param name="key">The key of the condition to retrieve</param>
		/// <returns>The condition with the specified key, or null if there is no condition with a matching key</returns>
		Condition GetCondition(ConditionKey key);
	}

	[ContractClassFor(typeof(IConditionRepository))]
	internal abstract class IConditionRepositoryContract : IConditionRepository
	{
		Condition IConditionRepository.GetCondition(ConditionKey key)
		{
			Contract.Requires(key != null);

			return null;
		}
	}
}