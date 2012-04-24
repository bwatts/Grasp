using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target <see cref="System.Char"/> is a separator
	/// </summary>
	public sealed class CheckIsSeparatorAttribute : CheckAttribute
	{
		/// <summary>
		/// Gets an instance of <see cref="IsSeparatorMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsSeparatorMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsSeparatorMethod();
		}
	}
}