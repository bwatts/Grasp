using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target <see cref="System.String"/> is a credit card number
	/// </summary>
	public sealed class CheckIsCreditCardNumberAttribute : CheckAttribute
	{
		/// <summary>
		/// Gets an instance of <see cref="IsCreditCardNumberMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsCreditCardNumberMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsCreditCardNumberMethod();
		}
	}
}