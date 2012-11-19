using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Grasp.Checks;

namespace Grasp
{
	/// <summary>
	/// The unique identifier of a Grasp entity
	/// </summary>
	[Serializable]
	[TypeConverter(typeof(EntityIdConverter))]
	public struct EntityId : IEquatable<EntityId>, IComparable<EntityId>
	{
		#region Operators

		public static bool operator ==(EntityId x, EntityId y)
		{
			return x.Equals(y);
		}

		public static bool operator !=(EntityId x, EntityId y)
		{
			return !(x == y);
		}

		public static bool operator >(EntityId x, EntityId y)
		{
			return x.CompareTo(y) > 0;
		}

		public static bool operator <(EntityId x, EntityId y)
		{
			return x.CompareTo(y) < 0;
		}

		public static bool operator >=(EntityId x, EntityId y)
		{
			return x.CompareTo(y) >= 0;
		}

		public static bool operator <=(EntityId x, EntityId y)
		{
			return x.CompareTo(y) <= 0;
		}
		#endregion

		/// <summary>
		/// Gets the <see cref="EntityId"/> value which represents an unassigned entity
		/// </summary>
		public static readonly EntityId Unassigned = new EntityId(Guid.Empty);

		/// <summary>
		/// Generates an entity identifier
		/// </summary>
		/// <returns>A unique identifier</returns>
		public static EntityId Generate()
		{
			return new EntityId(Guid.NewGuid());
		}

		/// <summary>
		/// Parses the specified value to an instance of <see cref="EntityId"/>
		/// </summary>
		/// <param name="value">The value to parse as an <see cref="EntityId"/></param>
		/// <returns>The <see cref="EntityId"/> represented by the specified value</returns>
		/// <exception cref="FormatException">Thrown if the value cannot be parsed as a <see cref="Guid"/></exception>
		public static EntityId Parse(string value)
		{
			EntityId entityId;

			if(!TryParse(value, out entityId))
			{
				throw new FormatException(Resources.InvalidEntityId.FormatInvariant(value));
			}

			return entityId;
		}

		/// <summary>
		/// Attempts to parse the specified value to an instance of <see cref="EntityId"/>
		/// </summary>
		/// <param name="value">The value to parse as an <see cref="EntityId"/></param>
		/// <param name="entityId">The result of parsing the specified value as an <see cref="EntityId"/>, and <see cref="Unassigned"/> otherwise</param>
		/// <returns>Whether the parse succeeded</returns>
		public static bool TryParse(string value, out EntityId entityId)
		{
			Guid guidValue;

			if(Check.That(value).IsNullOrEmpty())
			{
				entityId = EntityId.Unassigned;

				return true;
			}
			else
			{
				value = value.Trim();

				if(Guid.TryParse(value, out guidValue) || Guid.TryParseExact(value, "N", out guidValue))
				{
					entityId = new EntityId(guidValue);

					return true;
				}
				else
				{
					entityId = Unassigned;

					return false;
				}
			}
		}

		/// <summary>
		/// Initializes an identifier with the specified value
		/// </summary>
		/// <param name="value">The value of this identifier</param>
		public EntityId(Guid value) : this()
		{
			Value = value;
		}

		/// <summary>
		/// Initializes an identifier with the specified value
		/// </summary>
		/// <param name="value">The value of this identifier</param>
		public EntityId(string value) : this()
		{
			Value = Parse(value).Value;		// Hack or awesome?
		}

		/// <summary>
		/// Checks whether this identifier is equal to the specified identifier
		/// </summary>
		/// <param name="other">The identifier to check</param>
		/// <returns>Whether this identifier is equal to the specified identifier</returns>
		public bool Equals(EntityId other)
		{
			return Value == other.Value;
		}

		/// <summary>
		/// Checks whether the specified object is a <see cref="EntityId"/> and is equal to this identifier
		/// </summary>
		/// <param name="obj">The possible identifier to check</param>
		/// <returns>Whether this identifier is equal to the specified identifier</returns>
		public override bool Equals(object obj)
		{
			return obj is EntityId && Equals((EntityId) obj);
		}

		/// <summary>
		/// Gets the hash code of <see cref="Value"/>
		/// </summary>
		/// <returns>The hash code of <see cref="Value"/></returns>
		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}
		
		/// <summary>
		/// Compares this identifier to the specified identifier
		/// </summary>
		/// <param name="other">The identifier to compare with this identifier</param>
		/// <returns>A signed number indicating the relative values of this and the other instance</returns>
		public int CompareTo(EntityId other)
		{
			return Value.CompareTo(other.Value);
		}

		/// <summary>
		/// Gets the <see cref="Guid"/> value of this identifier
		/// </summary>
		public Guid Value { get; private set; }

		/// <summary>
		/// Gets a textual representation of this identifier
		/// </summary>
		/// <returns>A textual representation of this identifier</returns>
		public override string ToString()
		{
			return Value.ToString("N").ToUpper();
		}
	}
}