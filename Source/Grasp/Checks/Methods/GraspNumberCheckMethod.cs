using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Grasp.Checks.Methods
{
	/// <summary>
	/// Base implementation of a method defined by Grasp which applies to numbers
	/// </summary>
	[ContractClass(typeof(GraspNumberCheckMethodContract))]
	public abstract class GraspNumberCheckMethod : MultiTypeCheckMethod
	{
		internal GraspNumberCheckMethod()
		{}

		/// <summary>
		/// Gets <see cref="System.Decimal"/>, <see cref="System.Double"/>, <see cref="System.Single"/>, <see cref="System.Int32"/>, and <see cref="System.Int64"/>
		/// </summary>
		protected override IEnumerable<Type> TargetTypes
		{
			get
			{
				yield return typeof(decimal);
				yield return typeof(double);
				yield return typeof(float);
				yield return typeof(int);
				yield return typeof(long);
			}
		}

		/// <summary>
		/// Gets the method which corresponds to <see cref="MethodName"/>
		/// </summary>
		/// <param name="targetType">The type of target data to which this check method is applied</param>
		/// <param name="checkType">The type of check</param>
		/// <returns>The method which corresponds to <see cref="MethodName"/></returns>
		protected override MethodInfo GetMethod(Type targetType, Type checkType)
		{
			return typeof(Checkable)
				.GetMethods(BindingFlags.Public | BindingFlags.Static)
				.Where(method => method.Name == MethodName)
				.Where(method => method.GetParameters().First().ParameterType == checkType)
				.First();
		}

		/// <summary>
		/// When implemented by a derived class, gets the name of the applicable method
		/// </summary>
		protected abstract string MethodName { get; }
	}

	[ContractClassFor(typeof(GraspNumberCheckMethod))]
	internal abstract class GraspNumberCheckMethodContract : GraspNumberCheckMethod
	{
		protected override string MethodName
		{
			get
			{
				Contract.Ensures(!String.IsNullOrEmpty(Contract.Result<string>()));

				return null;
			}
		}
	}
}