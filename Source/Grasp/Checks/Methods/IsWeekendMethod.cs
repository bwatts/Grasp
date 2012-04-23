using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Grasp.Checks.Methods
{
	/// <summary>
	/// Produces check rules for the <see cref="Checkable.IsWeekend"/> method
	/// </summary>
	public sealed class IsWeekendMethod : SingleTypeCheckMethod
	{
		/// <summary>
		/// Gets <see cref="System.DateTime"/>
		/// </summary>
		protected override Type TargetType
		{
			get { return typeof(DateTime); }
		}

		/// <summary>
		/// Gets the <see cref="Checkable.IsWeekend"/> method
		/// </summary>
		/// <param name="checkType">The type of check</param>
		/// <returns>The <see cref="Checkable.IsWeekend"/> method</returns>
		protected override MethodInfo GetMethod(Type checkType)
		{
			return typeof(Checkable).GetMethod("IsWeekend");
		}
	}
}