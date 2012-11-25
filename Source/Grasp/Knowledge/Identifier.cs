using System;
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
	/// A symbol identifying a unique concept
	/// </summary>
	public sealed class Identifier : ComparableValue<Identifier, string>
	{
		private static readonly Regex _regex = new Regex(@"^[_A-Za-z]+\w*$", RegexOptions.Compiled);

		/// <summary>
		/// Determines if the specified text is a valid identifier
		/// </summary>
		/// <param name="value">The text to check if it is an identifier</param>
		/// <returns>Whether the specified text is an identifier</returns>
		[Pure]
		public static bool IsIdentifier(string value)
		{
			Contract.Requires(value != null);

			return _regex.IsMatch(value);
		}

		/// <summary>
		/// Initializes an identifier with the specified value
		/// </summary>
		/// <param name="value">The value of the identifier</param>
		public Identifier(string value) : base(value)
		{
			if(!IsIdentifier(value)) throw new FormatException(Resources.InvalidIdentifier.FormatCurrent(value));
		}
	}
}