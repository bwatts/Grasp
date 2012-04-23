using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Grasp.Checks.Methods
{
	/// <summary>
	/// Produces check rules for the <see cref="Checkable.IsTrue"/> method
	/// </summary>
	public sealed class IsTrueMethod : SingleTypeCheckMethod
	{
		/// <summary>
		/// Gets <see cref="System.Boolean"/>
		/// </summary>
		protected override Type TargetType
		{
			get { return typeof(bool); }
		}

		/// <summary>
		/// Gets the <see cref="Checkable.IsTrue"/> method
		/// </summary>
		/// <param name="checkType">The type of check</param>
		/// <returns>The <see cref="Checkable.IsTrue"/> method</returns>
		protected override MethodInfo GetMethod(Type checkType)
		{
			return typeof(Checkable).GetMethod("IsTrue");
		}
	}
}