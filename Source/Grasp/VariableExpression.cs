using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Grasp
{
	/// <summary>
	/// An expression tree node which represents a variable
	/// </summary>
	public sealed class VariableExpression : Expression
	{
		/// <summary>
		/// The value of <see cref="System.Linq.Expressions.ExpressionType"/> associated with variable expressions
		/// </summary>
		public static readonly ExpressionType ExpressionType = (ExpressionType) 1000;	// Create a space separate from base expression trees

		internal VariableExpression(Variable variable)
		{
			Variable = variable;
		}

		/// <summary>
		/// Gets the type of this expression tree node
		/// </summary>
		public override ExpressionType NodeType
		{
			get { return ExpressionType; }
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