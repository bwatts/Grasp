using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Cloak;

namespace Grasp.Knowledge.Runtime.Compilation
{
	internal static class DependencyAnalyzer
	{
		internal static IEnumerable<CalculationSchema> OrderByDependency(this IEnumerable<CalculationSchema> calculations, Schema schema)
		{
			return GetGraph(calculations, schema).OrderCalculations();
		}

		private static DependencyGraph GetGraph(IEnumerable<CalculationSchema> calculations, Schema schema)
		{
			return new DependencyGraph(GetNodes(calculations), schema);
		}

		private static IEnumerable<DependencyNode> GetNodes(IEnumerable<CalculationSchema> calculations)
		{
			return
				from calculation in calculations
				let dependencies =
					from possibleDependency in calculations
					where possibleDependency != calculation
					where IsDependency(calculation, possibleDependency)
					select possibleDependency
				select new DependencyNode(calculation, dependencies);
		}

		private static bool IsDependency(CalculationSchema calculation, CalculationSchema possibleDependency)
		{
			return calculation.VariableNames.Contains(possibleDependency.OutputVariable.Name);
		}
	}
}