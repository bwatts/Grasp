using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target value is between two specified values
	/// </summary>
	public sealed class CheckIsBetweenAttribute : CheckAttribute
	{
		/// <summary>
		/// Initializes an attribute with the specified value
		/// </summary>
		/// <param name="minimum">The minimum value to compare</param>
		/// <param name="maximum">The maximum value to compare</param>
		public CheckIsBetweenAttribute(object minimum, object maximum)
		{
			Minimum = minimum;
			Maximum = maximum;
		}

		/// <summary>
		/// Gets the minimum value to compare
		/// </summary>
		public object Minimum { get; private set; }

		/// <summary>
		/// Gets the maximum value to compare
		/// </summary>
		public object Maximum { get; private set; }

		/// <summary>
		/// Gets the specification of how to handle the minimum and maximum values
		/// </summary>
		public BoundaryType? BoundaryType { get; set; }

		/// <summary>
		/// Gets an instance of <see cref="IsBetweenMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsBetweenMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return BoundaryType == null ? new IsBetweenMethod(Minimum, Maximum) : new IsBetweenMethod(Minimum, Maximum, BoundaryType.Value);
		}
	}
}