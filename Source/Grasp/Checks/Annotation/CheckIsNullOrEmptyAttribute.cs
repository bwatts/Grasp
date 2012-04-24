using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target <see cref="System.String"/> is null or empty
	/// </summary>
	public sealed class CheckIsNullOrEmptyAttribute : CheckAttribute
	{
		/// <summary>
		/// Gets an instance of <see cref="IsNullOrEmptyMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsNullOrEmptyMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsNullOrEmptyMethod();
		}
	}
}