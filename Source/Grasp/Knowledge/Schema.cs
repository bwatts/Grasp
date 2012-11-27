using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using Grasp.Knowledge.Runtime.Compilation;

namespace Grasp.Knowledge
{
	/// <summary>
	/// A context in which a set of variables and a set of calculations are in effect
	/// </summary>
	public class Schema : Notion
	{
		public static readonly Field<Many<Variable>> VariablesField = Field.On<Schema>.For(x => x.Variables);
		public static readonly Field<Many<Calculation>> CalculationsField = Field.On<Schema>.For(x => x.Calculations);

		/// <summary>
		/// Initializes a schema with the specified variables and calculations
		/// </summary>
		/// <param name="variables">The variables in effect for this schema</param>
		/// <param name="calculations">The calculations in effect for this schema</param>
		public Schema(IEnumerable<Variable> variables = null, IEnumerable<Calculation> calculations = null)
		{
			Variables = (variables ?? Enumerable.Empty<Variable>()).ToMany();
			Calculations = (calculations ?? Enumerable.Empty<Calculation>()).ToMany();
		}

		/// <summary>
		/// Gets the variables in effect for this schema
		/// </summary>
		public Many<Variable> Variables { get { return GetValue(VariablesField); } private set { SetValue(VariablesField, value); } }

		/// <summary>
		/// Gets the calculations in effect for this schema
		/// </summary>
		public Many<Calculation> Calculations { get { return GetValue(CalculationsField); } private set { SetValue(CalculationsField, value); } }

		/// <summary>
		/// Gets an executable version of this schema which applies its calculations
		/// </summary>
		/// <returns>An executable version of this schema which applies its calculations</returns>
		/// <exception cref="CompilationException">
		/// Throw if any calculation references a variable that is not defined in either <see cref="Variables"/> or as the output variable of another calculation
		/// -or- any calculation's result type is not assignable to its output variable type -or- any calculation contains a cycle -or- any calculation contains
		/// an invalid expression tree -or- there is any other error while compiling this schema
		/// </exception>
		public Executable Compile()
		{
			try
			{
				return new SchemaCompiler(this).Compile();
			}
			catch(Exception ex)
			{
				throw new CompilationException(this, Resources.CompilationError, ex);
			}
		}

		/// <summary>
		/// Merges the variables and calculations of this and the specified schemas, using the specified rules to resolve conflicts
		/// </summary>
		/// <param name="other">The schema to merge</param>
		/// <param name="variableRule">Determines how to resolve conflicts between variables</param>
		/// <param name="calculationRule">Determines how to resolve conflicts between calculations</param>
		/// <returns>A schema containing the variables and calculations of this and the specified schemas</returns>
		public Schema Merge(Schema other, SchemaMergeRule variableRule = default(SchemaMergeRule), SchemaMergeRule calculationRule = default(SchemaMergeRule))
		{
			Contract.Requires(other != null);

			return new Schema(MergeVariables(other, variableRule), MergeCalculations(other, calculationRule));
		}

		private IEnumerable<Variable> MergeVariables(Schema other, SchemaMergeRule rule)
		{
			var variableSets =
				from variable in Variables.Union(other.Variables)
				group variable by variable.Name into variablesByName
				select variablesByName.ToList();

			foreach(var variableSet in variableSets)
			{
				if(variableSet.Count == 1)
				{
					yield return variableSet[0];
				}
				else if(variableSet.Count == 2)
				{
					yield return MergeVariable(rule, variableSet[0], variableSet[1]);
				}
				else
				{
					throw new ArgumentException();	// TODO
				}
			}
		}

		private static Variable MergeVariable(SchemaMergeRule rule, Variable left, Variable right)
		{
			switch(rule)
			{
				case SchemaMergeRule.ErrorOnConflict:
					throw new ArgumentException();	// TODO
				case SchemaMergeRule.PreferLeft:
					return left;
				case SchemaMergeRule.PreferRight:
					return right;
				default:
					throw new NotSupportedException();	// TODO
			}
		}

		private IEnumerable<Calculation> MergeCalculations(Schema other, SchemaMergeRule rule)
		{
			var calculationSets =
				from calculation in Calculations.Union(other.Calculations)
				group calculation by calculation.OutputVariable.Name into calculationsByOutputName
				select calculationsByOutputName.ToList();

			foreach(var calculationSet in calculationSets)
			{
				if(calculationSet.Count == 1)
				{
					yield return calculationSet[0];
				}
				else if(calculationSet.Count == 2)
				{
					yield return MergeCalculation(rule, calculationSet[0], calculationSet[1]);
				}
				else
				{
					throw new ArgumentException();	// TODO
				}
			}
		}

		private static Calculation MergeCalculation(SchemaMergeRule rule, Calculation left, Calculation right)
		{
			switch(rule)
			{
				case SchemaMergeRule.ErrorOnConflict:
					throw new ArgumentException();	// TODO
				case SchemaMergeRule.PreferLeft:
					return left;
				case SchemaMergeRule.PreferRight:
					return right;
				default:
					throw new NotSupportedException();	// TODO
			}
		}
	}
}