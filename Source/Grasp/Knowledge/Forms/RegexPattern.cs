using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Grasp.Checks.Methods;
using Grasp.Checks.Rules;

namespace Grasp.Knowledge.Forms
{
	public class RegexPattern : Notion, ITextPattern
	{
		public static readonly Field<Regex> RegexField = Field.On<RegexPattern>.For(x => x.Regex);

		public RegexPattern(Regex regex)
		{
			Contract.Requires(regex != null);

			Regex = regex;
		}

		public RegexPattern(string regex, RegexOptions options = RegexOptions.None, TimeSpan? matchTimeout = null)
		{
			Contract.Requires(regex != null);

			Regex = matchTimeout == null ? new Regex(regex, options) : new Regex(regex, options, matchTimeout.Value);
		}

		public Regex Regex { get { return GetValue(RegexField); } private set { SetValue(RegexField, value); } }

		public Rule GetRule()
		{
			return new MatchesMethod(Regex).GetRule(typeof(string));
		}
	}
}