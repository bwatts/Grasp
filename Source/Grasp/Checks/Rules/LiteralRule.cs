using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Grasp.Checks.Rules
{
	/// <summary>
	/// Represents a check whose target type is boolean and whose result is the target value
	/// </summary>
	public sealed class LiteralRule : Rule
	{
		internal LiteralRule() : base(RuleType.Literal)
		{}
	}
}