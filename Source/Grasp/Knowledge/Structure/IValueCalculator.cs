using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Structure
{
	[ContractClass(typeof(IValueCalculatorContract))]
	public interface IValueCalculator
	{
		Calculation GetCalculation(Variable target);
	}

	[ContractClassFor(typeof(IValueCalculator))]
	internal abstract class IValueCalculatorContract : IValueCalculator
	{
		Calculation IValueCalculator.GetCalculation(Variable target)
		{
			Contract.Requires(target != null);
			Contract.Ensures(Contract.Result<Calculation>() != null);

			return null;
		}
	}
}