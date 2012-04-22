using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Declares a default name for all annotation conditions on a class
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DefaultConditionNameAttribute : Attribute
	{
		/// <summary>
		/// Initializes an attribute with the specified name
		/// </summary>
		/// <param name="name">The default name of all annotation conditions on a class</param>
		public DefaultConditionNameAttribute(string name)
		{
			Contract.Requires(name != null);

			Name = name;
		}

		/// <summary>
		/// Gets the default name of all annotation conditions on a class
		/// </summary>
		public string Name { get; private set; }
	}
}