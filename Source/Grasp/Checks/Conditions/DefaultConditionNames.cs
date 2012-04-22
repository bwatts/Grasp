using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Checks.Conditions
{
	/// <summary>
	/// Provides methods for overriding the names of default conditions
	/// </summary>
	public static class DefaultConditionNames
	{
		/// <summary>
		/// Overrides the names of default conditions with the specified name
		/// </summary>
		/// <typeparam name="TDeclaration">The type of declarations in the sequence</typeparam>
		/// <param name="declarations">The declarations whose default condition names will be overridden</param>
		/// <param name="defaultConditionName">The effective name of default conditions</param>
		/// <returns>A sequence of declarations with the default condition names overridden with the specified name</returns>
		public static IEnumerable<IConditionDeclaration> WithDefaultName<TDeclaration>(this IEnumerable<TDeclaration> declarations, string defaultConditionName)
			where TDeclaration : IConditionDeclaration
		{
			Contract.Requires(declarations != null);
			Contract.Requires(defaultConditionName != null);

			return declarations.Select(declaration => new DeclarationWithDefaultName(declaration, defaultConditionName));
		}

		private sealed class DeclarationWithDefaultName : IConditionDeclaration
		{
			private readonly IConditionDeclaration _declaration;
			private readonly string _defaultName;

			internal DeclarationWithDefaultName(IConditionDeclaration declaration, string defaultName)
			{
				_declaration = declaration;
				_defaultName = defaultName;
			}

			public IEnumerable<Condition> GetConditions(Type targetType)
			{
				return _declaration.GetConditions(targetType).Select(GetEffectiveCondition);
			}

			private Condition GetEffectiveCondition(Condition condition)
			{
				return !condition.Key.IsDefault ? condition : new Condition(condition.Rule, condition.Key.TargetType, _defaultName);
			}
		}
	}
}