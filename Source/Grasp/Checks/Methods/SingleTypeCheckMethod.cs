using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Grasp.Checks.Methods
{
	/// <summary>
	/// Base implementation of a method that produces check rules which apply to a particular type
	/// </summary>
	[ContractClass(typeof(SingleTypeCheckMethodContract))]
	public abstract class SingleTypeCheckMethod : CheckMethod
	{
		/// <summary>
		/// Determines whether the specified target type is assignable to the supported type of this check method
		/// </summary>
		/// <param name="targetType">The type to check for support</param>
		/// <returns>Whether the specified target type is assignable to the supported type of this check method</returns>
		protected override bool SupportsTargetType(Type targetType)
		{
			return TargetType.IsAssignableFrom(targetType);
		}

		/// <summary>
		/// Gets the reflected method that applies this check method to the specified check type
		/// </summary>
		/// <param name="targetType">The type of target data to which the reflected method is applied</param>
		/// <param name="checkType">The type of check to which the reflected method is applied</param>
		/// <returns>The reflected method that applies this check method to the specified check type</returns>
		protected override MethodInfo GetMethod(Type targetType, Type checkType)
		{
			return GetMethod(checkType);
		}

		/// <summary>
		/// Gets the arguments applied to this check method
		/// </summary>
		/// <param name="targetType">The type of target data to which this check method is applied</param>
		/// <returns>The arguments to this check method</returns>
		protected sealed override IEnumerable<object> GetCheckArguments(Type targetType)
		{
			return GetCheckArguments();
		}

		/// <summary>
		/// Gets the arguments applied to this check method
		/// </summary>
		/// <returns>The arguments to this check method</returns>
		protected virtual IEnumerable<object> GetCheckArguments()
		{
			Contract.Ensures(Contract.Result<IEnumerable<object>>() != null);

			return Enumerable.Empty<object>();
		}

		/// <summary>
		/// When implemented by a derived class, gets the supported target type of this check method
		/// </summary>
		protected abstract Type TargetType { get; }

		/// <summary>
		/// When implemented in a derived class, gets the reflected method that applies this check method to the specified check type
		/// </summary>
		/// <param name="checkType">The type of check to which the reflected method is applied</param>
		/// <returns>The reflected method that applies this check method to the specified check type</returns>
		protected abstract MethodInfo GetMethod(Type checkType);
	}

	[ContractClassFor(typeof(SingleTypeCheckMethod))]
	internal abstract class SingleTypeCheckMethodContract : SingleTypeCheckMethod
	{
		protected override Type TargetType
		{
			get
			{
				Contract.Ensures(Contract.Result<Type>() != null);

				return null;
			}
		}

		protected override MethodInfo GetMethod(Type checkType)
		{
			Contract.Requires(checkType != null);
			Contract.Ensures(Contract.Result<MethodInfo>() != null);

			return null;
		}
	}
}