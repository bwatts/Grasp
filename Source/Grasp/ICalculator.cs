using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp
{
	/// <summary>
	/// Describes a calculation which applies to <see cref="GraspRuntime"/>s
	/// </summary>
	[ContractClass(typeof(ICalculatorContract))]
	public interface ICalculator
	{
		/// <summary>
		/// Applies the encapsulated calculation to the specified runtime
		/// </summary>
		/// <param name="runtime">A runtime to which the encapsulated calculation is applied</param>
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