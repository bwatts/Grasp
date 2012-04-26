using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.Reflection;

namespace Grasp.Checks.Methods
{
	/// <summary>
	/// Produces check rules for the <see cref="Checkable.IsOdd(ICheckable{decimal})"/>, <see cref="Checkable.IsOdd(ICheckable{double})"/>,
	/// <see cref="Checkable.IsOdd(ICheckable{float})"/>, <see cref="Checkable.IsOdd(ICheckable{int})"/>, and <see cref="Checkable.IsOdd(ICheckable{long})"/> methods
	/// </summary>
	public sealed class IsOddMethod : GraspNumberCheckMethod
	{
		/// <summary>
		/// Gets "IsOdd"
		/// </summary>
		protected override string MethodName
		{
			get { return "IsOdd"; }
		}
	}
}