using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target <see cref="System.DateTime"/> falls on a leap year
	/// </summary>
	public sealed class CheckIsLeapYearAttribute : CheckAttribute
	{
		/// <summary>
		/// Gets an instance of <see cref="IsLeapYearMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsLeapYearMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsLeapYearMethod();
		}
	}
}