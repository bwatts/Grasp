using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Cloak;

namespace Grasp.Knowledge.Runtime.Compilation
{
	internal sealed class SchemaCompiler
	{
		private readonly Schema _schema;
		private readonly IList<CalculationSchema> _calculations;
		private readonly IDictionary<FullName, Variable> _variablesByName;

		internal SchemaCompiler(Schema schema)
		{
			_schema = schema;

			_calculations = schema.Calculations.Select(calculation => new CalculationSchema(calculation)).ToList();

			_variablesByName = schema.EffectiveVariables.ToDictionary(variable => variable.Name);
		}

		internal Executable Compile()
		{
			ValidateCalculations();

			return new Executable(_schema, GetCalculator());
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
				Variable knownVariable;

				if(!_variablesByName.TryGetValue(variable.Name, out knownVariable))
				{
					throw new InvalidCalculationVariableException(
						_schema,
						variable,
						calculation.Source,
						Resources.InvalidCalculationVariable.FormatInvariant(variable, calculation));
				}

				if(knownVariable.Type != variable.Type)
				{
					throw new InvalidCalculationVariableException(
						_schema,
						variable,
						calculation.Source,
						Resources.CalculationVariableTypeDifferent.FormatInvariant(variable, calculation, variable.Type, knownVariable.Type));
				}
			}
		}

		private void EnsureAssignableToOutputVariable(CalculationSchema calculation)
		{
			if(!calculation.OutputVariable.Type.IsAssignableFrom(calculation.Expression.Type))
			{
				throw new InvalidCalculationResultTypeException(
					_schema,
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
			return _calculations.OrderByDependency(_schema).Select(GetCalculator);
		}
	}
}