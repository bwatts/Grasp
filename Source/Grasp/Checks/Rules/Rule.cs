using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Cloak;

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

		#region Operators
		/// <summary>
		/// Creates a <see cref="Grasp.Checks.Rules.BinaryRule"/> that represents a bitwise AND operation on the results of the specified operands
		/// </summary>
		/// <param name="left">The left operand of the AND</param>
		/// <param name="right">The right operand of the AND</param>
		/// <returns>A <see cref="Grasp.Checks.Rules.BinaryRule"/> with a type of <see cref="RuleType.And"/> and the specified operands</returns>
		public static BinaryRule And(Rule left, Rule right)
		{
			Contract.Requires(left != null);
			Contract.Requires(right != null);

			return new BinaryRule(RuleType.And, left, right);
		}

		/// <summary>
		/// Creates a <see cref="Grasp.Checks.Rules.BinaryRule"/> that represents a bitwise OR operation on the results of the specified operands
		/// </summary>
		/// <param name="left">The left operand of the OR</param>
		/// <param name="right">The right operand of the OR</param>
		/// <returns>A <see cref="Grasp.Checks.Rules.BinaryRule"/> with a type of <see cref="RuleType.Or"/> and the specified operands</returns>
		public static BinaryRule Or(Rule left, Rule right)
		{
			Contract.Requires(left != null);
			Contract.Requires(right != null);

			return new BinaryRule(RuleType.Or, left, right);
		}

		/// <summary>
		/// Creates a <see cref="Grasp.Checks.Rules.BinaryRule"/> that represents a bitwise XOR operation on the results of the specified operands
		/// </summary>
		/// <param name="left">The left operand of the XOR</param>
		/// <param name="right">The right operand of the XOR</param>
		/// <returns>A <see cref="Grasp.Checks.Rules.BinaryRule"/> with a type of <see cref="RuleType.ExclusiveOr"/> and the specified operands</returns>
		public static BinaryRule ExclusiveOr(Rule left, Rule right)
		{
			Contract.Requires(left != null);
			Contract.Requires(right != null);

			return new BinaryRule(RuleType.ExclusiveOr, left, right);
		}

		/// <summary>
		/// Creates a <see cref="Grasp.Checks.Rules.BinaryRule"/>, given the left and right operands, by calling the appropriate factory method
		/// </summary>
		/// <param name="binaryType">The type of the binary operation</param>
		/// <param name="left">The left operand of the binary operation</param>
		/// <param name="right">The right operand of the binary operation</param>
		/// <returns>A <see cref="Grasp.Checks.Rules.BinaryRule"/> with the specified type and operands</returns>
		public static BinaryRule MakeBinary(RuleType binaryType, Rule left, Rule right)
		{
			Contract.Requires(left != null);
			Contract.Requires(right != null);

			switch(binaryType)
			{
				case RuleType.And:
					return And(left, right);
				case RuleType.Or:
					return Or(left, right);
				case RuleType.ExclusiveOr:
					return ExclusiveOr(left, right);
				default:
					throw new ArgumentOutOfRangeException("binaryType", Resources.NotBinaryType.FormatInvariant(binaryType));
			}
		}
		#endregion

		/// <summary>
		/// When implemented by a derived class, gets the type of this node in the tree
		/// </summary>
		public abstract RuleType Type { get; }
	}
}