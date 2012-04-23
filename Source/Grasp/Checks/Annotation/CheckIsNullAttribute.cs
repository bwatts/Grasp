using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target value is null
	/// </summary>
	public sealed class CheckIsNullAttribute : CheckAttribute
	{
		/// <summary>
		/// Gets an instance of <see cref="IsNullMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsNullMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsNullMethod();
		}
	}
}