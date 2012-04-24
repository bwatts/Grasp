using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target <see cref="System.String"/> is null or whitespace
	/// </summary>
	public sealed class CheckIsNullOrWhiteSpaceAttribute : CheckAttribute
	{
		/// <summary>
		/// Gets an instance of <see cref="IsNullOrWhiteSpaceMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsNullOrWhiteSpaceMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsNullOrWhiteSpaceMethod();
		}
	}
}