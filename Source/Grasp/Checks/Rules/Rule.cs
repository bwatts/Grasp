using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Grasp.Checks.Rules
{
	/// <summary>
	/// Base class for nodes in trees which represent boolean-valued inquiries on target data
	/// </summary>
	public abstract class Rule
	{
		#region Check
		/// <summary>
		/// Creates a <see cref="Grasp.Checks.Rules.CheckRule"/> that represents a call to a check method
		/// </summary>
		/// <param name="method">The method which applies the check</param>
		/// <param name="checkArguments">The arguments to the check method (without the base check)</param>
		/// <returns>A <see cref="Grasp.Checks.Rules.CheckRule"/> with the specified method and check arguments</returns>
		public static CheckRule Check(MethodInfo method, IEnumerable<object> checkArguments)
		{
			Contract.Requires(method != null);
			Contract.Requires(checkArguments != null);

			return new CheckRule(method, checkArguments.ToList().AsReadOnly());
		}

		/// <summary>
		/// Creates a <see cref="Grasp.Checks.Rules.CheckRule"/> that represents a call to a check method
		/// </summary>
		/// <param name="method">The method which applies the check</param>
		/// <param name="checkArguments">The arguments to the check method (without the base check)</param>
		/// <returns>A <see cref="Grasp.Checks.Rules.CheckRule"/> with the specified method and check arguments</returns>
		public static CheckRule Check(MethodInfo method, params object[] checkArguments)
		{
			Contract.Requires(method != null);
			Contract.Requires(checkArguments != null);

			return Check(method, checkArguments as IEnumerable<object>);
		}
		#endregion

		/// <summary>
		/// When implemented by a derived class, gets the type of this node in the tree
		/// </summary>
		public abstract RuleType Type { get; }
	}
}