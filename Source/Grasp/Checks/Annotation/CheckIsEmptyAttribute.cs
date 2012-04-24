using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target <see cref="System.String"/> is empty
	/// </summary>
	public sealed class CheckIsEmptyAttribute : CheckAttribute
	{
		/// <summary>
		/// Gets an instance of <see cref="IsEmptyMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsEmptyMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsEmptyMethod();
		}
	}
}