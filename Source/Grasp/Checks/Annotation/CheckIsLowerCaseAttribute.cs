using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target <see cref="System.Char"/> is lower-case
	/// </summary>
	public sealed class CheckIsLowerCaseAttribute : CheckAttribute
	{
		/// <summary>
		/// Gets an instance of <see cref="IsLowerCaseMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsLowerCaseMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsLowerCaseMethod();
		}
	}
}