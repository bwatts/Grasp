using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.Reflection;

namespace Grasp.Checks.Methods
{
	/// <summary>
	/// Produces check rules for the <see cref="Checkable.IsEven(ICheckable{decimal})"/>, <see cref="Checkable.IsEven(ICheckable{double})"/>,
	/// <see cref="Checkable.IsEven(ICheckable{float})"/>, <see cref="Checkable.IsEven(ICheckable{int})"/>, and <see cref="Checkable.IsEven(ICheckable{long})"/> methods
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