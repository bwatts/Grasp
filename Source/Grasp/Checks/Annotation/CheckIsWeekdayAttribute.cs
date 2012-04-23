using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target <see cref="System.DateTime"/> falls on a weekday
	/// </summary>
	public sealed class CheckIsWeekdayAttribute : CheckAttribute
	{
		/// <summary>
		/// Gets an instance of <see cref="IsWeekdayMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsWeekdayMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsWeekdayMethod();
		}
	}
}