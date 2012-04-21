using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Cloak;

namespace Grasp.Checks.Conditions
{
	/// <summary>
	/// Uniquely identifies a rule that applies to a particular type
	/// </summary>
	public sealed class ConditionKey : IEquatable<ConditionKey>
	{
		/// <summary>
		/// Determines if the specified condition keys are equal
		/// </summary>
		[Pure]
		public static bool operator ==(ConditionKey x, ConditionKey y)
		{
			return Object.ReferenceEquals(x, y) || (!Object.ReferenceEquals(x, null) && x.Equals(y));
		}

		/// <summary>
		/// Determines if the specified condition keys are not equal
		/// </summary>
		[Pure]
		public static bool operator !=(ConditionKey x, ConditionKey y)
		{
			return !(x == y);
		}

		/// <summary>
		/// Determines whether the specified name represents the default condition for a type
		/// </summary>
		/// <param name="name">The name to check</param>
		/// <returns>Whether the specified name represents the default condition for a type</returns>
		[Pure]
		public static bool IsDefaultName(string name)
		{
			return NameEqualityComparer.Equals(name, DefaultName);
		}

		/// <summary>
		/// Determines whether the specified name represents a condition that applies to every instance of a type
		/// </summary>
		/// <param name="name">The name to check</param>
		/// <returns>Whether the specified name represents an invariant condition for a type</returns>
		[Pure]
		public static bool IsInvariantName(string name)
		{
			return NameEqualityComparer.Equals(name, InvariantName);
		}

		/// <summary>
		/// The name of the default condition for a type
		/// </summary>
		public const string DefaultName = "";

		/// <summary>
		/// The name of a condition that applies to every instance of a type
		/// </summary>
		public const string InvariantName = "*";

		/// <summary>
		/// The comparer used to determine equality of names
		/// </summary>
		public static readonly IEqualityComparer<string> NameEqualityComparer = StringComparer.InvariantCulture;

		/// <summary>
		/// Initializes a condition key with the specified target type and name
		/// </summary>
		/// <param name="targetType">The type to which the condition applies</param>
		/// <param name="name">The name which uniquely identifies the condition among all those for the target type</param>
		public ConditionKey(Type targetType, string name)
		{
			Contract.Requires(targetType != null);
			Contract.Requires(name != null);

			TargetType = targetType;
			Name = name;
		}

		/// <summary>
		/// Initializes a condition key with the specified target type and the default name
		/// </summary>
		/// <param name="targetType">The type to which the condition applies</param>
		public ConditionKey(Type targetType) : this(targetType, DefaultName)
		{}

		#region Overrides : Object
		/// <summary>
		/// Determines if the specified object is equal to this condition key
		/// </summary>
		/// <param name="obj">The object to check for equality</param>
		/// <returns>Whether the object is a condition key that is equal to this one</returns>
		public override bool Equals(object obj)
		{
			return Equals(obj as ConditionKey);
		}

		/// <summary>
		/// Gets a hash code that represents this condition key among all possible condition keys
		/// </summary>
		/// <returns>A hash code representing this condition key</returns>
		public override int GetHashCode()
		{
			return HashCode.Combine(TargetType, Name);
		}

		/// <summary>
		/// Gets a textual representation of this condition key
		/// </summary>
		/// <returns>A textual representation of this condition key</returns>
		public override string ToString()
		{
			return Resources.ConditionKey.FormatInvariant(Name, TargetType.FullName);
		}
		#endregion

		#region IEquatable
		/// <summary>
		/// Determines if the specified object is equal to this condition key
		/// </summary>
		/// <param name="other">The object to check for equality</param>
		/// <returns>Whether the object is a condition key that is equal to this one</returns>
		public bool Equals(ConditionKey other)
		{
			return other != null && TargetType == other.TargetType && NameEquals(other.Name);
		}
		#endregion

		/// <summary>
		/// Gets the type to which the condition applies
		/// </summary>
		public Type TargetType { get; private set; }

		/// <summary>
		/// Gets the name which uniquely identifies the condition among all those for the target type
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Gets whether this condition key has a name of <see cref="DefaultName"/>
		/// </summary>
		public bool IsDefault
		{
			get { return IsDefaultName(Name); }
		}

		/// <summary>
		/// Gets whether this condition key has a name of <see cref="InvariantName"/>
		/// </summary>
		public bool IsInvariant
		{
			get { return IsInvariantName(Name); }
		}

		/// <summary>
		/// Determines whether the specified name is equal to the name of this condition key
		/// </summary>
		/// <param name="name">The name to check for equality</param>
		/// <returns>Whether the specified name is equal to the name of this condition key</returns>
		[Pure]
		public bool NameEquals(string name)
		{
			return NameEqualityComparer.Equals(Name, name);
		}

		/// <summary>
		/// Determines whether the condition applies to the specified target type. A condition applies to a target type if it can assigned to the condition's target type.
		/// </summary>
		/// <param name="targetType">The type to determine if the condition applies</param>
		/// <returns>Whether the condition applies to the specified target type</returns>
		[Pure]
		public bool AppliesTo(Type targetType)
		{
			Contract.Requires(targetType != null);

			return TargetType.IsAssignableFrom(targetType);
		}

		/// <summary>
		/// Determines whether the condition applies to the specified target object. A condition applies to a target object if it can be assigned to the condition's target type.
		/// </summary>
		/// <param name="target">The object to determine if the condition applies</param>
		/// <returns>Whether the condition applies to the specified target object</returns>
		[Pure]
		public bool AppliesTo(object target)
		{
			return AppliesTo(target == null ? typeof(object) : target.GetType());
		}
	}
}