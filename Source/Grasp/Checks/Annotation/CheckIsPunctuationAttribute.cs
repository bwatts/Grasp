using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target <see cref="System.Char"/> is punctuation
	/// </summary>
	public sealed class CheckIsPunctuationAttribute : CheckAttribute
	{
		/// <summary>
		/// Gets an instance of <see cref="IsPunctuationMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsPunctuationMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsPunctuationMethod();
		}
	}
}