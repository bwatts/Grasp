using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Cloak;

namespace Grasp
{
	/// <summary>
	/// The unit of data in a schema
	/// </summary>
	public class Variable
	{
		#region Static
		/// <summary>
		/// Determines if the specified text is a valid namespace consisting of at least one identifier separated by "."
		/// </summary>
		/// <param name="value">The text to check if it is a namespace</param>
		/// <returns>Whether the specified text is a namespace</returns>
		[Pure]
		public static bool IsNamespace(string value)
		{
			Contract.Requires(value != null);

			return Regex.IsMatch(value, @"^([_A-Za-z]+\w*)+(\.[_A-Za-z]+\w*)*$");
		}

		/// <summary>
		/// Determines if the specified text is a valid name consisting of a single identifier
		/// </summary>
		/// <param name="value">The text to check if it is a name</param>
		/// <returns>Whether the specified text is a name</returns>
		[Pure]
		public static bool IsName(string value)
		{
			Contract.Requires(value != null);

			return Regex.IsMatch(value, @"^[_A-Za-z]+\w*$");
		}

		/// <summary>
		/// Creates an expression tree node which represents the specified variable
		/// </summary>
		/// <param name="variable">The variable for which to create a node</param>
		/// <returns>An expression tree node which represents the specified variable</returns>
		[Pure]
		public static VariableExpression Expression(Variable variable)
		{
			Contract.Requires(variable != null);

			return new VariableExpression(variable);
		}
		#endregion

		/// <summary>
		/// Initializes a variable with the specified namespace, name, and type
		/// </summary>
		/// <param name="namespace">The namespace which contains the variable</param>
		/// <param name="name">The name of the variable</param>
		/// <param name="type">The type of value represented by the variable</param>
		public Variable(string @namespace, string name, Type type)
		{
			Contract.Requires(IsNamespace(@namespace));
			Contract.Requires(IsName(name));
			Contract.Requires(type != null);

			Namespace = @namespace;
			Name = name;
			Type = type;
		}

		/// <summary>
		/// Gets the namespace which contains this variable
		/// </summary>
		public string Namespace { get; private set; }

		/// <summary>
		/// Gets the name of this variable
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Gets the type of value represented by this variable
		/// </summary>
		public Type Type { get; private set; }

		/// <summary>
		/// Gets the fully-qualified name of this variable
		/// </summary>
		public override string ToString()
		{
			return Resources.Variable.FormatInvariant(Namespace, Name);
		}
	}
}