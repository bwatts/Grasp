using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Cloak.Reflection;

namespace Grasp.Checks.Methods
{
	/// <summary>
	/// Produces check rules for the <see cref="Checkable.Matches(ICheckable{string}, Regex)"/>, <see cref="Checkable.Matches(ICheckable{string}, string)"/>,
	/// and <see cref="Checkable.Matches(ICheckable{string}, string, RegexOptions)"/> methods
	/// </summary>
	public sealed class MatchesMethod : SingleTypeCheckMethod
	{
		public static readonly Field<Regex> _regexField = Field.On<MatchesMethod>.For(x => x._regex);
		public static readonly Field<string> _patternField = Field.On<MatchesMethod>.For(x => x._pattern);
		public static readonly Field<RegexOptions?> _optionsField = Field.On<MatchesMethod>.For(x => x._options);

		private Regex _regex { get { return GetValue(_regexField); } set { SetValue(_regexField, value); } }
		private string _pattern { get { return GetValue(_patternField); } set { SetValue(_patternField, value); } }
		private RegexOptions? _options { get { return GetValue(_optionsField); } set { SetValue(_optionsField, value); } }

		/// <summary>
		/// Initializes a method with the specified regular expression
		/// </summary>
		/// <param name="regex">The regular expression which performs the match</param>
		public MatchesMethod(Regex regex)
		{
			Contract.Requires(regex != null);

			_regex = regex;
		}

		/// <summary>
		/// Initializes a method with the specified regular expression pattern
		/// </summary>
		/// <param name="pattern">The pattern of the regular expression which performs the match</param>
		public MatchesMethod(string pattern)
		{
			Contract.Requires(pattern != null);

			_pattern = pattern;
		}

		/// <summary>
		/// Initializes a method with the specified regular expression pattern
		/// </summary>
		/// <param name="pattern">The pattern of the regular expression which performs the match</param>
		/// <param name="options">The options with which to configure the regular expression</param>
		public MatchesMethod(string pattern, RegexOptions options) : this(pattern)
		{
			_options = options;
		}

		/// <summary>
		/// Gets <see cref="System.String"/>
		/// </summary>
		protected override Type TargetType
		{
			get { return typeof(string); }
		}

		/// <summary>
		/// Gets the relevant overload from <see cref="Checkable.Matches(ICheckable{string}, Regex)"/>, <see cref="Checkable.Matches(ICheckable{string}, string)"/>,
		/// or <see cref="Checkable.Matches(ICheckable{string}, string, RegexOptions)"/>
		/// </summary>
		/// <param name="checkType">The type of check</param>
		/// <returns>
		/// The relevant overload from <see cref="Checkable.Matches(ICheckable{string}, Regex)"/>, <see cref="Checkable.Matches(ICheckable{string}, string)"/>,
		/// or <see cref="Checkable.Matches(ICheckable{string}, string, RegexOptions)"/>
		/// </returns>
		protected override MethodInfo GetMethod(Type checkType)
		{
			if(_regex != null)
			{
				return Reflect.Func<ICheckable<string>, Regex, Check<string>>(Checkable.Matches);
			}
			else if(_options != null)
			{
				return Reflect.Func<ICheckable<string>, string, RegexOptions, Check<string>>(Checkable.Matches);
			}
			else
			{
				return Reflect.Func<ICheckable<string>, string, Check<string>>(Checkable.Matches);
			}
		}

		/// <summary>
		/// Gets the arguments to the relevant overload
		/// </summary>
		/// <returns>The arguments to the relevant overload</returns>
		protected override IEnumerable<object> GetCheckArguments()
		{
			if(_regex != null)
			{
				yield return _regex;
			}
			else
			{
				yield return _pattern;

				if(_options != null)
				{
					yield return _options.Value;
				}
			}
		}
	}
}