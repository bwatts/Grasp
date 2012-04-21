using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Grasp.Checks.Rules
{
	/// <summary>
	/// Represents the application of a lambda expression to the target data
	/// </summary>
	public sealed class LambdaRule : Rule
	{
		internal LambdaRule(LambdaExpression lambda)
		{
			Lambda = lambda;
		}

		/// <summary>
		/// Gets <see cref="RuleType.Lambda"/>
		/// </summary>
		public override RuleType Type
		{
			get { return RuleType.Lambda; }
		}

		/// <summary>
		/// Gets the lambda expression applied to the target data
		/// </summary>
		public new LambdaExpression Lambda { get; private set; }
	}
}