using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Cloak;

namespace Grasp
{
	public class GraspRuntime
	{
		private readonly Dictionary<Variable, VariableBinding> _bindingsByVariable;

		public GraspRuntime(GraspSchema schema, ICalculator calculator, IEnumerable<VariableBinding> bindings)
		{
			Contract.Requires(schema != null);
			Contract.Requires(calculator != null);
			Contract.Requires(bindings != null);

			Schema = schema;
			Calculator = calculator;

			_bindingsByVariable = bindings.ToDictionary(binding => binding.Variable);
		}

		public GraspSchema Schema { get; private set; }

		public ICalculator Calculator { get; private set; }

		public void ApplyCalculations()
		{
			Calculator.ApplyCalculation(this);
		}

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