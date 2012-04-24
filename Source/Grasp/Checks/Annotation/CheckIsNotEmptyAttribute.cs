using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target <see cref="System.String"/> is not empty
	/// </summary>
	public sealed class CheckIsNotEmptyAttribute : CheckAttribute
	{
		/// <summary>
		/// Gets an instance of <see cref="IsNotEmptyMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsNotEmptyMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsNotEmptyMethod();
		}
	}
}