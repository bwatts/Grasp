using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target <see cref="System.Char"/> is a control character
	/// </summary>
	public sealed class CheckIsControlAttribute : CheckAttribute
	{
		/// <summary>
		/// Gets an instance of <see cref="IsControlMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsControlMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsControlMethod();
		}
	}
}