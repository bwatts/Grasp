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
		public static readonly Field<LambdaExpression> LambdaField = Grasp.Field.On<LambdaRule>.For(x => x.Lambda);

		internal LambdaRule(LambdaExpression lambda) : base(RuleType.Lambda)
		{
			Lambda = lambda;
		}

		/// <summary>
		/// Gets the lambda expression applied to the target data
		/// </summary>
		public new LambdaExpression Lambda { get { return GetValue(LambdaField); } private set { SetValue(LambdaField, value); } }
	}
}