using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target value is not null
	/// </summary>
	public sealed class CheckIsNotNullAttribute : CheckAttribute
	{
		/// <summary>
		/// Gets an instance of <see cref="IsNotNullMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsNotNullMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsNotNullMethod();
		}
	}
}