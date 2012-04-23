using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Grasp.Checks.Methods
{
	/// <summary>
	/// Produces check rules for the <see cref="Checkable.IsNotNull"/> method
	/// </summary>
	public sealed class IsNotNullMethod : CheckMethod
	{
		/// <summary>
		/// Indicates that all target types are supported
		/// </summary>
		/// <param name="targetType">The target type to check</param>
		/// <returns>True</returns>
		protected override bool SupportsTargetType(Type targetType)
		{
			return true;
		}

		/// <summary>
		/// Gets a closed version of the <see cref="Checkable.IsNotNull"/> method using the specified target type
		/// </summary>
		/// <param name="targetType">The target type to check</param>
		/// <param name="checkType">The type of check</param>
		/// <returns>A closed version of the <see cref="Checkable.IsNotNull"/> method using the specified target type</returns>
		protected override MethodInfo GetMethod(Type targetType, Type checkType)
		{
			return typeof(Checkable)
				.GetMethods(BindingFlags.Public | BindingFlags.Static)
				.Where(method => method.Name == "IsNotNull")
				.First()
				.MakeGenericMethod(targetType);
		}
	}
}