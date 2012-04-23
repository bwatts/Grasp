using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target <see cref="System.String"/> matches a regular expression
	/// </summary>
	public sealed class CheckMatchesAttribute : CheckAttribute
	{
		/// <summary>
		/// Initializes an attribute with the specified pattern
		/// </summary>
		/// <param name="pattern">The pattern of the regular expression which performs the match</param>
		public CheckMatchesAttribute(string pattern)
		{
			Contract.Requires(pattern != null);

			Pattern = pattern;
		}

		/// <summary>
		/// Gets the pattern of the regular expression which performs the match
		/// </summary>
		public string Pattern { get; private set; }

		/// <summary>
		/// Gets or sets the options with which to configure the regular expression
		/// </summary>
		public RegexOptions? Options { get; set; }

		/// <summary>
		/// Gets an instance of <see cref="MatchesMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="MatchesMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return Options == null ? new MatchesMethod(Pattern) : new MatchesMethod(Pattern, Options.Value);
		}
	}
}