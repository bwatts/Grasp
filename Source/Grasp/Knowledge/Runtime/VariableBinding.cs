using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Cloak;

namespace Grasp.Knowledge.Runtime
{
	/// <summary>
	/// An association of a variable and a value
	/// </summary>
	public class VariableBinding : Notion
	{
		public static readonly Field<FullName> NameField = Field.On<VariableBinding>.For(x => x.Name);
		public static readonly Field<object> ValueField = Field.On<VariableBinding>.For(x => x.Value);

		/// <summary>
		/// Initializes a binding with the specified variable and value
		/// </summary>
		/// <param name="name">The name of the bound variable</param>
		/// <param name="value">The value to which the specified variable is bound</param>
		public VariableBinding(FullName name, object value)
		{
			Contract.Requires(name != null);

			Name = name;
			Value = value;
		}

		/// <summary>
		/// Initializes a binding with the specified variable and value
		/// </summary>
		/// <param name="name">The name of the bound variable</param>
		/// <param name="value">The value to which the specified variable is bound</param>
		public VariableBinding(string name, object value) : this(new FullName(name), value)
		{}

		/// <summary>
		/// Gets the bound variable
		/// </summary>
		public FullName Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }

		/// <summary>
		/// Gets the value to which the variable is bound
		/// </summary>
		public object Value { get { return GetValue(ValueField); } set { SetValue(ValueField, value); } }

		/// <summary>
		/// Gets a textual representation of this binding
		/// </summary>
		/// <returns>A textual representation of this binding</returns>
		public override string ToString()
		{
			return Value == null
				? Resources.VariableBindingNullValue.FormatInvariant(Name)
				: Resources.VariableBindingNonNullValue.FormatInvariant(Name, Value);
		}
	}
}