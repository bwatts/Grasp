using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target <see cref="System.Char"/> is a number
	/// </summary>
	public sealed class CheckIsNumberAttribute : CheckAttribute
	{
		/// <summary>
		/// Gets an instance of <see cref="IsNumberMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsNumberMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsNumberMethod();
		}
	}
}