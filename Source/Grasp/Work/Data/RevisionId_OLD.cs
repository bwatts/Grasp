using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Grasp.Checks;

namespace Grasp.Work
{
	/// <summary>
	/// Uniquely identifies a revision of a topic in a knowledge base
	/// </summary>
	[Serializable]
	[TypeConverter(typeof(RevisionIdConverter))]
	public struct RevisionId : IEquatable<RevisionId>, IComparable<RevisionId>
	{
		#region Operators

		public static bool operator ==(RevisionId x, RevisionId y)
		{
			return x.Equals(y);
		}

		public static bool operator !=(RevisionId x, RevisionId y)
		{
			return !(x == y);
		}

		public static bool operator >(RevisionId x, RevisionId y)
		{
			return x.CompareTo(y) > 0;
		}

		public static bool operator <(RevisionId x, RevisionId y)
		{
			return x.CompareTo(y) < 0;
		}

		public static bool operator >=(RevisionId x, RevisionId y)
		{
			return x.CompareTo(y) >= 0;
		}

		public static bool operator <=(RevisionId x, RevisionId y)
		{
			return x.CompareTo(y) <= 0;
		}
		#endregion

		#region Parsing
		/// <summary>
		/// Parses the specified value as a SHA-1 hash to an instance of <see cref="RevisionId"/>
		/// </summary>
		/// <param name="value">The SHA-1 hash to parse as an <see cref="RevisionId"/></param>
		/// <returns>The <see cref="RevisionId"/> represented by the specified SHA-1 hash</returns>
		/// <exception cref="FormatException">Thrown if the value cannot be parsed as a SHA-1 hash</exception>
		public static RevisionId Parse(string value)
		{
			RevisionId revisionId;

			if(!TryParse(value, out revisionId))
			{
				throw new FormatException(Resources.InvalidRevisionId.FormatInvariant(value));
			}

			return revisionId;
		}

		/// <summary>
		/// Attempts to parse the specified value as a SHA-1 hash to an instance of <see cref="RevisionId"/>
		/// </summary>
		/// <param name="value">The SHA-1 hash  to parse as an <see cref="RevisionId"/></param>
		/// <param name="revisionId">The result of parsing the specified SHA-1 hash as an <see cref="RevisionId"/> (<see cref="Disconnected"/> otherwise)</param>
		/// <param name="sha1hash">The numeric value of the SHA-1 hash (0 otherwise)</param>
		/// <returns>Whether the parse succeeded</returns>
		public static bool TryParse(string value, out RevisionId revisionId, out long sha1hash)
		{
			if(Check.That(value).IsNullOrEmpty())
			{
				revisionId = RevisionId.Disconnected;
				sha1hash = 0;

				return true;
			}
			else
			{
				value = value.Trim();

				if(value.Length == 40 && Int64.TryParse(value, NumberStyles.AllowHexSpecifier, NumberFormatInfo.CurrentInfo, out sha1hash))
				{
					revisionId = new RevisionId(value);

					return true;
				}
				else
				{
					revisionId = Disconnected;
					sha1hash = 0;

					return false;
				}
			}
		}

		/// <summary>
		/// Attempts to parse the specified value as a SHA-1 hash to an instance of <see cref="RevisionId"/>
		/// </summary>
		/// <param name="value">The SHA-1 hash  to parse as an <see cref="RevisionId"/></param>
		/// <param name="revisionId">The result of parsing the specified SHA-1 hash as an <see cref="RevisionId"/> (<see cref="Disconnected"/> otherwise)</param>
		/// <returns>Whether the parse succeeded</returns>
		public static bool TryParse(string value, out RevisionId revisionId)
		{
			long sha1Hash;

			return TryParse(value, out revisionId, out sha1Hash);
		}
		#endregion

		/// <summary>
		/// Gets the <see cref="RevisionId"/> value which represents a revision disconnected from any history
		/// </summary>
		public static readonly RevisionId Disconnected = new RevisionId();

		/// <summary>
		/// Initializes a revision identifier with the specified SHA-1 hash
		/// </summary>
		/// <param name="sha1Hash">The SHA-1 hash of this revision identifier</param>
		/// <exception cref="FormatException">Thrown if the value cannot be parsed as a SHA-1 hash</exception>
		public RevisionId(string sha1Hash) : this()
		{
			Parse(sha1Hash);	// Hack or awesome?

			Sha1Hash = sha1Hash;
		}

		/// <summary>
		/// Initializes a revision identifier with the specified SHA-1 hash
		/// </summary>
		/// <param name="sha1Hash">The SHA-1 hash of this revision identifier</param>
		public RevisionId(long sha1Hash) : this()
		{
			Sha1Hash = sha1Hash.ToString("X2");
		}

		/// <summary>
		/// Checks whether this identifier is equal to the specified identifier
		/// </summary>
		/// <param name="other">The identifier to check</param>
		/// <returns>Whether this identifier is equal to the specified identifier</returns>
		public bool Equals(RevisionId other)
		{
			return Sha1Hash == other.Sha1Hash;
		}

		/// <summary>
		/// Checks whether the specified object is a <see cref="RevisionId"/> and is equal to this identifier
		/// </summary>
		/// <param name="obj">The possible identifier to check</param>
		/// <returns>Whether this identifier is equal to the specified identifier</returns>
		public override bool Equals(object obj)
		{
			return obj is RevisionId && Equals((RevisionId) obj);
		}

		/// <summary>
		/// Gets the hash code of <see cref="Sha1Hash"/>
		/// </summary>
		/// <returns>The hash code of <see cref="Sha1Hash"/></returns>
		public override int GetHashCode()
		{
			return Sha1Hash.GetHashCode();
		}

		/// <summary>
		/// Compares this identifier to the specified identifier
		/// </summary>
		/// <param name="other">The identifier to compare with this identifier</param>
		/// <returns>A signed number indicating the relative values of this and the other instance</returns>
		public int CompareTo(RevisionId other)
		{
			return Sha1Hash.CompareTo(other.Sha1Hash);
		}

		/// <summary>
		/// Gets the SHA-1 hash of this identifier
		/// </summary>
		public string Sha1Hash { get; private set; }

		/// <summary>
		/// Gets this identifier's SHA-1 hash
		/// </summary>
		/// <returns>This identifier's SHA-1 hash</returns>
		public override string ToString()
		{
			return Sha1Hash;
		}

		/// <summary>
		/// Gets the <see cref="Int64"/> value of this revision identifier
		/// </summary>
		/// <returns>The <see cref="Int64"/> value of this revision identifier</returns>
		public long ToInt64()
		{
			return Int64.Parse(Sha1Hash, NumberStyles.AllowHexSpecifier);
		}
	}
}