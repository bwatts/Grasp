using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.Reflection;

namespace Grasp.Checks.Methods
{
	/// <summary>
	/// Produces check rules for the <see cref="Checkable.IsBetween{T}(ICheckable{T}, T, T)"/> and <see cref="Checkable.IsBetween{T}(ICheckable{T}, T, T, BoundaryType)"/> methods
	/// </summary>
	public sealed class IsBetweenMethod : ComparisonMethod
	{
		private readonly object _minimum;
		private readonly object _maximum;
		private readonly BoundaryType? _boundaryType;

		/// <summary>
		/// Initializes a method with the specified minimum and maximum
		/// </summary>
		/// <param name="minimum">The minimum value to compare</param>
		/// <param name="maximum">The maximum value to compare</param>
		public IsBetweenMethod(object minimum, object maximum)
		{
			_minimum = minimum;
			_maximum = maximum;
		}

		/// <summary>
		/// Initializes a method with the specified minimum, maximum, and boundary type
		/// </summary>
		/// <param name="minimum">The minimum value to compare</param>
		/// <param name="maximum">The maximum value to compare</param>
		/// <param name="boundaryType">The specification of how to handle the minimum and maximum values</param>
		public IsBetweenMethod(object minimum, object maximum, BoundaryType boundaryType) : this(minimum, maximum)
		{
			_boundaryType = boundaryType;
		}

		/// <summary>
		/// Gets the relevant overload from the <see cref="Checkable.IsBetween{T}(ICheckable{T}, T, T)"/> and <see cref="Checkable.IsBetween{T}(ICheckable{T}, T, T, BoundaryType)"/> methods
		/// </summary>
		/// <param name="targetType">The type of target data to which this check method is applied</param>
		/// <param name="checkType">The type of check</param>
		/// <returns>
		/// The relevant overload from the <see cref="Checkable.IsBetween{T}(ICheckable{T}, T, T)"/> and <see cref="Checkable.IsBetween{T}(ICheckable{T}, T, T, BoundaryType)"/> methods
		/// </returns>
		protected override MethodInfo GetMethod(Type targetType, Type checkType)
		{
			return _boundaryType == null ? GetOverload(targetType, 3) : GetOverload(targetType, 4);
		}

		/// <summary>
		/// Gets the arguments to the relevant overload
		/// </summary>
		/// <param name="targetType">The type of target data to which this check method is applied</param>
		/// <returns>The arguments to the relevant overload</returns>
		protected override IEnumerable<object> GetCheckArguments(Type targetType)
		{
			yield return _minimum;
			yield return _maximum;

			if(_boundaryType != null)
			{
				yield return _boundaryType.Value;
			}
		}

		private static MethodInfo GetOverload(Type targetType, int parameterCount)
		{
			return typeof(Checkable)
				.GetMethods(BindingFlags.Public | BindingFlags.Static)
				.Where(method => method.Name == "IsBetween" && method.GetParameters().Length == parameterCount)
				.First()
				.MakeGenericMethod(targetType);
		}
	}
}