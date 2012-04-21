using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Rules;

namespace Grasp.Checks
{
	/// <summary>
	/// Describes a boolean-valued rule applied to a piece of data
	/// </summary>
	public interface ISpecifiable : ICheckable
	{
		/// <summary>
		/// Gets the provider associated with this specification
		/// </summary>
		ISpecificationProvider Provider { get; }

		/// <summary>
		/// Gets the rule applied to the target data
		/// </summary>
		Rule Rule { get; }
	}

	/// <summary>
	/// Describes a boolean-valued rule applied to a piece of data of the specified type
	/// </summary>
	/// <typeparam name="T">The type of data to which the boolean-valued rule is applied</typeparam>
	public interface ISpecifiable<out T> : ICheckable<T>, ISpecifiable
	{}
}