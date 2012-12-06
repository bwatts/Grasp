using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Grasp.Checks.Rules
{
	/// <summary>
	/// Represents the result of a rule instead of a definition
	/// </summary>
	public sealed class ResultRule : Rule
	{
		public static readonly Field<Expression> ExpressionField = Grasp.Field.On<ResultRule>.For(x => x.Expression);

		internal ResultRule(Expression expression) : base(RuleType.Result)
		{
			Expression = expression;
		}

		/// <summary>
		/// Gets an expression which evaluates to the result of this rule
		/// </summary>
		public Expression Expression { get { return GetValue(ExpressionField); } private set { SetValue(ExpressionField, value); } }
	}
}