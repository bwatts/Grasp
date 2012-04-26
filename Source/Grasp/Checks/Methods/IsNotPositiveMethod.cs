using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.Reflection;

namespace Grasp.Checks.Methods
{
	/// <summary>
	/// Produces check rules for the <see cref="Checkable.IsNotPositive(ICheckable{decimal})"/>, <see cref="Checkable.IsNotPositive(ICheckable{double})"/>,
	/// <see cref="Checkable.IsNotPositive(ICheckable{float})"/>, <see cref="Checkable.IsNotPositive(ICheckable{int})"/>, and <see cref="Checkable.IsNotPositive(ICheckable{long})"/> methods
	/// </summary>
	public sealed class IsNotPositiveMethod : GraspNumberCheckMethod
	{
		/// <summary>
		/// Gets "IsNotPositive"
		/// </summary>
		protected override string MethodName
		{
			get { return "IsNotPositive"; }
		}
	}
}