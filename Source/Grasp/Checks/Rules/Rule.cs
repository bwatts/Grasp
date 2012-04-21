using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grasp.Checks.Rules
{
	/// <summary>
	/// Base class for nodes in trees which represent boolean-valued inquiries on target data
	/// </summary>
	public abstract class Rule
	{
		/// <summary>
		/// When implemented by a derived class, gets the type of this node in the tree
		/// </summary>
		public abstract RuleType Type { get; }
	}
}