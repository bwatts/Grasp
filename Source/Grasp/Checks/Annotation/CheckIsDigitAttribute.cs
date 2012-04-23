using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target <see cref="System.Char"/> is a digit
	/// </summary>
	public sealed class CheckIsDigitAttribute : CheckAttribute
	{
		/// <summary>
		/// Gets an instance of <see cref="IsDigitMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsDigitMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsDigitMethod();
		}
	}
}