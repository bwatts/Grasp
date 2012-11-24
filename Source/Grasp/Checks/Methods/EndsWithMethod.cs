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
	/// Produces check rules for the <see cref="Checkable.EndsWith(ICheckable{string}, string)"/>, <see cref="Checkable.EndsWith(ICheckable{string}, string, StringComparison)"/>, and
	/// <see cref="Checkable.EndsWith(ICheckable{string}, string, bool, CultureInfo)"/> methods
	/// </summary>
	public sealed class EndsWithMethod : SingleTypeCheckMethod
	{
		public static readonly Field<string> _valueField = Field.On<EndsWithMethod>.For(x => x._value);
		public static readonly Field<StringComparison?> _comparisonTypeField = Field.On<EndsWithMethod>.For(x => x._comparisonType);
		public static readonly Field<bool?> _ignoreCaseField = Field.On<EndsWithMethod>.For(x => x._ignoreCase);
		public static readonly Field<CultureInfo> _cultureField = Field.On<EndsWithMethod>.For(x => x._culture);

		private string _value { get { return GetValue(_valueField); } set { SetValue(_valueField, value); } }
		private StringComparison? _comparisonType { get { return GetValue(_comparisonTypeField); } set { SetValue(_comparisonTypeField, value); } }
		private bool? _ignoreCase { get { return GetValue(_ignoreCaseField); } set { SetValue(_ignoreCaseField, value); } }
		private CultureInfo _culture { get { return GetValue(_cultureField); } set { SetValue(_cultureField, value); } }

		/// <summary>
		/// Initializes a method with the specified value
		/// </summary>
		/// <param name="value">The value to look for at the end of the string</param>
		public EndsWithMethod(string value)
		{
			_value = value;
		}

		/// <summary>
		/// Initializes a method with the specified value and comparison type
		/// </summary>
		/// <param name="value">The value to look for at the end of the string</param>
		/// <param name="comparisonType">The type of comparison to perform</param>
		public EndsWithMethod(string value, StringComparison comparisonType)
		{
			_value = value;
			_comparisonType = comparisonType;
		}

		/// <summary>
		/// Initializes a method with the specified value, flag to ignore case, and culture
		/// </summary>
		/// <param name="value">The value to look for at the end of the string</param>
		/// <param name="ignoreCase">Indicates whether the comparison is case-insensitive</param>
		/// <param name="culture">The culture which performs the comparison</param>
		public EndsWithMethod(string value, bool ignoreCase, CultureInfo culture)
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
		/// Gets the relevant overload from <see cref="Checkable.EndsWith(ICheckable{string}, string)"/>, <see cref="Checkable.EndsWith(ICheckable{string}, string, StringComparison)"/>, or
		/// <see cref="Checkable.EndsWith(ICheckable{string}, string, bool, CultureInfo)"/> methods
		/// </summary>
		/// <param name="checkType">The type of check</param>
		/// <returns>
		/// The relevant overload from <see cref="Checkable.EndsWith(ICheckable{string}, string)"/>, <see cref="Checkable.EndsWith(ICheckable{string}, string, StringComparison)"/>, or
		/// <see cref="Checkable.EndsWith(ICheckable{string}, string, bool, CultureInfo)"/> methods
		/// </returns>
		protected override MethodInfo GetMethod(Type checkType)
		{
			if(_culture != null)
			{
				return Reflect.Func<ICheckable<string>, string, bool, CultureInfo, Check<string>>(Checkable.EndsWith);
			}
			else if(_comparisonType != null)
			{
				return Reflect.Func<ICheckable<string>, string, StringComparison, Check<string>>(Checkable.EndsWith);
			}
			else
			{
				return Reflect.Func<ICheckable<string>, string, Check<string>>(Checkable.EndsWith);
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