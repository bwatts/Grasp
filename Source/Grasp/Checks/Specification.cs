using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Grasp.Checks.Rules;

namespace Grasp.Checks
{
	/// <summary>
	/// Describes a boolean-valued rule applied to a piece of data of the specified type
	/// </summary>
	/// <typeparam name="T">The type of data to which the boolean-valued rule is applied</typeparam>
	public class Specification<T> : Check<T>, ISpecifiable<T>
	{
		/// <summary>
		/// Initializes a specification with the specified target data, provider, and rule
		/// </summary>
		/// <param name="target">The data to which the rule is applied</param>
		/// <param name="provider">The provider associated with this specification</param>
		/// <param name="rule">The rule applied to the target data</param>
		public Specification(T target, ISpecificationProvider provider, Rule rule) : base(target)
		{
			Contract.Requires(rule != null);
			Contract.Requires(provider != null);

			Rule = rule;
			Provider = provider;
		}

		#region ISpecifiable
		/// <summary>
		/// Gets the provider associated with this specification
		/// </summary>
		public ISpecificationProvider Provider { get; private set; }

		/// <summary>
		/// Gets the rule applied to the target data
		/// </summary>
		public Rule Rule { get; private set; }

		#endregion

		/// <summary>
		/// Applies the rule to the target data
		/// </summary>
		/// <returns>The result of applying the rule to the target data</returns>
		public override bool Apply()
		{
			return Provider.Apply(Target, Rule);
		}
	}
}