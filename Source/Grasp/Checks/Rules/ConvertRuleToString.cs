using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grasp.Checks.Rules
{
	internal sealed class ConvertRuleToString : RuleVisitor
	{
		private StringBuilder _builder;

		internal string ConvertToString(Rule rule)
		{
			_builder = new StringBuilder();

			Visit(rule);

			return _builder.ToString();
		}

		protected override Rule VisitCheck(CheckRule node)
		{
			_builder.Append(node.Method.Name);

			if(node.CheckArguments.Any())
			{
				_builder.Append("(");

				var wroteFirst = false;

				foreach(var checkArgument in node.CheckArguments)
				{
					if(wroteFirst)
					{
						_builder.Append(", ");
					}
					else
					{
						wroteFirst = true;
					}

					_builder.Append(checkArgument);
				}

				_builder.Append(")");
			}

			return node;
		}

		protected override Rule VisitConstant(ConstantRule node)
		{
			_builder.Append(node.Passes.ToString().ToLower());

			return node;
		}

		protected override Rule VisitLambda(LambdaRule node)
		{
			_builder.Append(node.Lambda.ToString());

			return node;
		}

		protected override Rule VisitBinary(BinaryRule node)
		{
			_builder.Append("(");

			Visit(node.Left);

			switch(node.Type)
			{
				case RuleType.And:
					_builder.Append(" && ");
					break;
				case RuleType.Or:
					_builder.Append(" || ");
					break;
				default:
					_builder.Append(" ^ ");
					break;
			}

			Visit(node.Right);

			_builder.Append(")");

			return node;
		}

		protected override Rule VisitNot(NotRule node)
		{
			_builder.Append("!(");

			Visit(node.Rule);

			_builder.Append(")");

			return node;
		}

		protected override Rule VisitMember(MemberRule node)
		{
			switch(node.Type)
			{
				case RuleType.Property:
				case RuleType.Field:
					_builder.AppendFormat(".{0} => ", node.Member.Name);
					break;
				default:
					_builder.AppendFormat(".{0}() => ", node.Member.Name);
					break;
			}

			Visit(node.Rule);

			return node;
		}
	}
}