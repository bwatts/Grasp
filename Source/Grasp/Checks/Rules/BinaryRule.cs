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
		private readonly RuleType _type;

		internal BinaryRule(RuleType type, Rule left, Rule right)
		{
			_type = type;
			Left = left;
			Right = right;
		}

		/// <summary>
		/// Gets the type of this binary rule
		/// </summary>
		public override RuleType Type
		{
			get { return _type; }
		}

		/// <summary>
		/// Gets the left operand of the binary operation
		/// </summary>
		public Rule Left { get; private set; }

		/// <summary>
		/// Gets the right operand of the binary operation
		/// </summary>
		public Rule Right { get; private set; }
	}
}