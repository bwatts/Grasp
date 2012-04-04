using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp
{
	[ContractClass(typeof(ICalculatorContract))]
	public interface ICalculator
	{
		void ApplyCalculation(GraspRuntime runtime);
	}

	[ContractClassFor(typeof(ICalculator))]
	internal abstract class ICalculatorContract : ICalculator
	{
		void ICalculator.ApplyCalculation(GraspRuntime runtime)
		{
			Contract.Requires(runtime != null);
		}
	}
}