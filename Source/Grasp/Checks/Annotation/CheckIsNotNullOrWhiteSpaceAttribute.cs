using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target <see cref="System.String"/> is not null or whitespace
	/// </summary>
	public sealed class CheckIsNotNullOrWhiteSpaceAttribute : CheckAttribute
	{
		/// <summary>
		/// Gets an instance of <see cref="IsNotNullOrWhiteSpaceMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsNotNullOrWhiteSpaceMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsNotNullOrWhiteSpaceMethod();
		}
	}
}