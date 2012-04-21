using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Checks
{
	/// <summary>
	/// Describes a boolean-valued function applied to a piece of data
	/// </summary>
	public interface ICheckable
	{
		/// <summary>
		/// Gets the type of target data
		/// </summary>
		Type TargetType { get; }

		/// <summary>
		/// Gets the target data
		/// </summary>
		object Target { get; }

		/// <summary>
		/// Applies the boolean-valued function to the target data
		/// </summary>
		/// <returns>The result of applying the boolean-valued function to the target data</returns>
		bool Apply();
	}

	/// <summary>
	/// Describes a boolean-valued function applied to a piece of data of the specified type
	/// </summary>
	/// <typeparam name="T">The type of data to which the boolean-valued function is applied</typeparam>
	public interface ICheckable<out T> : ICheckable
	{
		/// <summary>
		/// Gets the target data
		/// </summary>
		new T Target { get; }
	}
}