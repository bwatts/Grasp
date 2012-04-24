using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target <see cref="System.Char"/> is a letter or digit
	/// </summary>
	public sealed class CheckIsLetterOrDigitAttribute : CheckAttribute
	{
		/// <summary>
		/// Gets an instance of <see cref="IsLetterOrDigitMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsLetterOrDigitMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsLetterOrDigitMethod();
		}
	}
}