using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target <see cref="System.Char"/> is a low surrogate
	/// </summary>
	public sealed class CheckIsLowSurrogateAttribute : CheckAttribute
	{
		/// <summary>
		/// Gets an instance of <see cref="IsLowSurrogateMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsLowSurrogateMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsLowSurrogateMethod();
		}
	}
}