using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge
{
	public static class SchemaMerging
	{
		public static IEnumerable<Variable> Merge(this IEnumerable<Variable> variables, IEnumerable<Variable> otherVariables, SchemaMergeRule rule = SchemaMergeRule.ErrorOnConflict)
		{
			Contract.Requires(variables != null);
			Contract.Requires(otherVariables != null);

			var variableNameSets =
				from variable in variables.Union(otherVariables)
				group variable by variable.Name into variablesByName
				select variablesByName.ToList();

			foreach(var variableNameSet in variableNameSets)
			{
				if(variableNameSet.Count == 1)
				{
					yield return variableNameSet[0];
				}
				else if(variableNameSet.Count == 2)
				{
					yield return MergeVariable(variableNameSet[0], variableNameSet[1], rule);
				}
				else
				{
					throw new ArgumentException();	// TODO
				}
			}
		}

		public static IEnumerable<Calculation> Merge(this IEnumerable<Calculation> calculations, IEnumerable<Calculation> otherCalculations, SchemaMergeRule rule = SchemaMergeRule.ErrorOnConflict)
		{
			Contract.Requires(calculations != null);
			Contract.Requires(otherCalculations != null);

			var outputVariableSets =
				from calculation in calculations.Union(otherCalculations)
				group calculation by calculation.OutputVariable.Name into calculationsByOutputName
				select calculationsByOutputName.ToList();

			foreach(var outputVariableSet in outputVariableSets)
			{
				if(outputVariableSet.Count == 1)
				{
					yield return outputVariableSet[0];
				}
				else if(outputVariableSet.Count == 2)
				{
					yield return MergeCalculation(outputVariableSet[0], outputVariableSet[1], rule);
				}
				else
				{
					throw new ArgumentException();	// TODO
				}
			}
		}

		public static Schema Merge(this Schema schema, Schema other, SchemaMergeRule variableRule = SchemaMergeRule.ErrorOnConflict, SchemaMergeRule calculationRule = SchemaMergeRule.ErrorOnConflict)
		{
			Contract.Requires(schema != null);
			Contract.Requires(other != null);

			return new Schema(
				schema.Variables.Merge(other.Variables, variableRule),
				schema.Calculations.Merge(other.Calculations, calculationRule));
		}

		public static Schema Merge(this IEnumerable<Schema> schemas, SchemaMergeRule variableRule = SchemaMergeRule.ErrorOnConflict, SchemaMergeRule calculationRule = SchemaMergeRule.ErrorOnConflict)
		{
			Contract.Requires(schemas != null);

			return schemas.DefaultIfEmpty(Schema.Empty).Aggregate((left, right) => left.Merge(right, variableRule, calculationRule));
		}

		private static Variable MergeVariable(Variable left, Variable right, SchemaMergeRule rule)
		{
			switch(rule)
			{
				case SchemaMergeRule.ErrorOnConflict:
					throw new ArgumentException();	// TODO
				case SchemaMergeRule.UseLeft:
					return left;
				case SchemaMergeRule.UseRight:
					return right;
				default:
					throw new NotSupportedException();	// TODO
			}
		}

		private static Calculation MergeCalculation(Calculation left, Calculation right, SchemaMergeRule rule)
		{
			switch(rule)
			{
				case SchemaMergeRule.ErrorOnConflict:
					throw new ArgumentException();	// TODO
				case SchemaMergeRule.UseLeft:
					return left;
				case SchemaMergeRule.UseRight:
					return right;
				default:
					throw new NotSupportedException();	// TODO
			}
		}
	}
}