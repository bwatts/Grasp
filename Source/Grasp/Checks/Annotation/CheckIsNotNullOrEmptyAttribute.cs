using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target <see cref="System.String"/> is not null or empty
	/// </summary>
	public sealed class CheckIsNotNullOrEmptyAttribute : CheckAttribute
	{
		/// <summary>
		/// Gets an instance of <see cref="IsNotNullOrEmptyMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsNotNullOrEmptyMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsNotNullOrEmptyMethod();
		}
	}
}