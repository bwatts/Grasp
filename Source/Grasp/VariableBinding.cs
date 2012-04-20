using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp
{
	/// <summary>
	/// A binding between a variable and its value in a particular context
	/// </summary>
	public class VariableBinding
	{
		/// <summary>
		/// Initializes a binding with the specified variable and value
		/// </summary>
		/// <param name="variable">The bound variable</param>
		/// <param name="value">The value to which the specified variable is bound</param>
		public VariableBinding(Variable variable, object value)
		{
			Contract.Requires(variable != null);

			// TODO: Ensure that the value is assignable to the variable type

			Variable = variable;
			Value = value;
		}

		/// <summary>
		/// Gets the bound variable
		/// </summary>
		public Variable Variable { get; private set; }

		/// <summary>
		/// Gets the value to which <see cref="Variable"/> is bound
		/// </summary>
		public object Value { get; set; }
	}
}