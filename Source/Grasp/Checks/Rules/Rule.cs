using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Cloak;
using Cloak.Reflection;

namespace Grasp.Checks.Rules
{
	/// <summary>
	/// Base class for nodes in trees which represent boolean-valued inquiries on target data
	/// </summary>
	public abstract class Rule : Notion
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

			return new CheckRule(method, checkArguments);
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
		/// Creates a <see cref="Grasp.Checks.Rules.ConstantRule"/> that represents a constant result
		/// </summary>
		/// <param name="isTrue">Whether the result of the check is true</param>
		/// <returns>A <see cref="Grasp.Checks.Rules.ConstantRule"/> with the specified value</returns>
		public static ConstantRule Constant(bool isTrue)
		{
			return new ConstantRule(isTrue);
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

		/// <summary>
		/// Creates a <see cref="Grasp.Checks.Rules.NotRule"/> that represents a logical NOT of the result of the specified operand
		/// </summary>
		/// <param name="rule">The operand of the NOT</param>
		/// <returns>A <see cref="Grasp.Checks.Rules.NotRule"/> with the specified operand</returns>
		public static NotRule Not(Rule rule)
		{
			Contract.Requires(rule != null);

			return new NotRule(rule);
		}
		#endregion

		#region Members
		/// <summary>
		/// Creates a <see cref="Grasp.Checks.Rules.MemberRule"/> that represents the application of a rule to the specified property of the target data
		/// </summary>
		/// <param name="property">The property to which the rule is applied</param>
		/// <param name="rule">The rule to apply to the value of the field</param>
		/// <returns>A <see cref="Grasp.Checks.Rules.MemberRule"/> with a type of <see cref="RuleType.Property"/> and the specified property and rule</returns>
		public static MemberRule Property(PropertyInfo property, Rule rule)
		{
			Contract.Requires(property != null);
			Contract.Requires(rule != null);

			return new MemberRule(RuleType.Property, property, property.PropertyType, rule);
		}

		/// <summary>
		/// Creates a <see cref="Grasp.Checks.Rules.MemberRule"/> that represents the application of a rule to the specified field of the target data
		/// </summary>
		/// <param name="field">The field to which the rule is applied</param>
		/// <param name="rule">The rule to apply to the value of the field</param>
		/// <returns>A <see cref="Grasp.Checks.Rules.MemberRule"/> with a type of <see cref="RuleType.Field"/> and the specified field and rule</returns>
		public static MemberRule Field(FieldInfo field, Rule rule)
		{
			Contract.Requires(field != null);
			Contract.Requires(rule != null);

			return new MemberRule(RuleType.Field, field, field.FieldType, rule);
		}

		/// <summary>
		/// Creates a <see cref="Grasp.Checks.Rules.MemberRule"/> that represents the application of a rule to the return value of the specified method of the target data
		/// </summary>
		/// <param name="method">The method to which the rule is applied</param>
		/// <param name="rule">The rule to apply to the return value of the method</param>
		/// <returns>A <see cref="Grasp.Checks.Rules.MemberRule"/> with a type of <see cref="RuleType.Method"/> and the specified method and rule</returns>
		public static MemberRule Method(MethodInfo method, Rule rule)
		{
			Contract.Requires(method != null);
			Contract.Requires(rule != null);

			return new MemberRule(RuleType.Method, method, method.ReturnType, rule);
		}

		/// <summary>
		/// Creates a <see cref="Grasp.Checks.Rules.MemberRule"/>, given the specified member, by calling the appropriate factory method
		/// </summary>
		/// <param name="member">The member to which the rule is applied</param>
		/// <param name="rule">The rule to apply to the member</param>
		/// <returns>A <see cref="Grasp.Checks.Rules.MemberRule"/> with the specified member and rule</returns>
		public static MemberRule MakeMember(MemberInfo member, Rule rule)
		{
			Contract.Requires(member != null);
			Contract.Requires(rule != null);

			var property = member as PropertyInfo;

			if(property != null)
			{
				return Property(property, rule);
			}

			var field = member as FieldInfo;

			if(field != null)
			{
				return Field(field, rule);
			}

			var method = member as MethodInfo;

			if(method != null)
			{
				return Method(method, rule);
			}

			throw new ArgumentException(Resources.MemberTypeNotSupported.FormatInvariant(member.MemberType), "member");
		}
		#endregion

		public static readonly Field<RuleType> TypeField = Grasp.Field.On<Rule>.For(x => x.Type);

		/// <summary>
		/// Gets a rule with a constant result of true
		/// </summary>
		public static readonly ConstantRule True = Constant(true);

		/// <summary>
		/// Gets a rule with a constant result of false
		/// </summary>
		public static readonly ConstantRule False = Constant(false);

		/// <summary>
		/// Intiailizes a rule with the specified type
		/// </summary>
		/// <param name="type">The type of this node in the tree</param>
		protected Rule(RuleType type)
		{
			Type = type;
		}

		/// <summary>
		/// Gets the type of this node in the tree
		/// </summary>
		public RuleType Type { get { return GetValue(TypeField); } private set { SetValue(TypeField, value); } }

		/// <summary>
		/// Gets a textual representation of this rule
		/// </summary>
		/// <returns>A textual representation of this rule</returns>
		public override string ToString()
		{
			return new ConvertRuleToString().ConvertToString(this);
		}

		/// <summary>
		/// Creates a lambda expression that represents the application of this rule to target data of the specified type
		/// </summary>
		/// <param name="targetType">The type to which to apply the rule</param>
		/// <returns>A lambda expression with a parameter of the specified target type and a body which applies this rule to the parameter</returns>
		public LambdaExpression ToLambdaExpression(Type targetType)
		{
			Contract.Requires(targetType != null);

			return new ConvertRuleToLambdaExpression().ConvertToLambdaExpression(this, targetType);
		}

		/// <summary>
		/// Creates a lambda expression that represents the application of this rule to target data of the specified type
		/// </summary>
		/// <typeparam name="TTarget">The type to which to apply the rule</typeparam>
		/// <returns>A lambda expression with a parameter of the specified target type and a body which applies this rule to the parameter</returns>
		public Expression<Func<TTarget, bool>> ToLambdaExpression<TTarget>()
		{
			var lambda = ToLambdaExpression(typeof(TTarget));

			return Expression.Lambda<Func<TTarget, bool>>(lambda.Body, lambda.Parameters);
		}

		/// <summary>
		/// Compiles a function that applies this rule to target data of the specified type
		/// </summary>
		/// <param name="targetType">The type to which to apply the rule</param>
		/// <returns>A function which accepts a parameter of the specified type and which applies this rule to the parameter</returns>
		public Func<object, bool> ToFunction(Type targetType)
		{
			Contract.Requires(targetType != null);

			return ToObjectLambda(targetType).Compile();
		}

		/// <summary>
		/// Compiles a function that applies this rule to target data of the specified type
		/// </summary>
		/// <typeparam name="TTarget">The type to which to apply the rule</typeparam>
		/// <returns>A function which accepts a parameter of the specified type and which applies this rule to the parameter</returns>
		public Func<TTarget, bool> ToFunction<TTarget>()
		{
			return ToLambdaExpression<TTarget>().Compile();
		}

		/// <summary>
		/// Creates a rule with the semantics of both this and the specified rule, eliminating redundancy where possible
		/// </summary>
		/// <param name="otherRule">The rule to merge with this rule</param>
		/// <returns>A rule which has the semantics of both this and the specified rule</returns>
		public Rule MergeWith(Rule otherRule)
		{
			Contract.Requires(otherRule != null);

			// TODO: Naive - revisit

			return Rule.And(this, otherRule);
		}

		private Expression<Func<object, bool>> ToObjectLambda(Type targetType)
		{
			var lambda = ToLambdaExpression(targetType);

			// Building: untypedTarget => lambda(CastTarget<TTarget>(untypedTarget))

			var untypedTargetParameter = Expression.Parameter(typeof(object), "untypedTarget");

			var castTargetCall = Expression.Call(
				typeof(Rule),
				"CastTarget",
				new[] { targetType },
				untypedTargetParameter);

			var invokeLambda = Expression.Invoke(lambda, castTargetCall);

			return Expression.Lambda<Func<object, bool>>(invokeLambda, untypedTargetParameter);
		}

		private static T CastTarget<T>(object target)
		{
			if(!typeof(T).IsAssignableNull() && target == null)
			{
				throw new InvalidCastException(Resources.TargetTypeCannotBeAssignedNull.FormatInvariant(typeof(T)));
			}

			try
			{
				return (T) target;
			}
			catch(InvalidCastException ex)
			{
				// The target will never be null here. Null casts to all reference types, and we guarded against null with value types above.

				throw new InvalidCastException(Resources.InvalidTargetCast.FormatInvariant(target.GetType(), typeof(T)), ex);
			}
		}
	}
}