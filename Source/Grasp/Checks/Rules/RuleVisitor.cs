using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Grasp.Checks.Rules
{
	/// <summary>
	/// Base implementation of an algorithm that visits each node in a rule tree
	/// </summary>
	public abstract class RuleVisitor
	{
		/// <summary>
		/// Visits all nodes in the specified rule
		/// </summary>
		/// <param name="node">The rule to visit</param>
		/// <returns>The new form of the rule</returns>
		protected virtual Rule Visit(Rule node)
		{
			if(node == null)
			{
				return null;
			}

			switch(node.Type)
			{
				case RuleType.Check:
					return VisitCheck((CheckRule) node);
				case RuleType.Result:
					return VisitResult((ResultRule) node);
				case RuleType.Literal:
					return VisitLiteral((LiteralRule) node);
				case RuleType.Lambda:
					return VisitLambda((LambdaRule) node);
				case RuleType.And:
				case RuleType.Or:
				case RuleType.ExclusiveOr:
					return VisitBinary((BinaryRule) node);
				case RuleType.Not:
					return VisitNot((NotRule) node);
				case RuleType.Property:
				case RuleType.Field:
				case RuleType.Method:
					return VisitMember((MemberRule) node);
				default:
					return node;
			}
		}

		/// <summary>
		/// Visits the specified check rule
		/// </summary>
		/// <param name="node">The check rule to visit</param>
		/// <returns>The new form of the rule</returns>
		protected virtual Rule VisitCheck(CheckRule node)
		{
			return node;
		}

		/// <summary>
		/// Visits the specified result rule
		/// </summary>
		/// <param name="node">The result rule to visit</param>
		/// <returns>The new form of the rule</returns>
		protected virtual Rule VisitResult(ResultRule node)
		{
			return node;
		}

		/// <summary>
		/// Visits the specified literal rule
		/// </summary>
		/// <param name="node">The literal rule to visit</param>
		/// <returns>The new form of the rule</returns>
		protected virtual Rule VisitLiteral(LiteralRule node)
		{
			return node;
		}

		/// <summary>
		/// Visits the specified lambda rule
		/// </summary>
		/// <param name="node">The lambda rule to visit</param>
		/// <returns>The new form of the rule</returns>
		protected virtual Rule VisitLambda(LambdaRule node)
		{
			return node;
		}

		/// <summary>
		/// Visits the specified binary rule and its operands
		/// </summary>
		/// <param name="node">The binary rule to visit</param>
		/// <returns>The new form of the rule</returns>
		protected virtual Rule VisitBinary(BinaryRule node)
		{
			if(node == null)
			{
				return null;
			}

			var left = Visit(node.Left);
			var right = Visit(node.Right);

			return left == node.Left && right == node.Right ? node : Rule.MakeBinary(node.Type, left, right);
		}

		/// <summary>
		/// Visits the specified not rule and the negated rule
		/// </summary>
		/// <param name="node">The not rule to visit</param>
		/// <returns>The new form of the rule</returns>
		protected virtual Rule VisitNot(NotRule node)
		{
			if(node == null)
			{
				return null;
			}

			var rule = Visit(node.Rule);

			return rule == node.Rule ? node : Rule.Not(rule);
		}

		/// <summary>
		/// Visits the specified member rule and the rule applied to the member's value
		/// </summary>
		/// <param name="node">The member rule to visit</param>
		/// <returns>The new form of the rule</returns>
		protected virtual Rule VisitMember(MemberRule node)
		{
			if(node == null)
			{
				return null;
			}

			var rule = Visit(node.Rule);

			return rule == node.Rule ? node : Rule.MakeMember(node.Member, rule);
		}
	}
}