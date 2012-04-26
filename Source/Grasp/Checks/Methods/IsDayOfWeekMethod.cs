using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.Reflection;

namespace Grasp.Checks.Methods
{
	/// <summary>
	/// Produces check rules for the <see cref="Checkable.IsDayOfWeek"/> method
	/// </summary>
	public sealed class IsDayOfWeekMethod : SingleTypeCheckMethod
	{
		private readonly DayOfWeek _dayOfWeek;

		/// <summary>
		/// Initializes a method with the specified day of the week
		/// </summary>
		/// <param name="dayOfWeek">The day of the week to compare to the target date</param>
		public IsDayOfWeekMethod(DayOfWeek dayOfWeek)
		{
			_dayOfWeek = dayOfWeek;
		}

		/// <summary>
		/// Gets <see cref="System.DateTime"/>
		/// </summary>
		protected override Type TargetType
		{
			get { return typeof(DateTime); }
		}

		/// <summary>
		/// Gets the <see cref="Checkable.IsDayOfWeek"/> method
		/// </summary>
		/// <param name="checkType">The type of check</param>
		/// <returns>The <see cref="Checkable.IsDayOfWeek"/> method</returns>
		protected override MethodInfo GetMethod(Type checkType)
		{
			return Reflect.Func<ICheckable<DateTime>, DayOfWeek, Check<DateTime>>(Checkable.IsDayOfWeek);
		}

		/// <summary>
		/// Gets the day of the week
		/// </summary>
		/// <returns>The day of the week</returns>
		protected override IEnumerable<object> GetCheckArguments()
		{
			yield return _dayOfWeek;
		}
	}
}