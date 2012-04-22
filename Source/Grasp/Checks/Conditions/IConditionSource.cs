using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Checks.Conditions
{
	/// <summary>
	/// Describes a source from which conditions are queried and integrated into a schema
	/// </summary>
	[ContractClass(typeof(IConditionSourceContract))]
	public interface IConditionSource
	{
		/// <summary>
		/// Gets the conditions in effect for this source
		/// </summary>
		/// <returns>The conditions in effect for this source</returns>
		IEnumerable<Condition> GetConditions();
	}

	[ContractClassFor(typeof(IConditionSource))]
	internal abstract class IConditionSourceContract : IConditionSource
	{
		IEnumerable<Condition> IConditionSource.GetConditions()
		{
			Contract.Ensures(Contract.Result<IEnumerable<Condition>>() != null);

			return null;
		}
	}
}