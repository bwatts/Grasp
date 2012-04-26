using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.Reflection;

namespace Grasp.Checks.Methods
{
	/// <summary>
	/// Produces check rules for the <see cref="Checkable.IsAdjustedPercentage(ICheckable{decimal})"/>, <see cref="Checkable.IsAdjustedPercentage(ICheckable{double})"/>,
	/// and <see cref="Checkable.IsAdjustedPercentage(ICheckable{float})"/> methods
	/// </summary>
	public sealed class IsAdjustedPercentageMethod : GraspNumberCheckMethod
	{
		/// <summary>
		/// Gets <see cref="System.Decimal"/>, <see cref="System.Double"/>, and <see cref="System.Single"/>
		/// </summary>
		protected override IEnumerable<Type> TargetTypes
		{
			get
			{
				yield return typeof(decimal);
				yield return typeof(double);
				yield return typeof(float);
			}
		}

		/// <summary>
		/// Gets "IsAdjustedPercentage"
		/// </summary>
		protected override string MethodName
		{
			get { return "IsAdjustedPercentage"; }
		}
	}
}