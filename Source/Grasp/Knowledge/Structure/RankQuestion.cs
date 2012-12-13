using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Cloak.Reflection;
using Grasp.Checks;
using Grasp.Checks.Rules;

namespace Grasp.Knowledge.Structure
{
	public class RankQuestion : Question
	{
		public static readonly Field<ManyInOrder<Identifier>> ItemVariableNamesField = Field.On<RankQuestion>.For(x => x.ItemVariableNames);
		public static readonly Field<Identifier> ValidVariableNameField = Field.On<RankQuestion>.For(x => x.ValidVariableName);

		public RankQuestion(IEnumerable<Identifier> itemVariableNames, Identifier validVariableName, FullName name = null) : base(name)
		{
			Contract.Requires(itemVariableNames != null);
			Contract.Requires(validVariableName != null);

			ItemVariableNames = itemVariableNames.ToManyInOrder();
			ValidVariableName = validVariableName;
		}

		public ManyInOrder<Identifier> ItemVariableNames { get { return GetValue(ItemVariableNamesField); } private set { SetValue(ItemVariableNamesField, value); } }
		public Identifier ValidVariableName { get { return GetValue(ValidVariableNameField); } private set { SetValue(ValidVariableNameField, value); } }

		public override Schema GetSchema(Namespace rootNamespace)
		{
			var variables = GetVariables(rootNamespace).ToList();

			var validCalculation = GetValidationRule(rootNamespace, variables);

			return new Schema(variables, Params.Of(validCalculation));
		}

		private IEnumerable<Variable> GetVariables(Namespace rootNamespace)
		{
			return ItemVariableNames.Select(itemVariableName => new Variable<int>(rootNamespace + itemVariableName));
		}

		private Calculation GetValidationRule(Namespace rootNamespace, IEnumerable<Variable> variables)
		{
			return new Calculation<bool>(rootNamespace + ValidVariableName, GetValidExpression(variables));
		}

		private static Expression GetValidExpression(IEnumerable<Variable> variables)
		{
			// First, build a lambda expression which checks that a sequence of values is distinct:
			//
			// areDistinctLambda: (int[] values) => Check.That(values).AreDistinct()
			//
			// Then, apply it to the variables:
			//
			// areDistinctLambda(variables)

			// TODO: Use CheckMethod when available for Enumerable and Queryable checks

			var areDistinctMethod = Reflect.Func<ICheckable<IEnumerable<int>>, Check<IEnumerable<int>>>(Checkable.AreDistinct);

			var areDistinctLambda = Rule.Check(areDistinctMethod).ToLambdaExpression(typeof(int[]));

			var variableArray = Expression.NewArrayInit(typeof(int), variables.Select(variable => variable.ToExpression()));

			return Expression.Invoke(areDistinctLambda, variableArray);
		}
	}
}