using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grasp.Checks.Methods
{
	/// <summary>
	/// Base implementation of a check method which performs a comparison
	/// </summary>
	public abstract class ComparisonMethod : CheckMethod
	{
		/// <summary>
		/// Determines whether the target type implements <see cref="IComparable{T}"/>
		/// </summary>
		/// <param name="targetType">The type to check for support</param>
		/// <returns>Whether the target type implements <see cref="IComparable{T}"/></returns>
		protected override bool SupportsTargetType(Type targetType)
		{
			var comparableType = typeof(IComparable<>).MakeGenericType(targetType);

			return comparableType.IsAssignableFrom(targetType);
		}
	}
}