using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grasp.Checks.Rules
{
	/// <summary>
	/// Represents a constant result
	/// </summary>
	public sealed class ConstantRule : Rule
	{
		public static readonly Field<bool> IsTrueField = Grasp.Field.On<ConstantRule>.For(x => x.IsTrue);

		internal ConstantRule(bool isTrue) : base(RuleType.Constant)
		{
			IsTrue = isTrue;
		}

		/// <summary>
		/// Gets whether the result of this rule is true
		/// </summary>
		public bool IsTrue { get { return GetValue(IsTrueField); } private set { SetValue(IsTrueField, value); } }
	}
}