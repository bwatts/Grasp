﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.Reflection;

namespace Grasp.Checks.Methods
{
	/// <summary>
	/// Produces check rules for the <see cref="Checkable.IsFalse"/> method
	/// </summary>
	public sealed class IsFalseMethod : SingleTypeCheckMethod
	{
		/// <summary>
		/// Gets <see cref="System.Boolean"/>
		/// </summary>
		protected override Type TargetType
		{
			get { return typeof(bool); }
		}

		/// <summary>
		/// Gets the <see cref="Checkable.IsFalse"/> method
		/// </summary>
		/// <param name="checkType">The type of check</param>
		/// <returns>The <see cref="Checkable.IsFalse"/> method</returns>
		protected override MethodInfo GetMethod(Type checkType)
		{
			return Reflect.Func<ICheckable<bool>, Check<bool>>(Checkable.IsFalse);
		}
	}
}