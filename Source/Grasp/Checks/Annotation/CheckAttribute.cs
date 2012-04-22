using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Grasp.Checks.Conditions;
using Grasp.Checks.Methods;
using Grasp.Checks.Rules;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Declares an annotated check on a member of a target type
	/// </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = true)]
	[ContractClass(typeof(CheckAttributeContract))]
	public abstract class CheckAttribute : Attribute, IConditionDeclaration
	{
		#region IConditionDeclaration
		/// <summary>
		/// Gets the annotated conditions declared for the specified target type
		/// </summary>
		/// <param name="targetType">The target type for which to get annotated conditions</param>
		/// <returns>The annotated conditions declared for the specified target type</returns>
		public IEnumerable<Condition> GetConditions(Type targetType)
		{
			Rule rule = GetCheckMethod().GetRule(targetType);

			if(Negate)
			{
				rule = Rule.Not(rule);
			}

			return GetConditions(rule, targetType);
		}
		#endregion

		/// <summary>
		/// Gets the comma-separated set of conditions in which this check takes effect
		/// </summary>
		public string Conditions { get; set; }

		/// <summary>
		/// Gets whether the result of this check is negated
		/// </summary>
		public bool Negate { get; set; }

		/// <summary>
		/// When implemented by a derived class, gets the check method represented by this attribute
		/// </summary>
		/// <returns>The check method represented by this attribute</returns>
		protected abstract ICheckMethod GetCheckMethod();

		private IEnumerable<Condition> GetConditions(Rule rule, Type targetType)
		{
			if(String.IsNullOrEmpty(Conditions))
			{
				yield return new Condition(rule, targetType);
			}
			else
			{
				foreach(var conditionName in Conditions.Split(',').Distinct())
				{
					yield return new Condition(rule, targetType, conditionName);
				}
			}
		}
	}

	[ContractClassFor(typeof(CheckAttribute))]
	internal abstract class CheckAttributeContract : CheckAttribute
	{
		protected override ICheckMethod GetCheckMethod()
		{
			Contract.Ensures(Contract.Result<ICheckMethod>() != null);

			return null;
		}
	}
}