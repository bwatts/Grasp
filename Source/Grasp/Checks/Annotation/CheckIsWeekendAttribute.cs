using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target <see cref="System.DateTime"/> falls on a weekend
	/// </summary>
	public sealed class CheckIsWeekendAttribute : CheckAttribute
	{
		/// <summary>
		/// Gets an instance of <see cref="IsWeekendMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsWeekendMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsWeekendMethod();
		}
	}
}