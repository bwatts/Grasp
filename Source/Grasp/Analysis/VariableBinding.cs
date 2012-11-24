using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Cloak;

namespace Grasp.Analysis
{
	/// <summary>
	/// A binding between a variable and its value in a particular context
	/// </summary>
	public class VariableBinding : Notion
	{
		public static readonly Field<Variable> VariableField = Field.On<VariableBinding>.For(x => x.Variable);
		public static readonly Field<object> ValueField = Field.On<VariableBinding>.For(x => x.Value);

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
		public Variable Variable { get { return GetValue(VariableField); } private set { SetValue(VariableField, value); } }

		/// <summary>
		/// Gets the value to which <see cref="Variable"/> is bound
		/// </summary>
		public object Value { get { return GetValue(ValueField); } set { SetValue(ValueField, value); } }

		/// <summary>
		/// Gets a textual representation of this binding
		/// </summary>
		/// <returns>A textual representation of this binding</returns>
		public override string ToString()
		{
			return Value == null
				? Resources.VariableBindingNullValue.FormatInvariant(Variable)
				: Resources.VariableBindingNonNullValue.FormatInvariant(Variable, Value);
		}
	}
}