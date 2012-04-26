using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target <see cref="System.Decimal"/>, <see cref="System.Double"/>, <see cref="System.Int32"/>, <see cref="System.Int64"/>, or <see cref="System.Single"/> is odd
	/// </summary>
	public sealed class CheckIsOddAttribute : CheckAttribute
	{
		/// <summary>
		/// Gets an instance of <see cref="IsOddMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsOddMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsOddMethod();
		}
	}
}