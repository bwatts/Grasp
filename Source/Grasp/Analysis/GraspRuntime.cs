using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Cloak;

namespace Grasp.Analysis
{
	/// <summary>
	/// A context in which a set of variables are bound to values
	/// </summary>
	public class GraspRuntime
	{
		private readonly Dictionary<Variable, VariableBinding> _bindingsByVariable;

		/// <summary>
		/// Initializes a runtime with the specified schema, calculator, and bindings
		/// </summary>
		/// <param name="schema">The schema which defines the variables and calculations in effect for this runtime</param>
		/// <param name="calculator">The calculator which applies the specified schema's calculations to this runtime</param>
		/// <param name="bindings">The initial states of the variables in this runtime</param>
		public GraspRuntime(GraspSchema schema, ICalculator calculator, IEnumerable<VariableBinding> bindings)
		{
			Contract.Requires(schema != null);
			Contract.Requires(calculator != null);
			Contract.Requires(bindings != null);

			// TODO: Ensure that all bound variables exist in schema

			Schema = schema;
			Calculator = calculator;

			_bindingsByVariable = bindings.ToDictionary(binding => binding.Variable);
		}

		/// <summary>
		/// Initializes a runtime with the specified schema, calculator, and bindings
		/// </summary>
		/// <param name="schema">The schema which defines the variables and calculations in effect for this runtime</param>
		/// <param name="calculator">The calculator which applies the specified schema's calculations to this runtime</param>
		/// <param name="bindings">The initial states of the variables in this runtime</param>
		public GraspRuntime(GraspSchema schema, ICalculator calculator, params VariableBinding[] bindings) : this(schema, calculator, bindings as IEnumerable<VariableBinding>)
		{}

		/// <summary>
		/// Gets the schema which defines the variables and calculations in effect for this runtime
		/// </summary>
		public GraspSchema Schema { get; private set; }

		/// <summary>
		/// Gets the calculator which applies the schema's calculations to this runtime
		/// </summary>
		public ICalculator Calculator { get; private set; }

		/// <summary>
		/// Applies the calculations defined by <see cref="Schema"/> to this runtime
		/// </summary>
		public void ApplyCalculations()
		{
			Calculator.ApplyCalculation(this);
		}

		/// <summary>
		/// Gets the value of the specified variable
		/// </summary>
		/// <param name="variable">The variable for which to get the value</param>
		/// <returns>The value of the specified variable</returns>
		/// <exception cref="UnboundVariableException">Thrown if the specified variable is not bound</exception>
		public object GetVariableValue(Variable variable)
		{
			Contract.Requires(variable != null);

			var binding = TryGetBinding(variable);

			if(binding == null)
			{
				throw new UnboundVariableException(variable, Resources.VariableNotBound.FormatInvariant(variable));
			}

			return binding.Value;
		}

		/// <summary>
		/// Sets the value of the specified variable
		/// </summary>
		/// <param name="variable">The variable for which to set the specified value</param>
		/// <param name="value">The new value of the specified variable</param>
		public void SetVariableValue(Variable variable, object value)
		{
			Contract.Requires(variable != null);

			var binding = TryGetBinding(variable);

			if(binding != null)
			{
				binding.Value = value;
			}
			else
			{
				binding = new VariableBinding(variable, value);

				_bindingsByVariable[variable] = binding;
			}
		}

		private VariableBinding TryGetBinding(Variable variable)
		{
			VariableBinding binding;

			_bindingsByVariable.TryGetValue(variable, out binding);

			return binding;
		}
	}
}