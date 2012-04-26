using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target <see cref="System.DateTime"/> falls on a particular day
	/// </summary>
	public sealed class CheckIsDayOfWeekAttribute : CheckAttribute
	{
		/// <summary>
		/// Initializes an attribute with the specified day of the week
		/// </summary>
		/// <param name="dayOfWeek">The day of the week to compare to the target date</param>
		public CheckIsDayOfWeekAttribute(DayOfWeek dayOfWeek)
		{
			DayOfWeek = dayOfWeek;
		}

		/// <summary>
		/// Gets the value to check for equality against the target value
		/// </summary>
		public DayOfWeek DayOfWeek { get; private set; }

		/// <summary>
		/// Gets an instance of <see cref="IsDayOfWeekMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsDayOfWeekMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsDayOfWeekMethod(DayOfWeek);
		}
	}
}