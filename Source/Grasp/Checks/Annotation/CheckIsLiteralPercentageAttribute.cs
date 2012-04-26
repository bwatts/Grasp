using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target <see cref="System.Decimal"/>, <see cref="System.Double"/>, or <see cref="System.Single"/> is a literal percentage
	/// </summary>
	public sealed class CheckIsLiteralPercentageAttribute : CheckAttribute
	{
		/// <summary>
		/// Gets an instance of <see cref="IsLiteralPercentageMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsLiteralPercentageMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsLiteralPercentageMethod();
		}
	}
}