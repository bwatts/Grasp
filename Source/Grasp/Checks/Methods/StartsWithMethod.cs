using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.Reflection;

namespace Grasp.Checks.Methods
{
	/// <summary>
	/// Produces check rules for the <see cref="Checkable.StartsWith(ICheckable{string}, string)"/>, <see cref="Checkable.StartsWith(ICheckable{string}, string, StringComparison)"/>, and
	/// <see cref="Checkable.StartsWith(ICheckable{string}, string, bool, CultureInfo)"/> methods
	/// </summary>
	public sealed class StartsWithMethod : SingleTypeCheckMethod
	{
		private readonly string _value;
		private readonly StringComparison? _comparisonType;
		private readonly bool? _ignoreCase;
		private readonly CultureInfo _culture;

		/// <summary>
		/// Initializes a method with the specified value
		/// </summary>
		/// <param name="value">The value to look for at the start of the string</param>
		public StartsWithMethod(string value)
		{
			_value = value;
		}

		/// <summary>
		/// Initializes a method with the specified value and comparison type
		/// </summary>
		/// <param name="value">The value to look for at the start of the string</param>
		/// <param name="comparisonType">The type of comparison to perform</param>
		public StartsWithMethod(string value, StringComparison comparisonType)
		{
			_value = value;
			_comparisonType = comparisonType;
		}

		/// <summary>
		/// Initializes a method with the specified value, flag to ignore case, and culture
		/// </summary>
		/// <param name="value">The value to look for at the start of the string</param>
		/// <param name="ignoreCase">Indicates whether the comparison is case-insensitive</param>
		/// <param name="culture">The culture which performs the comparison</param>
		public StartsWithMethod(string value, bool ignoreCase, CultureInfo culture)
		{
			_value = value;
			_ignoreCase = ignoreCase;
			_culture = culture;
		}

		/// <summary>
		/// Gets <see cref="System.String"/>
		/// </summary>
		protected override Type TargetType
		{
			get { return typeof(string); }
		}

		/// <summary>
		/// Gets the relevant overload from <see cref="Checkable.StartsWith(ICheckable{string}, string)"/>, <see cref="Checkable.StartsWith(ICheckable{string}, string, StringComparison)"/>, or
		/// <see cref="Checkable.StartsWith(ICheckable{string}, string, bool, CultureInfo)"/> methods
		/// </summary>
		/// <param name="checkType">The type of check</param>
		/// <returns>
		/// The relevant overload from <see cref="Checkable.StartsWith(ICheckable{string}, string)"/>, <see cref="Checkable.StartsWith(ICheckable{string}, string, StringComparison)"/>, or
		/// <see cref="Checkable.StartsWith(ICheckable{string}, string, bool, CultureInfo)"/> methods
		/// </returns>
		protected override MethodInfo GetMethod(Type checkType)
		{
			if(_culture != null)
			{
				return Reflect.Func<ICheckable<string>, string, bool, CultureInfo, Check<string>>(Checkable.StartsWith);
			}
			else if(_comparisonType != null)
			{
				return Reflect.Func<ICheckable<string>, string, StringComparison, Check<string>>(Checkable.StartsWith);
			}
			else
			{
				return Reflect.Func<ICheckable<string>, string, Check<string>>(Checkable.StartsWith);
			}
		}

		/// <summary>
		/// Gets the arguments to the relevant overload
		/// </summary>
		/// <returns>The arguments to the relevant overload</returns>
		protected override IEnumerable<object> GetCheckArguments()
		{
			yield return _value;

			if(_culture != null)
			{
				yield return _ignoreCase.Value;
				yield return _culture;
			}
			else
			{
				if(_comparisonType != null)
				{
					yield return _comparisonType.Value;
				}
			}
		}
	}
}