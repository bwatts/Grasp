﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Cloak;

namespace Grasp.Knowledge
{
	/// <summary>
	/// A hierarchical qualification of concepts
	/// </summary>
	public sealed class Namespace : ComparableValue<Namespace, string>, IEnumerable<Identifier>
	{
		public static Namespace operator +(Namespace @namespace, Namespace otherNamespace)
		{
			return @namespace.Append(otherNamespace);
		}

		public static FullName operator +(Namespace @namespace, Identifier identifier)
		{
			return @namespace.Append(identifier);
		}

		private static readonly Regex _regex = new Regex(@"^([_A-Za-z]+\w*)+(\.[_A-Za-z]+\w*)*$", RegexOptions.Compiled);

		/// <summary>
		/// Determines if the specified text is a valid namespace consisting of at least one identifier separated by "."
		/// </summary>
		/// <param name="value">The text to check if it is a namespace</param>
		/// <returns>Whether the specified text is a namespace</returns>
		[Pure]
		public static bool IsNamespace(string value)
		{
			Contract.Requires(value != null);

			return value == "" || _regex.IsMatch(value);
		}

		/// <summary>
		/// Gets the namespace representing the root of the hierarchy
		/// </summary>
		public static readonly Namespace Root = new Namespace();

		/// <summary>
		/// Initializes a namespace with the specified value
		/// </summary>
		/// <param name="value">The value of the namespace</param>
		public Namespace(string value) : base(value)
		{
			if(!IsNamespace(value)) throw new FormatException(Resources.InvalidNamespace.FormatCurrent(value));
		}

		/// <summary>
		/// Initializes a namespace rooted at the specified full name
		/// </summary>
		/// <param name="fullName">The full name in which to root the namespace</param>
		public Namespace(FullName fullName) : this(fullName.ToString())
		{}

		/// <summary>
		/// Initializes a namespace with the specified identifiers
		/// </summary>
		/// <param name="identifiers">The identifiers of the namespace</param>
		public Namespace(IEnumerable<Identifier> identifiers) : this(String.Join(".", identifiers.Select(identifier => identifier.ToString())))
		{}

		private Namespace() : base("")
		{}

		/// <summary>
		/// Gets an enumerator of the identifiers which make up this namespace
		/// </summary>
		/// <returns>The identifiers which making up this namespace</returns>
		public IEnumerator<Identifier> GetEnumerator()
		{
			if(this != Root)
			{
				foreach(var part in Value.Split('.'))
				{
					yield return new Identifier(part);
				}
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <summary>
		/// Gets a namespace consisting of this and the specified namespace
		/// </summary>
		/// <param name="other">The namespace to append to this namespace</param>
		/// <returns>A namespace consisting of this and the specified namespace</returns>
		public Namespace Append(Namespace other)
		{
			Contract.Requires(other != null);

			return new Namespace(String.Join(".", new[] { ToString(), other.ToString() }));
		}

		/// <summary>
		/// Gets a full name with this namespace and the specified identifier
		/// </summary>
		/// <param name="identifier">The identifier to to qualify with this namespace</param>
		/// <returns>A full name with this namespace and the specified identifier</returns>
		public FullName Append(Identifier identifier)
		{
			return new FullName(this, identifier);
		}
	}
}