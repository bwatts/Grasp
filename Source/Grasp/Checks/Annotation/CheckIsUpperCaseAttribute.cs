using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target <see cref="System.Char"/> is upper-case
	/// </summary>
	public sealed class CheckIsUpperCaseAttribute : CheckAttribute
	{
		/// <summary>
		/// Gets an instance of <see cref="IsUpperCaseMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsUpperCaseMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsUpperCaseMethod();
		}
	}
}