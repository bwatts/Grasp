using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target <see cref="System.String"/> is a phone number
	/// </summary>
	public sealed class CheckIsPhoneNumberAttribute : CheckAttribute
	{
		/// <summary>
		/// Gets an instance of <see cref="IsPhoneNumberMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsPhoneNumberMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsPhoneNumberMethod();
		}
	}
}