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
		private readonly string _defaultConditionName;

		/// <summary>
		/// Initializes a source with the specified target type
		/// </summary>
		/// <param name="targetType">The type with annotated conditions</param>
		public AnnotatedConditionSource(Type targetType)
		{
			Contract.Requires(targetType != null);

			_targetType = targetType;

			var defaultConditionNameAttribute = targetType.GetAttribute<DefaultConditionNameAttribute>();

			_defaultConditionName = defaultConditionNameAttribute == null ? null : defaultConditionNameAttribute.Name;
		}

		/// <summary>
		/// Gets the type with annotated conditions
		/// </summary>
		public override Type TargetType
		{
			get { return _targetType; }
		}

		/// <summary>
		/// Gets the conditions declared by attribtes on the specified member
		/// </summary>
		/// <param name="member">The member to check for annotated conditions</param>
		/// <returns>The conditions declared by attributes on the specified member</returns>
		protected override IEnumerable<IConditionDeclaration> GetDeclarations(MemberInfo member)
		{
			var declarations = member.GetAttributes<CheckAttribute>().Cast<IConditionDeclaration>();

			if(_defaultConditionName != null)
			{
				declarations = declarations.WithDefaultName(_defaultConditionName);
			}

			return declarations;
		}
	}
}