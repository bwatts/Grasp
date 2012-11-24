using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grasp.Checks.Rules
{
	/// <summary>
	/// Represents a logical operation on the results of two rules
	/// </summary>
	public sealed class BinaryRule : Rule
	{
		public static readonly Field<Rule> LeftField = Grasp.Field.On<BinaryRule>.For(x => x.Left);
		public static readonly Field<Rule> RightField = Grasp.Field.On<BinaryRule>.For(x => x.Right);

		internal BinaryRule(RuleType type, Rule left, Rule right) : base(type)
		{
			Left = left;
			Right = right;
		}

		/// <summary>
		/// Gets the left operand of the binary operation
		/// </summary>
		public Rule Left { get { return GetValue(LeftField); } private set { SetValue(LeftField, value); } }

		/// <summary>
		/// Gets the right operand of the binary operation
		/// </summary>
		public Rule Right { get { return GetValue(RightField); } private set { SetValue(RightField, value); } }
	}
}