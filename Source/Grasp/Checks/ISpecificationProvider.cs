using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Grasp.Checks.Rules;

namespace Grasp.Checks
{
	/// <summary>
	/// Describes a provider which can create and executes rule trees
	/// </summary>
	[ContractClass(typeof(ISpecificationProviderContract))]
	public interface ISpecificationProvider
	{
		/// <summary>
		/// Constructs a specification that can apply the specified rule tree to the specified target data
		/// </summary>
		/// <typeparam name="T">The type of target data</typeparam>
		/// <param name="target">The target data</param>
		/// <param name="rule">The rule to apply to the specified target data</param>
		/// <returns>A specification that can apply the specified rule tree to the specified target data</returns>
		Specification<T> CreateSpecification<T>(T target, Rule rule);

		/// <summary>
		/// Applies the specified rule tree to the specified target data
		/// </summary>
		/// <typeparam name="T">The type of target data</typeparam>
		/// <param name="target">The target data</param>
		/// <param name="rule">The rule to apply to the specified target data</param>
		/// <returns>The result of applying the specified rule tree to the specified target data</returns>
		bool Apply<T>(T target, Rule rule);
	}

	[ContractClassFor(typeof(ISpecificationProvider))]
	internal abstract class ISpecificationProviderContract : ISpecificationProvider
	{
		Specification<T> ISpecificationProvider.CreateSpecification<T>(T target, Rule rule)
		{
			Contract.Requires(rule != null);
			Contract.Ensures(Contract.Result<Specification<T>>() != null);

			return null;
		}

		bool ISpecificationProvider.Apply<T>(T target, Rule rule)
		{
			Contract.Requires(rule != null);

			return false;
		}
	}
}