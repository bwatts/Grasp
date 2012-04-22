using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.Reflection;
using Grasp.Checks.Conditions;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// A source which provides conditions declared by attributes on members
	/// </summary>
	public class AnnotatedConditionSource : MemberConditionSource
	{
		private readonly Type _targetType;

		/// <summary>
		/// Initializes a source with the specified target type
		/// </summary>
		/// <param name="targetType">The type with annotated conditions</param>
		public AnnotatedConditionSource(Type targetType)
		{
			Contract.Requires(targetType != null);

			_targetType = targetType;

			var defaultConditionNameAttribute = targetType.GetAttribute<DefaultConditionNameAttribute>();

			DefaultConditionName = defaultConditionNameAttribute != null ? defaultConditionNameAttribute.Name : ConditionKey.DefaultName;
		}

		/// <summary>
		/// Gets the type with annotated conditions
		/// </summary>
		public override Type TargetType
		{
			get { return _targetType; }
		}

		/// <summary>
		/// Gets the default name of conditions produced by this source
		/// </summary>
		public string DefaultConditionName { get; private set; }

		/// <summary>
		/// Gets the conditions declared by attribtes on the specified member
		/// </summary>
		/// <param name="member">The member to check for annotated conditions</param>
		/// <returns>The conditions declared by attributes on the specified member</returns>
		protected override IEnumerable<IConditionDeclaration> GetDeclarations(MemberInfo member)
		{
			var declarations = member.GetAttributes<CheckAttribute>().Cast<IConditionDeclaration>();

			if(DefaultConditionName != ConditionKey.DefaultName)
			{
				declarations = declarations.WithDefaultName(DefaultConditionName);
			}

			return declarations;
		}
	}
}