using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target <see cref="System.String"/> is a ZIP code
	/// </summary>
	public sealed class CheckIsZipCodeAttribute : CheckAttribute
	{
		/// <summary>
		/// Gets an instance of <see cref="IsZipCodeMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsZipCodeMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsZipCodeMethod();
		}
	}
}