using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target <see cref="System.String"/> is a social security number
	/// </summary>
	public sealed class CheckIsSocialSecurityNumberAttribute : CheckAttribute
	{
		/// <summary>
		/// Gets an instance of <see cref="IsSocialSecurityNumberMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsSocialSecurityNumberMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsSocialSecurityNumberMethod();
		}
	}
}