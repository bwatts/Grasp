using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.Reflection;

namespace Grasp.Checks.Methods
{
	/// <summary>
	/// Produces check rules for the <see cref="Checkable.IsNotNegative(ICheckable{decimal})"/>, <see cref="Checkable.IsNotNegative(ICheckable{double})"/>,
	/// <see cref="Checkable.IsNotNegative(ICheckable{float})"/>, <see cref="Checkable.IsNotNegative(ICheckable{int})"/>, and <see cref="Checkable.IsNotNegative(ICheckable{long})"/> methods
	/// </summary>
	public sealed class IsNotNegativeMethod : GraspNumberCheckMethod
	{
		/// <summary>
		/// Gets "IsNotNegative"
		/// </summary>
		protected override string MethodName
		{
			get { return "IsNotNegative"; }
		}
	}
}