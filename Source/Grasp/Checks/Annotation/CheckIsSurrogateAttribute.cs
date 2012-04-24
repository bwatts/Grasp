using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target <see cref="System.Char"/> is a surrogate
	/// </summary>
	public sealed class CheckIsSurrogateAttribute : CheckAttribute
	{
		/// <summary>
		/// Gets an instance of <see cref="IsSurrogateMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsSurrogateMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsSurrogateMethod();
		}
	}
}