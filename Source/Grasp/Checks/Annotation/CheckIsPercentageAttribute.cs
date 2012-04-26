using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target <see cref="System.Int32"/> or <see cref="System.Int64"/> is a percentage in the range 0-100 (inclusive)
	/// </summary>
	public sealed class CheckIsPercentageAttribute : CheckAttribute
	{
		/// <summary>
		/// Gets an instance of <see cref="IsPercentageMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsPercentageMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsPercentageMethod();
		}
	}
}