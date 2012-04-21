using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp
{
	/// <summary>
	/// A series of calculators which applies to <see cref="GraspRuntime"/>s
	/// </summary>
	public sealed class CompositeCalculator : ICalculator
	{
		/// <summary>
		/// Initializes a composite calculator with the specified calculators
		/// </summary>
		/// <param name="calculators">The series of calculators to be applied to runtimes</param>
		public CompositeCalculator(IEnumerable<ICalculator> calculators)
		{
			Contract.Requires(calculators != null);

			Calculators = calculators.ToList().AsReadOnly();
		}

		/// <summary>
		/// Initializes a composite calculator with the specified calculators
		/// </summary>
		/// <param name="calculators">The series of calculators to be applied to runtimes</param>
		public CompositeCalculator(params ICalculator[] calculators) : this(calculators as IEnumerable<ICalculator>)
		{}

		/// <summary>
		/// Gets the series of calculators to be applied to runtimes
		/// </summary>
		public ReadOnlyCollection<ICalculator> Calculators { get; private set; }

		/// <summary>
		/// Applies the series of calculators to the specified runtime
		/// </summary>
		/// <param name="runtime">A runtime to which the series of calculators is applied</param>
		public void ApplyCalculation(GraspRuntime runtime)
		{
			foreach(var calculator in Calculators)
			{
				calculator.ApplyCalculation(runtime);
			}
		}
	}
}