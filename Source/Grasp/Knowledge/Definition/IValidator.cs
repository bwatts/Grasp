using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Definition
{
	[ContractClass(typeof(IValidatorContract))]
	public interface IValidator
	{
		IValidationRule GetRule(Variable target);
	}

	[ContractClassFor(typeof(IValidator))]
	internal abstract class IValidatorContract : IValidator
	{
		IValidationRule IValidator.GetRule(Variable target)
		{
			Contract.Requires(target != null);
			Contract.Ensures(Contract.Result<IValidationRule>() != null);

			return null;
		}
	}
}