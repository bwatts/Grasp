using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target <see cref="System.String"/> is a credit card CVV
	/// </summary>
	public sealed class CheckIsCreditCardCvvAttribute : CheckAttribute
	{
		/// <summary>
		/// Gets an instance of <see cref="IsCreditCardCvvMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsCreditCardCvvMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsCreditCardCvvMethod();
		}
	}
}