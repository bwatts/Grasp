using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Cloak;

namespace Grasp.Analysis
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
		/// Attempts to get the namespaces and name in the specified full name
		/// </summary>
		/// <param name="fullName">The name containing a namespace and name</param>
		/// <param name="namespace">Is set to the namespace (if any) in the specified full name</param>
		/// <param name="name">Is set to the name (if any) in the specified full name</param>
		/// <returns>true if the specified full name is a variable name with or without a namespace; false otherwise</returns>
		[Pure]
		public static bool TryGetNamespaceAndName(string fullName, out string @namespace, out string name)
		{
			Contract.Requires(fullName != null);

			bool hasFormat;

			var separatorIndex = fullName.LastIndexOf('.');

			if(separatorIndex == -1)
			{
				@namespace = "";
				name = fullName;

				hasFormat = IsName(name);
			}
			else
			{
				@namespace = fullName.Substring(0, separatorIndex);

				name = fullName.Substring(separatorIndex + 1);

				hasFormat = IsNamespace(@namespace) && IsName(name);
			}

			if(!hasFormat)
			{
				@namespace = null;
				name = null;
			}

			return hasFormat;
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
		/// Initializes a variable with the specified type, namespace, and name
		/// </summary>
		/// <param name="type">The type of value represented by the variable</param>
		/// <param name="namespace">The namespace which contains the variable</param>
		/// <param name="name">The name of the variable</param>
		public Variable(Type type, string @namespace, string name)
		{
			Contract.Requires(type != null);
			Contract.Requires(IsNamespace(@namespace));
			Contract.Requires(IsName(name));

			Type = type;
			Namespace = @namespace;
			Name = name;
		}

		/// <summary>
		/// Initializes a variable with the specified type and full name
		/// </summary>
		/// <param name="type">The type of value represented by the variable</param>
		/// <param name="fullName">The namespace and name of the variable</param>
		/// <exception cref="FormatException">Thrown if the full name is not a qualified variable name</exception>
		public Variable(Type type, string fullName)
		{
			Contract.Requires(type != null);

			Type = type;

			string @namespace;
			string name;

			if(!TryGetNamespaceAndName(fullName, out @namespace, out name))
			{
				throw new FormatException(Resources.NotQualifiedVariableName.FormatInvariant(fullName));
			}

			
			Namespace = @namespace;
			Name = name;
		}

		/// <summary>
		/// Gets the type of value represented by this variable
		/// </summary>
		public Type Type { get; private set; }

		/// <summary>
		/// Gets the namespace which contains this variable
		/// </summary>
		public string Namespace { get; private set; }

		/// <summary>
		/// Gets the name of this variable
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Gets the fully-qualified name of this variable
		/// </summary>
		/// <returns>The fully-qualified name of this variable</returns>
		public override string ToString()
		{
			return String.IsNullOrEmpty(Namespace) ? Name : Resources.QualifiedVariable.FormatInvariant(Namespace, Name);
		}
	}

	/// <summary>
	/// The unit of data in a schema
	/// </summary>
	/// <typeparam name="T">The type of value represented by the variable</typeparam>
	public class Variable<T> : Variable
	{
		/// <summary>
		/// Initializes a variable of type <typeparamref name="T"/> with the specified namespace and name
		/// </summary>
		/// <param name="namespace">The namespace which contains the variable</param>
		/// <param name="name">The name of the variable</param>
		public Variable(string @namespace, string name) : base(typeof(T), @namespace, name)
		{}

		/// <summary>
		/// Initializes a variable of type <typeparamref name="T"/> with the specified full name
		/// </summary>
		/// <param name="fullName">The namespace and name of the variable</param>
		/// <exception cref="FormatException">Thrown if the full name is not a qualified variable name</exception>
		public Variable(string fullName) : base(typeof(T), fullName)
		{}
	}
}