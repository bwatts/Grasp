using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Grasp.Compilation
{
	internal sealed class DependencyNode
	{
		internal DependencyNode(CalculationSchema calculation, IEnumerable<CalculationSchema> dependencies)
		{
			Calculation = calculation;
			Dependencies = dependencies.ToList().AsReadOnly();
		}

		internal CalculationSchema Calculation { get; private set; }

		internal ReadOnlyCollection<CalculationSchema> Dependencies { get; private set; }
	}
}