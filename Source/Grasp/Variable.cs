using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Cloak;

namespace Grasp
{
	public class Variable
	{
		#region Static

		[Pure]
		public static bool IsNamespace(string value)
		{
			Contract.Requires(value != null);

			return Regex.IsMatch(value, @"^([_A-Za-z]+\w*)+(\.[_A-Za-z]+\w*)*$");
		}

		[Pure]
		public static bool IsName(string value)
		{
			Contract.Requires(value != null);

			return Regex.IsMatch(value, @"^[_A-Za-z]+\w*$");
		}

		public static VariableExpression Expression(Variable variable)
		{
			Contract.Requires(variable != null);

			return new VariableExpression(variable);
		}
		#endregion

		public Variable(string @namespace, string name, Type type)
		{
			Contract.Requires(IsNamespace(@namespace));
			Contract.Requires(IsName(name));
			Contract.Requires(type != null);

			Namespace = @namespace;
			Name = name;
			Type = type;
		}

		public string Namespace { get; private set; }

		public string Name { get; private set; }

		public Type Type { get; private set; }

		public override string ToString()
		{
			return Resources.Variable.FormatInvariant(Namespace, Name);
		}
	}
}