using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target <see cref="System.Decimal"/>, <see cref="System.Double"/>, or <see cref="System.Single"/> is a percentage in the range 0-100 (inclusive)
	/// </summary>
	public sealed class CheckIsAdjustedPercentageAttribute : CheckAttribute
	{
		/// <summary>
		/// Gets an instance of <see cref="IsAdjustedPercentageMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsAdjustedPercentageMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsAdjustedPercentageMethod();
		}
	}
}