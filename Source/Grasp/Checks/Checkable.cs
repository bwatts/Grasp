using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Grasp.Checks
{
	/// <summary>
	/// Provides a set of static methods for checking objects which implement <see cref="ICheckable{T}"/>
	/// </summary>
	public static class Checkable
	{
		#region Passes
		/// <summary>
		/// Checks if the target data passes the specified check
		/// </summary>
		/// <typeparam name="T">The type of target data</typeparam>
		/// <param name="check">The base check</param>
		/// <param name="nextCheck">The check which should pass</param>
		/// <returns>A check which applies the base and next checks</returns>
		public static Check<T> Passes<T>(this ICheckable<T> check, Func<T, bool> nextCheck)
		{
			Contract.Requires(check != null);
			Contract.Requires(nextCheck != null);

			return new PassesCheck<T>(check, nextCheck);
		}

		/// <summary>
		/// Checks if the target data passed an already-applied check
		/// </summary>
		/// <typeparam name="T">The type of target data</typeparam>
		/// <param name="check">The base check</param>
		/// <param name="nextCheckResult">The result of the check which should pass</param>
		/// <returns>A check which applies the base and next check result</returns>
		public static Check<T> Passes<T>(this ICheckable<T> check, bool nextCheckResult)
		{
			Contract.Requires(check != null);

			return new FixedPassesCheck<T>(check, nextCheckResult);
		}

		private sealed class PassesCheck<T> : Check<T>
		{
			private readonly ICheckable<T> _baseCheck;
			private readonly Func<T, bool> _nextCheck;

			internal PassesCheck(ICheckable<T> baseCheck, Func<T, bool> nextCheck)
				: base(baseCheck.Target)
			{
				_baseCheck = baseCheck;
				_nextCheck = nextCheck;
			}

			public override bool Apply()
			{
				return _baseCheck.Apply() && _nextCheck(Target);
			}
		}

		private sealed class FixedPassesCheck<T> : Check<T>
		{
			private readonly ICheckable<T> _baseCheck;
			private readonly bool _nextCheckResult;

			internal FixedPassesCheck(ICheckable<T> baseCheck, bool nextCheckResult)
				: base(baseCheck.Target)
			{
				_baseCheck = baseCheck;
				_nextCheckResult = nextCheckResult;
			}

			public override bool Apply()
			{
				return _baseCheck.Apply() && _nextCheckResult;
			}
		}
		#endregion

		#region Fails
		/// <summary>
		/// Checks if the target data fails the specified check
		/// </summary>
		/// <typeparam name="T">The type of target data</typeparam>
		/// <param name="check">The base check</param>
		/// <param name="nextCheck">The check which should fail</param>
		/// <returns>A check which applies the base and next checks</returns>
		public static Check<T> Fails<T>(this ICheckable<T> check, Func<T, bool> nextCheck)
		{
			Contract.Requires(check != null);
			Contract.Requires(nextCheck != null);

			return new FailsCheck<T>(check, nextCheck);
		}

		/// <summary>
		/// Checks if the target data failed an already-applied check
		/// </summary>
		/// <typeparam name="T">The type of target data</typeparam>
		/// <param name="check">The base check</param>
		/// <param name="nextCheckResult">The result of the check which should fail</param>
		/// <returns>A check which applies the base and next check result</returns>
		public static Check<T> Fails<T>(this ICheckable<T> check, bool nextCheckResult)
		{
			Contract.Requires(check != null);

			return new FixedFailsCheck<T>(check, nextCheckResult);
		}

		private sealed class FailsCheck<T> : Check<T>
		{
			private readonly ICheckable<T> _baseCheck;
			private readonly Func<T, bool> _nextCheck;

			internal FailsCheck(ICheckable<T> baseCheck, Func<T, bool> nextCheck) : base(baseCheck.Target)
			{
				_baseCheck = baseCheck;
				_nextCheck = nextCheck;
			}

			public override bool Apply()
			{
				return _baseCheck.Apply() && !_nextCheck(Target);
			}
		}

		private sealed class FixedFailsCheck<T> : Check<T>
		{
			private readonly ICheckable<T> _baseCheck;
			private readonly bool _nextCheckResult;

			internal FixedFailsCheck(ICheckable<T> baseCheck, bool nextCheckResult) : base(baseCheck.Target)
			{
				_baseCheck = baseCheck;
				_nextCheckResult = nextCheckResult;
			}

			public override bool Apply()
			{
				return _baseCheck.Apply() && !_nextCheckResult;
			}
		}
		#endregion

		#region Boolean
		/// <summary>
		/// Checks if the target data is true
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<bool> IsTrue(this ICheckable<bool> check)
		{
			Contract.Requires(check != null);

			return check.Passes(t => t);
		}

		/// <summary>
		/// Checks if the target data is false
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<bool> IsFalse(this ICheckable<bool> check)
		{
			Contract.Requires(check != null);

			return check.Fails(t => t);
		}
		#endregion

		#region Char
		/// <summary>
		/// Checks if the target data is a letter or digit
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsLetterOrDigit(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.Passes(c => Char.IsLetterOrDigit(c));
		}

		/// <summary>
		/// Checks if the target data is a control character
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsControl(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.Passes(c => Char.IsControl(c));
		}

		/// <summary>
		/// Checks if the target data is a digit
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsDigit(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.Passes(c => Char.IsDigit(c));
		}

		/// <summary>
		/// Checks if the target data is a high surrogate
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsHighSurrogate(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.Passes(c => Char.IsHighSurrogate(c));
		}

		/// <summary>
		/// Checks if the target data is a letter
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsLetter(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.Passes(c => Char.IsLetter(c));
		}

		/// <summary>
		/// Checks if the target data is a lower-case character
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsLowerCase(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.Passes(c => Char.IsLower(c));
		}

		/// <summary>
		/// Checks if the target data is a low surrogate
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsLowSurrogate(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.Passes(c => Char.IsLowSurrogate(c));
		}

		/// <summary>
		/// Checks if the target data is a number
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsNumber(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.Passes(c => Char.IsNumber(c));
		}

		/// <summary>
		/// Checks if the target data is punctuation
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsPunctuation(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.Passes(c => Char.IsPunctuation(c));
		}

		/// <summary>
		/// Checks if the target data is a separator
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsSeparator(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.Passes(c => Char.IsSeparator(c));
		}

		/// <summary>
		/// Checks if the target data is a surrogate
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsSurrogate(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.Passes(c => Char.IsSurrogate(c));
		}

		/// <summary>
		/// Checks if the target data is a symbol
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsSymbol(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.Passes(c => Char.IsSymbol(c));
		}

		/// <summary>
		/// Checks if the target data is an upper-case character
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsUpperCase(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.Passes(c => Char.IsUpper(c));
		}

		/// <summary>
		/// Checks if the target data is whitespace
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsWhiteSpace(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.Passes(c => Char.IsWhiteSpace(c));
		}
		#endregion

		#region DateTime
		/// <summary>
		/// Checks if the target data falls on a weekend
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<DateTime> IsWeekend(this ICheckable<DateTime> check)
		{
			Contract.Requires(check != null);

			return check.Passes(d => d.DayOfWeek == DayOfWeek.Saturday || d.DayOfWeek == DayOfWeek.Sunday);
		}

		/// <summary>
		/// Checks if the target data falls on a weekday
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<DateTime> IsWeekday(this ICheckable<DateTime> check)
		{
			Contract.Requires(check != null);

			return check.Passes(d => d.DayOfWeek != DayOfWeek.Saturday && d.DayOfWeek != DayOfWeek.Sunday);
		}

		/// <summary>
		/// Checks if the target data falls on a leap year
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<DateTime> IsLeapYear(this ICheckable<DateTime> check)
		{
			Contract.Requires(check != null);

			return check.Passes(d => DateTime.IsLeapYear(d.Year));
		}
		#endregion

		#region Decimal
		/// <summary>
		/// Checks if the target data is even
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<decimal> IsEven(this ICheckable<decimal> check)
		{
			Contract.Requires(check != null);

			return check.Passes(n => n % 2 == 0);
		}

		/// <summary>
		/// Checks if the target data is odd
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<decimal> IsOdd(this ICheckable<decimal> check)
		{
			Contract.Requires(check != null);

			return check.Passes(n => n % 2 == 1);
		}

		/// <summary>
		/// Checks if the target data is positive (greater than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<decimal> IsPositive(this ICheckable<decimal> check)
		{
			Contract.Requires(check != null);

			return check.Passes(n => n > 0);
		}

		/// <summary>
		/// Checks if the target data is negative (less than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<decimal> IsNegative(this ICheckable<decimal> check)
		{
			Contract.Requires(check != null);

			return check.Passes(n => n < 0);
		}

		/// <summary>
		/// Checks if the target data is not positive (0 or less than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<decimal> IsNotPositive(this ICheckable<decimal> check)
		{
			Contract.Requires(check != null);

			return check.Passes(n => n <= 0);
		}

		/// <summary>
		/// Checks if the target data is not negative (0 or greater than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<decimal> IsNotNegative(this ICheckable<decimal> check)
		{
			Contract.Requires(check != null);

			return check.Passes(n => n >= 0);
		}

		/// <summary>
		/// Checks if the target data is a percentage in the range 0-100 (inclusive)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<decimal> IsAdjustedPercentage(this ICheckable<decimal> check)
		{
			Contract.Requires(check != null);

			return check.Passes(n => n >= 0 && n <= 100);
		}

		/// <summary>
		/// Checks if the target data is a percentage in the range 0-1 (inclusive)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<decimal> IsLiteralPercentage(this ICheckable<decimal> check)
		{
			Contract.Requires(check != null);

			return check.Passes(n => n >= 0 && n <= 1);
		}
		#endregion

		#region Double
		/// <summary>
		/// Checks if the target data is even
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<double> IsEven(this ICheckable<double> check)
		{
			Contract.Requires(check != null);

			return check.Passes(n => n % 2 == 0);
		}

		/// <summary>
		/// Checks if the target data is odd
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<double> IsOdd(this ICheckable<double> check)
		{
			Contract.Requires(check != null);

			return check.Passes(n => n % 2 == 1);
		}

		/// <summary>
		/// Checks if the target data is positive (greater than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<double> IsPositive(this ICheckable<double> check)
		{
			Contract.Requires(check != null);

			return check.Passes(n => n > 0);
		}

		/// <summary>
		/// Checks if the target data is negative (less than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<double> IsNegative(this ICheckable<double> check)
		{
			Contract.Requires(check != null);

			return check.Passes(n => n < 0);
		}

		/// <summary>
		/// Checks if the target data is not positive (0 or less than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<double> IsNotPositive(this ICheckable<double> check)
		{
			Contract.Requires(check != null);

			return check.Passes(n => n <= 0);
		}

		/// <summary>
		/// Checks if the target data is not negative (0 or greater than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<double> IsNotNegative(this ICheckable<double> check)
		{
			Contract.Requires(check != null);

			return check.Passes(n => n >= 0);
		}

		/// <summary>
		/// Checks if the target data is a percentage in the range 0-100 (inclusive)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<double> IsAdjustedPercentage(this ICheckable<double> check)
		{
			Contract.Requires(check != null);

			return check.Passes(n => n >= 0 && n <= 100);
		}

		/// <summary>
		/// Checks if the target data is a percentage in the range 0-1 (inclusive)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<double> IsLiteralPercentage(this ICheckable<double> check)
		{
			Contract.Requires(check != null);

			return check.Passes(n => n >= 0 && n <= 1);
		}
		#endregion

		#region Enumerable
		/// <summary>
		/// Checks if the target data contains the specified value
		/// </summary>
		/// <param name="check">The base check</param>
		/// <param name="value">The value to locate in the sequence</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<IEnumerable<T>> Contains<T>(this ICheckable<IEnumerable<T>> check, T value)
		{
			Contract.Requires(check != null);

			return check.Passes(source => source != null && source.Contains(value));
		}

		/// <summary>
		/// Checks if the target data contains the specified value using the specified comparer
		/// </summary>
		/// <param name="check">The base check</param>
		/// <param name="value">The value to locate in the sequence</param>
		/// <param name="comparer">The equality comparer which compares values</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<IEnumerable<T>> Contains<T>(this ICheckable<IEnumerable<T>> check, T value, IEqualityComparer<T> comparer)
		{
			Contract.Requires(check != null);
			Contract.Requires(comparer != null);

			return check.Passes(source => source != null && source.Contains(value, comparer));
		}

		/// <summary>
		/// Checks if the target data has no items
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<IEnumerable<T>> HasNone<T>(this ICheckable<IEnumerable<T>> check)
		{
			Contract.Requires(check != null);

			return check.Passes(source => source != null && !source.Any());
		}

		/// <summary>
		/// Checks if the target data has no items which match the specified item check
		/// </summary>
		/// <param name="check">The base check</param>
		/// <param name="itemCheck">The function which checks each item</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<IEnumerable<T>> HasNone<T>(this ICheckable<IEnumerable<T>> check, Func<T, bool> itemCheck)
		{
			Contract.Requires(check != null);
			Contract.Requires(itemCheck != null);

			return check.Passes(source => source != null && !source.Any(itemCheck));
		}

		/// <summary>
		/// Checks if the target data has at least one item
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<IEnumerable<T>> HasAny<T>(this ICheckable<IEnumerable<T>> check)
		{
			Contract.Requires(check != null);

			return check.Passes(source => source != null && source.Any());
		}

		/// <summary>
		/// Checks if the target data has at least one item which matches the specified item check
		/// </summary>
		/// <param name="check">The base check</param>
		/// <param name="itemCheck">The function which checks each item</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<IEnumerable<T>> HasAny<T>(this ICheckable<IEnumerable<T>> check, Func<T, bool> itemCheck)
		{
			Contract.Requires(check != null);
			Contract.Requires(itemCheck != null);

			return check.Passes(source => source != null && source.Any(itemCheck));
		}

		/// <summary>
		/// Checks if the target data has all items which matches the specified item check
		/// </summary>
		/// <param name="check">The base check</param>
		/// <param name="itemCheck">The function which checks each item</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<IEnumerable<T>> HasAll<T>(this ICheckable<IEnumerable<T>> check, Func<T, bool> itemCheck)
		{
			Contract.Requires(check != null);
			Contract.Requires(itemCheck != null);

			return check.Passes(source => source != null && source.All(itemCheck));
		}
		#endregion

		#region IsIn
		/// <summary>
		/// Checks if the target data is one of the specified values
		/// </summary>
		/// <param name="check">The base check</param>
		/// <param name="values">The sequence in which to locate the target data</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<T> IsIn<T>(this ICheckable<T> check, IEnumerable<T> values)
		{
			Contract.Requires(check != null);
			Contract.Requires(values != null);

			return check.Passes(t => values.Contains(t));
		}

		/// <summary>
		/// Checks if the target data is one of the specified values
		/// </summary>
		/// <param name="check">The base check</param>
		/// <param name="values">The sequence in which to locate the target data</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<T> IsIn<T>(this ICheckable<T> check, params T[] values)
		{
			Contract.Requires(check != null);
			Contract.Requires(values != null);

			return check.IsIn(values as IEnumerable<T>);
		}

		/// <summary>
		/// Checks if the target data is one of the specified values using the specified comparer
		/// </summary>
		/// <param name="check">The base check</param>
		/// <param name="comparer">The equality comparer which compares values</param>
		/// <param name="values">The sequence in which to locate the target data</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<T> IsIn<T>(this ICheckable<T> check, IEqualityComparer<T> comparer, IEnumerable<T> values)
		{
			Contract.Requires(check != null);
			Contract.Requires(comparer != null);
			Contract.Requires(values != null);

			return check.Passes(t => values.Contains(t, comparer));
		}

		/// <summary>
		/// Checks if the target data is one of the specified values using the specified comparer
		/// </summary>
		/// <param name="check">The base check</param>
		/// <param name="comparer">The equality comparer which compares values</param>
		/// <param name="values">The sequence in which to locate the target data</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<T> IsIn<T>(this ICheckable<T> check, IEqualityComparer<T> comparer, params T[] values)
		{
			Contract.Requires(check != null);
			Contract.Requires(comparer != null);
			Contract.Requires(values != null);

			return check.IsIn(comparer, values as IEnumerable<T>);
		}

		/// <summary>
		/// Checks if the target data is in the results of the specified query
		/// </summary>
		/// <param name="check">The base check</param>
		/// <param name="query">The query in which to locate the target data</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<T> IsIn<T>(this ICheckable<T> check, IQueryable<T> query)
		{
			Contract.Requires(check != null);
			Contract.Requires(query != null);

			return check.Passes(t => query.Contains(t));
		}

		/// <summary>
		/// Checks if the target data is in the results of the specified query
		/// </summary>
		/// <param name="check">The base check</param>
		/// <param name="comparer">The equality comparer which compares values</param>
		/// <param name="query">The query in which to locate the target data</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<T> IsIn<T>(this ICheckable<T> check, IEqualityComparer<T> comparer, IQueryable<T> query)
		{
			Contract.Requires(check != null);
			Contract.Requires(comparer != null);
			Contract.Requires(query != null);

			return check.Passes(t => query.Contains(t, comparer));
		}

		/// <summary>
		/// Checks if the target data is in the specified collection
		/// </summary>
		/// <param name="check">The base check</param>
		/// <param name="values">The collection in which to locate the target data</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<T> IsIn<T>(this ICheckable<T> check, ICollection<T> values)
		{
			Contract.Requires(check != null);
			Contract.Requires(values != null);

			return check.Passes(t => values.Contains(t));
		}
		#endregion

		#region Int32
		/// <summary>
		/// Checks if the target data is even
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<int> IsEven(this ICheckable<int> check)
		{
			Contract.Requires(check != null);

			return check.Passes(n => n % 2 == 0);
		}

		/// <summary>
		/// Checks if the target data is odd
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<int> IsOdd(this ICheckable<int> check)
		{
			Contract.Requires(check != null);

			return check.Passes(n => n % 2 == 1);
		}

		/// <summary>
		/// Checks if the target data is positive (greater than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<int> IsPositive(this ICheckable<int> check)
		{
			Contract.Requires(check != null);

			return check.Passes(n => n > 0);
		}

		/// <summary>
		/// Checks if the target data is negative (less than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<int> IsNegative(this ICheckable<int> check)
		{
			Contract.Requires(check != null);

			return check.Passes(n => n < 0);
		}

		/// <summary>
		/// Checks if the target data is not positive (0 or less than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<int> IsNotPositive(this ICheckable<int> check)
		{
			Contract.Requires(check != null);

			return check.Passes(n => n <= 0);
		}

		/// <summary>
		/// Checks if the target data is not negative (0 or greater than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<int> IsNotNegative(this ICheckable<int> check)
		{
			Contract.Requires(check != null);

			return check.Passes(n => n >= 0);
		}

		/// <summary>
		/// Checks if the target data is a percentage in the range 0-100 (inclusive)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<int> IsPercentage(this ICheckable<int> check)
		{
			Contract.Requires(check != null);

			return check.Passes(n => n >= 0 && n <= 100);
		}
		#endregion

		#region Int64
		/// <summary>
		/// Checks if the target data is even
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<long> IsEven(this ICheckable<long> check)
		{
			Contract.Requires(check != null);

			return check.Passes(n => n % 2 == 0);
		}

		/// <summary>
		/// Checks if the target data is odd
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<long> IsOdd(this ICheckable<long> check)
		{
			Contract.Requires(check != null);

			return check.Passes(n => n % 2 == 1);
		}

		/// <summary>
		/// Checks if the target data is positive (greater than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<long> IsPositive(this ICheckable<long> check)
		{
			Contract.Requires(check != null);

			return check.Passes(n => n > 0);
		}

		/// <summary>
		/// Checks if the target data is negative (less than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<long> IsNegative(this ICheckable<long> check)
		{
			Contract.Requires(check != null);

			return check.Passes(n => n < 0);
		}

		/// <summary>
		/// Checks if the target data is not positive (0 or less than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<long> IsNotPositive(this ICheckable<long> check)
		{
			Contract.Requires(check != null);

			return check.Passes(n => n <= 0);
		}

		/// <summary>
		/// Checks if the target data is not negative (0 or greater than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<long> IsNotNegative(this ICheckable<long> check)
		{
			Contract.Requires(check != null);

			return check.Passes(n => n >= 0);
		}

		/// <summary>
		/// Checks if the target data is a percentage in the range 0-100 (inclusive)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<long> IsPercentage(this ICheckable<long> check)
		{
			Contract.Requires(check != null);

			return check.Passes(n => n >= 0 && n <= 100);
		}
		#endregion

		#region Nullity
		/// <summary>
		/// Checks if the target data is null
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<T> IsNull<T>(this ICheckable<T> check)
		{
			Contract.Requires(check != null);

			return check.Passes(t => t == null);
		}

		/// <summary>
		/// Checks if the target data is not null
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<T> IsNotNull<T>(this ICheckable<T> check)
		{
			Contract.Requires(check != null);

			return check.Passes(t => t != null);
		}
		#endregion

		#region Queryable
		/// <summary>
		/// Checks if the target data contains the specified value
		/// </summary>
		/// <param name="check">The base check</param>
		/// <param name="value">The value to locate in the query</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<IQueryable<T>> Contains<T>(this ICheckable<IQueryable<T>> check, T value)
		{
			Contract.Requires(check != null);

			return check.Passes(source => source != null && source.Contains(value));
		}

		/// <summary>
		/// Checks if the target data contains the specified value using the specified comparer
		/// </summary>
		/// <param name="check">The base check</param>
		/// <param name="value">The value to locate in the query</param>
		/// <param name="comparer">The equality comparer which compares values</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<IQueryable<T>> Contains<T>(this ICheckable<IQueryable<T>> check, T value, IEqualityComparer<T> comparer)
		{
			Contract.Requires(check != null);
			Contract.Requires(comparer != null);

			return check.Passes(source => source != null && source.Contains(value, comparer));
		}

		/// <summary>
		/// Checks if the target data has no items
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<IQueryable<T>> HasNone<T>(this ICheckable<IQueryable<T>> check)
		{
			Contract.Requires(check != null);

			return check.Passes(source => source != null && !source.Any());
		}

		/// <summary>
		/// Checks if the target data has no items which match the specified item check
		/// </summary>
		/// <param name="check">The base check</param>
		/// <param name="itemCheck">The function which checks each item</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<IQueryable<T>> HasNone<T>(this ICheckable<IQueryable<T>> check, Expression<Func<T, bool>> itemCheck)
		{
			Contract.Requires(check != null);
			Contract.Requires(itemCheck != null);

			return check.Passes(source => source != null && !source.Any(itemCheck));
		}

		/// <summary>
		/// Checks if the target data has at least one item
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<IQueryable<T>> HasAny<T>(this ICheckable<IQueryable<T>> check)
		{
			Contract.Requires(check != null);

			return check.Passes(source => source != null && source.Any());
		}

		/// <summary>
		/// Checks if the target data has at least one item which matches the specified item check
		/// </summary>
		/// <param name="check">The base check</param>
		/// <param name="itemCheck">The function which checks each item</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<IQueryable<T>> HasAny<T>(this ICheckable<IQueryable<T>> check, Expression<Func<T, bool>> itemCheck)
		{
			Contract.Requires(check != null);
			Contract.Requires(itemCheck != null);

			return check.Passes(source => source != null && source.Any(itemCheck));
		}

		/// <summary>
		/// Checks if the target data has all items which matches the specified item check
		/// </summary>
		/// <param name="check">The base check</param>
		/// <param name="itemCheck">The function which checks each item</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<IQueryable<T>> HasAll<T>(this ICheckable<IQueryable<T>> check, Expression<Func<T, bool>> itemCheck)
		{
			Contract.Requires(check != null);
			Contract.Requires(itemCheck != null);

			return check.Passes(source => source != null && source.All(itemCheck));
		}
		#endregion
	}
}