using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;

namespace Grasp.Knowledge
{
	/// <summary>
	/// Builds instances of <see cref="Schema"/>
	/// </summary>
	public sealed class SchemaBuilder : Builder<Schema>, IEnumerable
	{
		private readonly List<Variable> _variables = new List<Variable>();
		private readonly List<Calculation> _calculations = new List<Calculation>();

		/// <summary>
		/// Initializes a builder with the specified root namespace
		/// </summary>
		/// <param name="rootNamespace">The namespace to prepend to all variable names</param>
		public SchemaBuilder(Namespace rootNamespace)
		{
			Contract.Requires(rootNamespace != null);

			RootNamespace = rootNamespace;
		}

		/// <summary>
		/// Gets the namespace to prepend to all variable names
		/// </summary>
		public Namespace RootNamespace { get; private set; }

		/// <summary>
		/// Creates a schema with the current variables and calculations in the root namespace
		/// </summary>
		/// <returns>A schema with the current variables and calculations in the root namespace</returns>
		protected override Schema CreateInstance()
		{
			return new Schema(
				_variables.Select(variable => new Variable(variable.Type, GetRootedName(variable.Name))),
				_calculations.Select(calculation => new Calculation(
					new Variable(calculation.OutputVariable.Type, GetRootedName(calculation.OutputVariable.Name)),
					calculation.Expression)));
		}

		/// <summary>
		/// Gets the specified identifier in the root namespace
		/// </summary>
		/// <param name="identifier">The identifier as it appears in the root namespace</param>
		/// <returns>A full name with the specified identifier in the root namespace</returns>
		public FullName GetRootedName(Identifier identifier)
		{
			Contract.Requires(identifier != null);

			return RootNamespace + identifier;
		}

		/// <summary>
		/// Gets the specified full name in the root namespace
		/// </summary>
		/// <param name="fullName">The full name as it appears in the root namespace</param>
		/// <returns>A full name with the specified full name in the root namespace</returns>
		public FullName GetRootedName(FullName fullName)
		{
			Contract.Requires(fullName != null);

			return fullName.Namespace == RootNamespace ? fullName : RootNamespace + fullName;
		}

		/// <summary>
		/// Adds the specified variable to the schema
		/// </summary>
		/// <param name="variable">The variable to add to the schema</param>
		public void Add(Variable variable)
		{
			Contract.Requires(variable != null);

			_variables.Add(variable);
		}

		/// <summary>
		/// Adds the specified variables to the schema
		/// </summary>
		/// <param name="variables">The variables to add to the schema</param>
		public void Add(IEnumerable<Variable> variables)
		{
			Contract.Requires(variables != null);

			_variables.AddRange(variables);

			ClearInstance();
		}

		/// <summary>
		/// Adds the specified variable to the schema
		/// </summary>
		/// <param name="variables">The variable to add to the schema</param>
		public void Add(params Variable[] variables)
		{
			Add(variables as IEnumerable<Variable>);
		}

		/// <summary>
		/// Adds a variable with the specified type and identifier in the root namespace
		/// </summary>
		/// <param name="type">The type of value represented by the variable</param>
		/// <param name="identifier">The identifier of the variable as it appears in the root namespace</param>
		public Variable Add(Type type, Identifier identifier)
		{
			Contract.Requires(type != null);
			Contract.Requires(identifier != null);

			var variable = new Variable(type, RootNamespace + identifier);

			Add(variable);

			return variable;
		}

		/// <summary>
		/// Adds a variable with the specified type and full name in the root namespace
		/// </summary>
		/// <param name="type">The type of value represented by the variable</param>
		/// <param name="fullName">The full name of the variable as it appears in the root namespace</param>
		public Variable Add(Type type, FullName fullName)
		{
			Contract.Requires(type != null);
			Contract.Requires(fullName != null);

			var variable = new Variable(type, RootNamespace + fullName);

			Add(variable);

			return variable;
		}

		/// <summary>
		/// Adds the specified calculation to the schema
		/// </summary>
		/// <param name="calculation">The calculation to add to the schema</param>
		public void Add(Calculation calculation)
		{
			Contract.Requires(calculation != null);

			_calculations.Add(calculation);

			ClearInstance();
		}

		/// <summary>
		/// Adds the specified calculations to the schema
		/// </summary>
		/// <param name="calculations">The calculations to add to the schema</param>
		public void Add(IEnumerable<Calculation> calculations)
		{
			Contract.Requires(calculations != null);

			_calculations.AddRange(calculations);

			ClearInstance();
		}

		/// <summary>
		/// Adds the specified calculations to the schema
		/// </summary>
		/// <param name="calculations">The calculations to add to the schema</param>
		public void Add(params Calculation[] calculations)
		{
			Add(calculations as IEnumerable<Calculation>);
		}

		/// <summary>
		/// Merges the variables and calculations of the specified schema
		/// </summary>
		/// <param name="other">The schema to merge with this schema</param>
		public void Add(Schema other)
		{
			Contract.Requires(other != null);

			Add(other.Variables);
			Add(other.Calculations);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			// Only exists to support the collection initializer syntax

			yield break;
		}
	}
}