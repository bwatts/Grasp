using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.Reflection;

namespace Grasp.Checks.Methods
{
	/// <summary>
	/// Produces check rules for the <see cref="Checkable.IsWhiteSpace"/> method
	/// </summary>
	public sealed class IsWhiteSpaceMethod : SingleTypeCheckMethod
	{
		/// <summary>
		/// Gets <see cref="System.Char"/>
		/// </summary>
		protected override Type TargetType
		{
			get { return typeof(char); }
		}

		/// <summary>
		/// Gets the <see cref="Checkable.IsWhiteSpace"/> method
		/// </summary>
		/// <param name="checkType">The type of check</param>
		/// <returns>The <see cref="Checkable.IsWhiteSpace"/> method</returns>
		protected override MethodInfo GetMethod(Type checkType)
		{
			return Reflect.Func<ICheckable<char>, Check<char>>(Checkable.IsWhiteSpace);
		}
	}
}