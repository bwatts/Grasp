using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
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
		/// Creates a <see cref="Grasp.Checks.Rules.CheckRule"/> that represents the application of a check method to the target data
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
		/// Creates a <see cref="Grasp.Checks.Rules.CheckRule"/> that represents the application of a check method to the target data
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

		#region Constant
		/// <summary>
		/// Creates a <see cref="Grasp.Checks.Rules.ConstantRule"/> that represents a constant value
		/// </summary>
		/// <returns>A <see cref="Grasp.Checks.Rules.ConstantRule"/> with the specified value</returns>
		public static ConstantRule Constant(bool passes)
		{
			return new ConstantRule(passes);
		}
		#endregion

		#region Lambda
		/// <summary>
		/// Creates a <see cref="Grasp.Checks.Rules.LambdaRule"/> that represents a constant value
		/// </summary>
		/// <param name="lambda">The lambda expression applied to the target data</param>
		/// <returns>A <see cref="Grasp.Checks.Rules.LambdaRule"/> with the specified lambda expression</returns>
		public static LambdaRule Lambda(LambdaExpression lambda)
		{
			Contract.Requires(lambda != null);

			return new LambdaRule(lambda);
		}
		#endregion

		/// <summary>
		/// When implemented by a derived class, gets the type of this node in the tree
		/// </summary>
		public abstract RuleType Type { get; }
	}
}