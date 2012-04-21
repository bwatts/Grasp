using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Cloak;

namespace Grasp.Analysis.Compilation
{
	internal sealed class GraspCompiler
	{
		private readonly GraspSchema _schema;
		private readonly ISet<Variable> _variables;
		private readonly IList<CalculationSchema> _calculations;

		internal GraspCompiler(GraspSchema schema)
		{
			_schema = schema;

			_calculations = schema.Calculations.Select(calculation => new CalculationSchema(calculation)).ToList();

			_variables = new HashSet<Variable>(schema.Variables.Concat(_calculations.Select(calculation => calculation.OutputVariable)));
		}

		internal GraspExecutable Compile()
		{
			ValidateCalculations();

			return new GraspExecutable(_schema, GetCalculator());
		}

		private void ValidateCalculations()
		{
			foreach(var calculation in _calculations)
			{
				EnsureVariablesExistInSchema(calculation);

				EnsureAssignableToOutputVariable(calculation);
			}
		}

		private void EnsureVariablesExistInSchema(CalculationSchema calculation)
		{
			foreach(var variable in calculation.Variables)
			{
				if(!_variables.Contains(variable))
				{
					throw new InvalidCalculationVariableException(
						variable,
						calculation.Source,
						_schema,
						Resources.InvalidCalculationVariable.FormatInvariant(variable, calculation));
				}
			}
		}

		private void EnsureAssignableToOutputVariable(CalculationSchema calculation)
		{
			if(!calculation.OutputVariable.Type.IsAssignableFrom(calculation.Expression.Type))
			{
				throw new InvalidCalculationResultTypeException(
					calculation.Source,
					Resources.InvalidCalculationResultType.FormatInvariant(
						calculation.Expression.Type,
						calculation,
						calculation.OutputVariable.Type,
						calculation.OutputVariable));
			}
		}

		private ICalculator GetCalculator()
		{
			return _calculations.Count == 1 ? GetCalculator(_calculations.Single()) : GetCalculators();
		}

		private static ICalculator GetCalculator(CalculationSchema schema)
		{
			return new CalculationCompiler().CompileCalculation(schema);
		}

		private ICalculator GetCalculators()
		{
			return new CompositeCalculator(OrderCalculatorsByDependency());
		}

		private IEnumerable<ICalculator> OrderCalculatorsByDependency()
		{
			return _calculations.OrderByDependency().Select(GetCalculator);
		}
	}
}