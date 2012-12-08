﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Grasp.Knowledge
{
	/// <summary>
	/// An expression tree node which represents a variable
	/// </summary>
	public sealed class VariableExpression : Expression
	{
		internal VariableExpression(Variable variable)
		{
			Variable = variable;
		}

		/// <summary>
		/// Gets the extension node type
		/// </summary>
		public override ExpressionType NodeType
		{
			get { return ExpressionType.Extension; }
		}

		/// <summary>
		/// Gets the type of value represented by this expression tree node
		/// </summary>
		public override Type Type
		{
			get { return Variable.Type; }
		}

		/// <summary>
		/// Gets the variable represented by this expression tree node
		/// </summary>
		public new Variable Variable { get; private set; }

		/// <summary>
		/// Gets the fully-qualified name of the variable represented by this expression tree node
		/// </summary>
		/// <returns>The fully-qualified name of the variable represented by this expression tree node</returns>
		public override string ToString()
		{
			return Variable.ToString();
		}
	}
}