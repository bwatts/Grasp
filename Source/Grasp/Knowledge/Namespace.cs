using System;
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

			return _regex.IsMatch(value);
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

		private Namespace() : base("")
		{}

		/// <summary>
		/// Gets an enumerator of the identifiers which make up this namespace
		/// </summary>
		/// <returns>The identifiers which making up this namespace</returns>
		public IEnumerator<Identifier> GetEnumerator()
		{
			return Value.Split('.').Select(part => new Identifier(part)).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}