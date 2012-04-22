using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Checks.Conditions
{
	/// <summary>
	/// Describes the declaration of conditions that apply to a target type
	/// </summary>
	[ContractClass(typeof(IConditionDeclarationContract))]
	public interface IConditionDeclaration
	{
		/// <summary>
		/// Gets the conditions declared for the specified target type
		/// </summary>
		/// <param name="targetType">The target type for which to get conditions</param>
		/// <returns>The conditions declared for the specified target type</returns>
		IEnumerable<Condition> GetConditions(Type targetType);
	}

	[ContractClassFor(typeof(IConditionDeclaration))]
	internal abstract class IConditionDeclarationContract : IConditionDeclaration
	{
		IEnumerable<Condition> IConditionDeclaration.GetConditions(Type targetType)
		{
			Contract.Requires(targetType != null);
			Contract.Ensures(Contract.Result<IEnumerable<Condition>>() != null);

			return null;
		}
	}
}