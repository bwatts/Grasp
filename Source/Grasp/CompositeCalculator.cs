using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp
{
	public sealed class CompositeCalculator : ICalculator
	{
		public CompositeCalculator(IEnumerable<ICalculator> calculators)
		{
			Contract.Requires(calculators != null);

			Calculators = calculators.ToList().AsReadOnly();
		}

		public CompositeCalculator(params ICalculator[] calculators) : this(calculators as IEnumerable<ICalculator>)
		{}

		public ReadOnlyCollection<ICalculator> Calculators { get; private set; }

		public void ApplyCalculation(GraspRuntime runtime)
		{
			foreach(var calculator in Calculators)
			{
				calculator.ApplyCalculation(runtime);
			}
		}
	}
}