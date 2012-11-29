using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Definition
{
	[ContractClass(typeof(IValidationRuleContract))]
	public interface IValidationRule
	{
		Calculation<bool> GetCalculation(Variable target);
	}

	[ContractClassFor(typeof(IValidationRule))]
	internal abstract class IValidationRuleContract : IValidationRule
	{
		Calculation<bool> IValidationRule.GetCalculation(Variable target)
		{
			Contract.Requires(target != null);
			Contract.Ensures(Contract.Result<Calculation<bool>>() != null);

			return null;
		}
	}
}