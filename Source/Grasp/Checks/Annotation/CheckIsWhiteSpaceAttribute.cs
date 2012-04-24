using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target <see cref="System.Char"/> is whitespace
	/// </summary>
	public sealed class CheckIsWhiteSpaceAttribute : CheckAttribute
	{
		/// <summary>
		/// Gets an instance of <see cref="IsWhiteSpaceMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsWhiteSpaceMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsWhiteSpaceMethod();
		}
	}
}