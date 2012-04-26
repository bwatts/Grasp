using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.Reflection;

namespace Grasp.Checks.Methods
{
	/// <summary>
	/// Produces check rules for the <see cref="Checkable.IsNegative(ICheckable{decimal})"/>, <see cref="Checkable.IsNegative(ICheckable{double})"/>,
	/// <see cref="Checkable.IsNegative(ICheckable{float})"/>, <see cref="Checkable.IsNegative(ICheckable{int})"/>, and <see cref="Checkable.IsNegative(ICheckable{long})"/> methods
	/// </summary>
	public sealed class IsNegativeMethod : GraspNumberCheckMethod
	{
		/// <summary>
		/// Gets "IsNegative"
		/// </summary>
		protected override string MethodName
		{
			get { return "IsNegative"; }
		}
	}
}