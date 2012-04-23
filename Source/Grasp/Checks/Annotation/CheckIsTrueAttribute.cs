using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target <see cref="System.Boolean"/> is false
	/// </summary>
	public sealed class CheckIsTrueAttribute : CheckAttribute
	{
		/// <summary>
		/// Gets an instance of <see cref="IsTrueMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsTrueMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsTrueMethod();
		}
	}
}