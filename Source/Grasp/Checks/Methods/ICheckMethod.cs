using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Grasp.Checks.Rules;

namespace Grasp.Checks.Methods
{
	/// <summary>
	/// Describes a method that produces check rules when applied to a target type
	/// </summary>
	[ContractClass(typeof(ICheckMethodContract))]
	public interface ICheckMethod
	{
		/// <summary>
		/// Gets the check rule that represents this method as applied to the specified type
		/// </summary>
		/// <param name="targetType">The type to which this method is applied</param>
		/// <returns>The check rule that represents this method as applied to the specified type</returns>
		CheckRule GetRule(Type targetType);
	}

	[ContractClassFor(typeof(ICheckMethod))]
	internal abstract class ICheckMethodContract : ICheckMethod
	{
		CheckRule ICheckMethod.GetRule(Type targetType)
		{
			Contract.Requires(targetType != null);
			Contract.Ensures(Contract.Result<CheckRule>() != null);

			return null;
		}
	}
}