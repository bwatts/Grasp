using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak;
using Grasp.Checks.Rules;

namespace Grasp.Checks.Methods
{
	/// <summary>
	/// Base implementation of a method that produces check rules when applied to a target type
	/// </summary>
	[ContractClass(typeof(CheckMethodContract))]
	public abstract class CheckMethod : Notion, ICheckMethod
	{
		/// <summary>
		/// Gets the check rule that represents this method as applied to the specified type
		/// </summary>
		/// <param name="targetType">The type to which this method is applied</param>
		/// <returns>The check rule that represents this method as applied to the specified type</returns>
		public CheckRule GetRule(Type targetType)
		{
			if(!SupportsTargetType(targetType))
			{
				throw new GraspException(Resources.UnsupportedTargetType.FormatInvariant(targetType, GetType()));
			}

			targetType = GetEffectiveTargetType(targetType);

			var checkType = GetCheckType(targetType);

			return Rule.Check(GetMethod(targetType, checkType), GetCheckArguments(targetType));
		}

		/// <summary>
		/// When implemented by a derived class, determines whether the specified target type is supported by this check method
		/// </summary>
		/// <param name="targetType">The type to check for support</param>
		/// <returns>Whether the specified target type is supported by this check method</returns>
		protected abstract bool SupportsTargetType(Type targetType);

		/// <summary>
		/// Gets the target type which takes effect for the specified target type
		/// </summary>
		/// <param name="targetType">The type from which the effective target type is determined</param>
		/// <returns>The target type which takes effect for the specified target type</returns>
		protected virtual Type GetEffectiveTargetType(Type targetType)
		{
			Contract.Requires(targetType != null);
			Contract.Ensures(Contract.Result<Type>() != null);

			return targetType;
		}

		/// <summary>
		/// When implemented in a derived class, gets the reflected method that applies this check method to the specified target and check types
		/// </summary>
		/// <param name="targetType">The type of target data to which the reflected method is applied</param>
		/// <param name="checkType">The type of check to which the reflected method is applied</param>
		/// <returns>The reflected method that applies this check method to the specified target and check types</returns>
		protected abstract MethodInfo GetMethod(Type targetType, Type checkType);

		/// <summary>
		/// Gets the arguments applied to this check method
		/// </summary>
		/// <param name="targetType">The type of target data to which this check method is applied</param>
		/// <returns>The arguments to this check method</returns>
		protected virtual IEnumerable<object> GetCheckArguments(Type targetType)
		{
			Contract.Requires(targetType != null);
			Contract.Ensures(Contract.Result<IEnumerable<object>>() != null);

			return Enumerable.Empty<object>();
		}

		/// <summary>
		/// Gets a closed version of <see cref="ICheckable{T}"/> with the specified target type as the type argument
		/// </summary>
		/// <param name="targetType">The target type of the check</param>
		/// <returns>A closed version of <see cref="ICheckable{T}"/> with the specified target type as the type argument</returns>
		[Pure]
		protected static Type GetCheckType(Type targetType)
		{
			Contract.Requires(targetType != null);

			return typeof(ICheckable<>).MakeGenericType(targetType);
		}
	}

	[ContractClassFor(typeof(CheckMethod))]
	internal abstract class CheckMethodContract : CheckMethod
	{
		protected override bool SupportsTargetType(Type targetType)
		{
			Contract.Requires(targetType != null);

			return false;
		}

		protected override MethodInfo GetMethod(Type targetType, Type checkType)
		{
			Contract.Requires(targetType != null);
			Contract.Requires(checkType != null);
			Contract.Ensures(Contract.Result<MethodInfo>() != null);

			return null;
		}
	}
}