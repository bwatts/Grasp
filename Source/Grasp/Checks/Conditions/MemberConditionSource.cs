using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using Grasp.Checks.Rules;

namespace Grasp.Checks.Conditions
{
	/// <summary>
	/// A source which provides conditions for members of a type
	/// </summary>
	[ContractClass(typeof(MemberConditionSourceContract))]
	public abstract class MemberConditionSource : IConditionSource
	{
		#region IConditionSource
		/// <summary>
		/// Gets conditions for the members of <see cref="TargetType"/>
		/// </summary>
		/// <returns>The conditions for the members of <see cref="TargetType"/></returns>
		public IEnumerable<Condition> GetConditions()
		{
			return GetPropertyConditions().Concat(GetFieldConditions()).Concat(GetMethodConditions());
		}
		#endregion

		/// <summary>
		/// When implemented by a derived class, gets the target type for which conditions are declared
		/// </summary>
		public abstract Type TargetType { get; }

		/// <summary>
		/// When implemented by a derived class, gets the condition declarations which apply to the specified member
		/// </summary>
		/// <param name="member">The member for which to get condition declarations</param>
		/// <returns>The condition declarations which apply to the specified member</returns>
		protected abstract IEnumerable<IConditionDeclaration> GetDeclarations(MemberInfo member);

		private IEnumerable<Condition> GetPropertyConditions()
		{
			var properties = TargetType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

			return GetMemberConditions(properties, property => property.PropertyType);
		}

		private IEnumerable<Condition> GetFieldConditions()
		{
			var fields = TargetType.GetFields(BindingFlags.Public | BindingFlags.Instance);

			return GetMemberConditions(fields, field => field.FieldType);
		}

		private IEnumerable<Condition> GetMethodConditions()
		{
			var propertyMethods =
				from property in TargetType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
				from propertyMethod in new[] { property.GetGetMethod(), property.GetSetMethod() }
				where propertyMethod != null
				select propertyMethod;

			var methods = TargetType
				.GetMethods(BindingFlags.Public | BindingFlags.Instance)
				.Except(propertyMethods)
				.Where(method => method.ReturnType != typeof(void))
				.Where(method => !method.GetParameters().Any());

			return GetMemberConditions(methods, method => method.ReturnType);
		}

		private IEnumerable<Condition> GetMemberConditions<TMember>(IEnumerable<TMember> members, Func<TMember, Type> getMemberType) where TMember : MemberInfo
		{
			return
				from member in members
				from declaration in GetDeclarations(member)
				from condition in declaration.GetConditions(getMemberType(member))
				select new Condition(Rule.MakeMember(member, condition.Rule), TargetType, condition.Key.Name);
		}
	}

	[ContractClassFor(typeof(MemberConditionSource))]
	internal abstract class MemberConditionSourceContract : MemberConditionSource
	{
		public override Type TargetType
		{
			get
			{
				Contract.Ensures(Contract.Result<Type>() != null);

				return null;
			}
		}

		protected override IEnumerable<IConditionDeclaration> GetDeclarations(MemberInfo member)
		{
			Contract.Requires(member != null);
			Contract.Ensures(Contract.Result<IEnumerable<IConditionDeclaration>>() != null);

			return null;
		}
	}
}