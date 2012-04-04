using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Grasp
{
	public sealed class VariableExpression : Expression
	{
		public static readonly ExpressionType ExpressionType = (ExpressionType) 1000;	// Create a space separate from base expression trees

		internal VariableExpression(Variable variable)
		{
			Variable = variable;
		}

		public override ExpressionType NodeType
		{
			get { return ExpressionType; }
		}

		public override Type Type
		{
			get { return Variable.Type; }
		}

		public new Variable Variable { get; private set; }

		public override string ToString()
		{
			return Variable.ToString();
		}
	}
}