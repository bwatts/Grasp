using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.Reflection;

namespace Grasp.Checks.Methods
{
	/// <summary>
	/// Produces check rules for the <see cref="Checkable.IsPercentage(ICheckable{int})"/> and <see cref="Checkable.IsPercentage(ICheckable{long})"/> methods
	/// </summary>
	public sealed class IsPercentageMethod : GraspNumberCheckMethod
	{
		/// <summary>
		/// Gets <see cref="System.Decimal"/>, <see cref="System.Double"/>, and <see cref="System.Single"/>
		/// </summary>
		protected override IEnumerable<Type> TargetTypes
		{
			get
			{
				yield return typeof(int);
				yield return typeof(long);
			}
		}

		/// <summary>
		/// Gets "IsPercentage"
		/// </summary>
		protected override string MethodName
		{
			get { return "IsPercentage"; }
		}
	}
}