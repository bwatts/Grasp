using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.Reflection;

namespace Grasp.Checks.Methods
{
	/// <summary>
	/// Produces check rules for the <see cref="Checkable.IsPositive(ICheckable{decimal})"/>, <see cref="Checkable.IsPositive(ICheckable{double})"/>,
	/// <see cref="Checkable.IsPositive(ICheckable{float})"/>, <see cref="Checkable.IsPositive(ICheckable{int})"/>, and <see cref="Checkable.IsPositive(ICheckable{long})"/> methods
	/// </summary>
	public sealed class IsPositiveMethod : GraspNumberCheckMethod
	{
		/// <summary>
		/// Gets "IsPositive"
		/// </summary>
		protected override string MethodName
		{
			get { return "IsPositive"; }
		}
	}
}