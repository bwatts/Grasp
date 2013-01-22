using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Cloak.Linq;

namespace Grasp.Checks
{
	/// <summary>
	/// Provides a set of static methods for checking objects which implement <see cref="ICheckable{T}"/>
	/// </summary>
	public static class Checkable
	{
		// TODO: Change "Passes" and "Fails" to "IsTrue" and "IsFalse" in metadata

		#region IsTrue
		/// <summary>
		/// Checks if the target data is true
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<bool> IsTrue(this ICheckable<bool> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x);
		}

		/// <summary>
		/// Checks if the specified function returns <code>true</code> for the target data
		/// </summary>
		/// <typeparam name="T">The type of target data</typeparam>
		/// <param name="check">The base check</param>
		/// <param name="nextCheck">The check which should pass</param>
		/// <returns>A check which applies the base and next checks</returns>
		public static Check<T> IsTrue<T>(this ICheckable<T> check, Func<T, bool> nextCheck)
		{
			Contract.Requires(check != null);
			Contract.Requires(nextCheck != null);

			return new IsTrueCheck<T>(check, nextCheck);
		}

		/// <summary>
		/// Checks if the target data passed an already-applied check
		/// </summary>
		/// <typeparam name="T">The type of target data</typeparam>
		/// <param name="check">The base check</param>
		/// <param name="nextCheckResult">The result of the check which should pass</param>
		/// <returns>A check which applies the base and next check result</returns>
		public static Check<T> IsTrue<T>(this ICheckable<T> check, bool nextCheckResult)
		{
			Contract.Requires(check != null);

			return new FixedIsTrueCheck<T>(check, nextCheckResult);
		}

		private sealed class IsTrueCheck<T> : Check<T>
		{
			private readonly ICheckable<T> _baseCheck;
			private readonly Func<T, bool> _nextCheck;

			internal IsTrueCheck(ICheckable<T> baseCheck, Func<T, bool> nextCheck) : base(baseCheck.Target)
			{
				_baseCheck = baseCheck;
				_nextCheck = nextCheck;
			}

			public override bool Apply()
			{
				return _baseCheck.Apply() && _nextCheck(Target);
			}
		}

		private sealed class FixedIsTrueCheck<T> : Check<T>
		{
			private readonly ICheckable<T> _baseCheck;
			private readonly bool _nextCheckResult;

			internal FixedIsTrueCheck(ICheckable<T> baseCheck, bool nextCheckResult) : base(baseCheck.Target)
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

		#region IsFalse
		/// <summary>
		/// Checks if the target data is false
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<bool> IsFalse(this ICheckable<bool> check)
		{
			Contract.Requires(check != null);

			return check.IsFalse(x => x);
		}

		/// <summary>
		/// Checks if the specified function returns <code>false</code> for the target data
		/// </summary>
		/// <typeparam name="T">The type of target data</typeparam>
		/// <param name="check">The base check</param>
		/// <param name="nextCheck">The check which should fail</param>
		/// <returns>A check which applies the base and next checks</returns>
		public static Check<T> IsFalse<T>(this ICheckable<T> check, Func<T, bool> nextCheck)
		{
			Contract.Requires(check != null);
			Contract.Requires(nextCheck != null);

			return new IsFalseCheck<T>(check, nextCheck);
		}

		/// <summary>
		/// Checks if the target data failed an already-applied check
		/// </summary>
		/// <typeparam name="T">The type of target data</typeparam>
		/// <param name="check">The base check</param>
		/// <param name="nextCheckResult">The result of the check which should fail</param>
		/// <returns>A check which applies the base and next check result</returns>
		public static Check<T> IsFalse<T>(this ICheckable<T> check, bool nextCheckResult)
		{
			Contract.Requires(check != null);

			return new FixedIsFalseCheck<T>(check, nextCheckResult);
		}

		private sealed class IsFalseCheck<T> : Check<T>
		{
			private readonly ICheckable<T> _baseCheck;
			private readonly Func<T, bool> _nextCheck;

			internal IsFalseCheck(ICheckable<T> baseCheck, Func<T, bool> nextCheck) : base(baseCheck.Target)
			{
				_baseCheck = baseCheck;
				_nextCheck = nextCheck;
			}

			public override bool Apply()
			{
				return _baseCheck.Apply() && !_nextCheck(Target);
			}
		}

		private sealed class FixedIsFalseCheck<T> : Check<T>
		{
			private readonly ICheckable<T> _baseCheck;
			private readonly bool _nextCheckResult;

			internal FixedIsFalseCheck(ICheckable<T> baseCheck, bool nextCheckResult) : base(baseCheck.Target)
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

		#region Char
		/// <summary>
		/// Checks if the target data is a letter or digit
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsLetterOrDigit(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => Char.IsLetterOrDigit(x));
		}

		/// <summary>
		/// Checks if the target data is a control character
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsControl(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => Char.IsControl(x));
		}

		/// <summary>
		/// Checks if the target data is a digit
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsDigit(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => Char.IsDigit(x));
		}

		/// <summary>
		/// Checks if the target data is a high surrogate
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsHighSurrogate(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => Char.IsHighSurrogate(x));
		}

		/// <summary>
		/// Checks if the target data is a letter
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsLetter(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => Char.IsLetter(x));
		}

		/// <summary>
		/// Checks if the target data is a lower-case character
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsLowerCase(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => Char.IsLower(x));
		}

		/// <summary>
		/// Checks if the target data is a low surrogate
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsLowSurrogate(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => Char.IsLowSurrogate(x));
		}

		/// <summary>
		/// Checks if the target data is a number
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsNumber(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => Char.IsNumber(x));
		}

		/// <summary>
		/// Checks if the target data is punctuation
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsPunctuation(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => Char.IsPunctuation(x));
		}

		/// <summary>
		/// Checks if the target data is a separator
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsSeparator(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => Char.IsSeparator(x));
		}

		/// <summary>
		/// Checks if the target data is a surrogate
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsSurrogate(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => Char.IsSurrogate(x));
		}

		/// <summary>
		/// Checks if the target data is a symbol
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsSymbol(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => Char.IsSymbol(x));
		}

		/// <summary>
		/// Checks if the target data is an upper-case character
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsUpperCase(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => Char.IsUpper(x));
		}

		/// <summary>
		/// Checks if the target data is whitespace
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsWhiteSpace(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => Char.IsWhiteSpace(x));
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

			return check.IsTrue(x => x.DayOfWeek == DayOfWeek.Saturday || x.DayOfWeek == DayOfWeek.Sunday);
		}

		/// <summary>
		/// Checks if the target data falls on a weekday
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<DateTime> IsWeekday(this ICheckable<DateTime> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x.DayOfWeek != DayOfWeek.Saturday && x.DayOfWeek != DayOfWeek.Sunday);
		}

		/// <summary>
		/// Checks if the target data falls on a particular day of the week
		/// </summary>
		/// <param name="check">The base check</param>
		/// <param name="dayOfWeek">The day of the week for which to check the target date</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<DateTime> IsDayOfWeek(this ICheckable<DateTime> check, DayOfWeek dayOfWeek)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x.DayOfWeek == dayOfWeek);
		}

		/// <summary>
		/// Checks if the target data falls on a leap year
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<DateTime> IsLeapYear(this ICheckable<DateTime> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => DateTime.IsLeapYear(x.Year));
		}
		#endregion

		// TODO: Metadata for TimeSpan checks

		#region TimeSpan
		/// <summary>
		/// Checks if the target data is the infinite time span
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<TimeSpan> IsInfinite(this ICheckable<TimeSpan> check)
		{
			Contract.Requires(check != null);

			return check.IsEqualTo(Timeout.InfiniteTimeSpan);
		}

		/// <summary>
		/// Checks if the target data is positive (greater than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<TimeSpan> IsPositive(this ICheckable<TimeSpan> check)
		{
			Contract.Requires(check != null);

			return check.IsGreaterThan(TimeSpan.Zero);
		}

		/// <summary>
		/// Checks if the target data is negative (less than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<TimeSpan> IsNegative(this ICheckable<TimeSpan> check)
		{
			Contract.Requires(check != null);

			return check.IsLessThan(TimeSpan.Zero);
		}

		/// <summary>
		/// Checks if the target data is not positive (not greater than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<TimeSpan> IsNotPositive(this ICheckable<TimeSpan> check)
		{
			Contract.Requires(check != null);

			return check.IsLessThanOrEqualTo(TimeSpan.Zero);
		}

		/// <summary>
		/// Checks if the target data is not negative (not less than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<TimeSpan> IsNotNegative(this ICheckable<TimeSpan> check)
		{
			Contract.Requires(check != null);

			return check.IsGreaterThanOrEqualTo(TimeSpan.Zero);
		}

		/// <summary>
		/// Checks if the target data is an unsigned measure of time, either the infinite time span or greater than 0
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<TimeSpan> IsInfiniteOrPositive(this ICheckable<TimeSpan> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x == Timeout.InfiniteTimeSpan || x > TimeSpan.Zero);
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

			return check.IsTrue(x => x % 2 == 0);
		}

		/// <summary>
		/// Checks if the target data is odd
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<decimal> IsOdd(this ICheckable<decimal> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x % 2 == 1);
		}

		/// <summary>
		/// Checks if the target data is positive (greater than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<decimal> IsPositive(this ICheckable<decimal> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x > 0);
		}

		/// <summary>
		/// Checks if the target data is negative (less than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<decimal> IsNegative(this ICheckable<decimal> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x < 0);
		}

		/// <summary>
		/// Checks if the target data is not positive (not greater than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<decimal> IsNotPositive(this ICheckable<decimal> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x <= 0);
		}

		/// <summary>
		/// Checks if the target data is not negative (not less than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<decimal> IsNotNegative(this ICheckable<decimal> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x >= 0);
		}

		/// <summary>
		/// Checks if the target data is a percentage in the range 0-100 (inclusive)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<decimal> IsAdjustedPercentage(this ICheckable<decimal> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x >= 0 && x <= 100);
		}

		/// <summary>
		/// Checks if the target data is a percentage in the range 0-1 (inclusive)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<decimal> IsLiteralPercentage(this ICheckable<decimal> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x >= 0 && x <= 1);
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

			return check.IsTrue(x => x % 2 == 0);
		}

		/// <summary>
		/// Checks if the target data is odd
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<double> IsOdd(this ICheckable<double> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x % 2 == 1);
		}

		/// <summary>
		/// Checks if the target data is positive (greater than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<double> IsPositive(this ICheckable<double> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x > 0);
		}

		/// <summary>
		/// Checks if the target data is negative (less than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<double> IsNegative(this ICheckable<double> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x < 0);
		}

		/// <summary>
		/// Checks if the target data is not positive (not greater than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<double> IsNotPositive(this ICheckable<double> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x <= 0);
		}

		/// <summary>
		/// Checks if the target data is not negative (not less than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<double> IsNotNegative(this ICheckable<double> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x >= 0);
		}

		/// <summary>
		/// Checks if the target data is a percentage in the range 0-100 (inclusive)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<double> IsAdjustedPercentage(this ICheckable<double> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x >= 0 && x <= 100);
		}

		/// <summary>
		/// Checks if the target data is a percentage in the range 0-1 (inclusive)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<double> IsLiteralPercentage(this ICheckable<double> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x >= 0 && x <= 1);
		}
		#endregion

		#region Enumerable
		/// <summary>
		/// Checks if the target data contains the specified value
		/// </summary>
		/// <typeparam name="T">The type of items in the target data</typeparam>
		/// <param name="check">The base check</param>
		/// <param name="value">The value to locate in the sequence</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<IEnumerable<T>> Contains<T>(this ICheckable<IEnumerable<T>> check, T value)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x != null && x.Contains(value));
		}

		/// <summary>
		/// Checks if the target data contains the specified value using the specified comparer
		/// </summary>
		/// <typeparam name="T">The type of items in the target data</typeparam>
		/// <param name="check">The base check</param>
		/// <param name="value">The value to locate in the sequence</param>
		/// <param name="comparer">The equality comparer which compares values</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<IEnumerable<T>> Contains<T>(this ICheckable<IEnumerable<T>> check, T value, IEqualityComparer<T> comparer)
		{
			Contract.Requires(check != null);
			Contract.Requires(comparer != null);

			return check.IsTrue(x => x != null && x.Contains(value, comparer));
		}

		/// <summary>
		/// Checks if the target data has no items
		/// </summary>
		/// <typeparam name="T">The type of items in the target data</typeparam>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<IEnumerable<T>> HasNone<T>(this ICheckable<IEnumerable<T>> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x != null && !x.Any());
		}

		/// <summary>
		/// Checks if the target data has no items which match the specified item check
		/// </summary>
		/// <typeparam name="T">The type of items in the target data</typeparam>
		/// <param name="check">The base check</param>
		/// <param name="itemCheck">The function which checks each item</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<IEnumerable<T>> HasNone<T>(this ICheckable<IEnumerable<T>> check, Func<T, bool> itemCheck)
		{
			Contract.Requires(check != null);
			Contract.Requires(itemCheck != null);

			return check.IsTrue(x => x != null && !x.Any(itemCheck));
		}

		/// <summary>
		/// Checks if the target data has at least one item
		/// </summary>
		/// <typeparam name="T">The type of items in the target data</typeparam>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<IEnumerable<T>> HasAny<T>(this ICheckable<IEnumerable<T>> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x != null && x.Any());
		}

		/// <summary>
		/// Checks if the target data has at least one item which matches the specified item check
		/// </summary>
		/// <typeparam name="T">The type of items in the target data</typeparam>
		/// <param name="check">The base check</param>
		/// <param name="itemCheck">The function which checks each item</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<IEnumerable<T>> HasAny<T>(this ICheckable<IEnumerable<T>> check, Func<T, bool> itemCheck)
		{
			Contract.Requires(check != null);
			Contract.Requires(itemCheck != null);

			return check.IsTrue(x => x != null && x.Any(itemCheck));
		}

		/// <summary>
		/// Checks if the target data has all items which matches the specified item check
		/// </summary>
		/// <typeparam name="T">The type of items in the target data</typeparam>
		/// <param name="check">The base check</param>
		/// <param name="itemCheck">The function which checks each item</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<IEnumerable<T>> HasAll<T>(this ICheckable<IEnumerable<T>> check, Func<T, bool> itemCheck)
		{
			Contract.Requires(check != null);
			Contract.Requires(itemCheck != null);

			return check.IsTrue(x => x != null && x.All(itemCheck));
		}

		/// <summary>
		/// Checks if the target data has all distinct items
		/// </summary>
		/// <typeparam name="T">The type of items in the target data</typeparam>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<IEnumerable<T>> AreDistinct<T>(this ICheckable<IEnumerable<T>> check)
		{
			return check.IsTrue(x => x != null && x.GroupBy(item => item).Any(itemGroup => itemGroup.Skip(1).Any()));
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

			return check.IsTrue(x => values.Contains(x));
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

			return check.IsTrue(x => values.Contains(x, comparer));
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

			return check.IsTrue(x => query.Contains(x));
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

			return check.IsTrue(x => query.Contains(x, comparer));
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

			return check.IsTrue(x => values.Contains(x));
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

			return check.IsTrue(x => x % 2 == 0);
		}

		/// <summary>
		/// Checks if the target data is odd
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<int> IsOdd(this ICheckable<int> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x % 2 == 1);
		}

		/// <summary>
		/// Checks if the target data is positive (greater than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<int> IsPositive(this ICheckable<int> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x > 0);
		}

		/// <summary>
		/// Checks if the target data is negative (less than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<int> IsNegative(this ICheckable<int> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x < 0);
		}

		/// <summary>
		/// Checks if the target data is not positive (not greater than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<int> IsNotPositive(this ICheckable<int> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x <= 0);
		}

		/// <summary>
		/// Checks if the target data is not negative (not less than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<int> IsNotNegative(this ICheckable<int> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x >= 0);
		}

		/// <summary>
		/// Checks if the target data is a percentage in the range 0-100 (inclusive)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<int> IsPercentage(this ICheckable<int> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x >= 0 && x <= 100);
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

			return check.IsTrue(x => x % 2 == 0);
		}

		/// <summary>
		/// Checks if the target data is odd
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<long> IsOdd(this ICheckable<long> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x % 2 == 1);
		}

		/// <summary>
		/// Checks if the target data is positive (greater than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<long> IsPositive(this ICheckable<long> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x > 0);
		}

		/// <summary>
		/// Checks if the target data is negative (less than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<long> IsNegative(this ICheckable<long> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x < 0);
		}

		/// <summary>
		/// Checks if the target data is not positive (not greater than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<long> IsNotPositive(this ICheckable<long> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x <= 0);
		}

		/// <summary>
		/// Checks if the target data is not negative (not less than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<long> IsNotNegative(this ICheckable<long> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x >= 0);
		}

		/// <summary>
		/// Checks if the target data is a percentage in the range 0-100 (inclusive)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<long> IsPercentage(this ICheckable<long> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x >= 0 && x <= 100);
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

			return check.IsTrue(x => x == null);
		}

		/// <summary>
		/// Checks if the target data is not null
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<T> IsNotNull<T>(this ICheckable<T> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x != null);
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

			return check.IsTrue(x => x != null && x.Contains(value));
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

			return check.IsTrue(x => x != null && x.Contains(value, comparer));
		}

		/// <summary>
		/// Checks if the target data has no items
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<IQueryable<T>> HasNone<T>(this ICheckable<IQueryable<T>> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x != null && !x.Any());
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

			return check.IsTrue(x => x != null && !x.Any(itemCheck));
		}

		/// <summary>
		/// Checks if the target data has at least one item
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<IQueryable<T>> HasAny<T>(this ICheckable<IQueryable<T>> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x != null && x.Any());
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

			return check.IsTrue(x => x != null && x.Any(itemCheck));
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

			return check.IsTrue(x => x != null && x.All(itemCheck));
		}

		/// <summary>
		/// Checks if the target data has all distinct items
		/// </summary>
		/// <typeparam name="T">The type of items in the target data</typeparam>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<IQueryable<T>> AreDistinct<T>(this ICheckable<IQueryable<T>> check)
		{
			return check.IsTrue(x => x != null && x.GroupBy(item => item).Any(itemGroup => itemGroup.Skip(1).Any()));
		}
		#endregion

		#region Comparisons
		/// <summary>
		/// Checks if the target data is less than the specified value
		/// </summary>
		/// <param name="check">The base check</param>
		/// <param name="value">The value to compare</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<T> IsLessThan<T>(this ICheckable<T> check, T value) where T : IComparable<T>
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x != null && x.CompareTo(value) < 0);
		}

		/// <summary>
		/// Checks if the target data is less than or equal to the specified value
		/// </summary>
		/// <param name="check">The base check</param>
		/// <param name="value">The value to compare</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<T> IsLessThanOrEqualTo<T>(this ICheckable<T> check, T value) where T : IComparable<T>
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x != null && x.CompareTo(value) <= 0);
		}

		/// <summary>
		/// Checks if the target data is equal to the specified value using the specified comparer
		/// </summary>
		/// <param name="check">The base check</param>
		/// <param name="comparer">The equality comparer to use for the comparison</param>
		/// <param name="value">The value to compare</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<T> IsEqualTo<T>(this ICheckable<T> check, T value, IEqualityComparer<T> comparer)
		{
			Contract.Requires(check != null);
			Contract.Requires(comparer != null);

			return check.IsTrue(x => comparer.Equals(x, value));
		}

		/// <summary>
		/// Checks if the target data is equal to the specified value using the default comparer for <typeparamref name="T"/>
		/// </summary>
		/// <param name="check">The base check</param>
		/// <param name="value">The value to compare</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<T> IsEqualTo<T>(this ICheckable<T> check, T value)
		{
			Contract.Requires(check != null);

			return check.IsEqualTo(value, EqualityComparer<T>.Default);
		}

		/// <summary>
		/// Checks if the target data is greater than or equal to the specified value
		/// </summary>
		/// <param name="check">The base check</param>
		/// <param name="value">The value to compare</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<T> IsGreaterThanOrEqualTo<T>(this ICheckable<T> check, T value) where T : IComparable<T>
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x != null && x.CompareTo(value) >= 0);
		}

		/// <summary>
		/// Checks if the target data is greater than the specified value
		/// </summary>
		/// <param name="check">The base check</param>
		/// <param name="value">The value to compare</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<T> IsGreaterThan<T>(this ICheckable<T> check, T value) where T : IComparable<T>
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x != null && x.CompareTo(value) > 0);
		}

		/// <summary>
		/// Checks if the target data is between the specified minimum and maximum using the specified boundary type
		/// </summary>
		/// <param name="check">The base check</param>
		/// <param name="minimum">The minimum value in the range</param>
		/// <param name="maximum">The maximum value in the range</param>
		/// <param name="boundaryType">The specification of how to handle the minimum and maximum values</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<T> IsBetween<T>(this ICheckable<T> check, T minimum, T maximum, BoundaryType boundaryType) where T : IComparable<T>
		{
			Contract.Requires(check != null);
			Contract.Requires(minimum == null || minimum.CompareTo(maximum) <= 0);

			if(check.Target != null)
			{
				switch(boundaryType)
				{
					case BoundaryType.Inclusive:
						return check.IsTrue(x => x.CompareTo(minimum) >= 0 && x.CompareTo(maximum) <= 0);
					case BoundaryType.ExcludeMinimum:
						return check.IsTrue(x => x.CompareTo(minimum) > 0 && x.CompareTo(maximum) <= 0);
					case BoundaryType.ExcludeMaximum:
						return check.IsTrue(x => x.CompareTo(minimum) >= 0 && x.CompareTo(maximum) < 0);
					case BoundaryType.Exclusive:
						return check.IsTrue(x => x.CompareTo(minimum) > 0 && x.CompareTo(maximum) < 0);
					default:
						throw new ArgumentOutOfRangeException("boundaryType");
				}
			}
			else
			{
				bool isInNullRange;

				switch(boundaryType)
				{
					case BoundaryType.Inclusive:
						isInNullRange = minimum == null && maximum == null;
						break;
					case BoundaryType.ExcludeMinimum:
						isInNullRange = maximum == null;
						break;
					case BoundaryType.ExcludeMaximum:
						isInNullRange = minimum == null;
						break;
					case BoundaryType.Exclusive:
						isInNullRange = false;
						break;
					default:
						throw new ArgumentOutOfRangeException("boundaryType");
				}

				return check.IsTrue(isInNullRange);
			}
		}

		/// <summary>
		/// Checks if the target data is between the specified minimum and maximum using an inclusive boundary
		/// </summary>
		/// <param name="check">The base check</param>
		/// <param name="minimum">The minimum value in the range</param>
		/// <param name="maximum">The maximum value in the range</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<T> IsBetween<T>(this ICheckable<T> check, T minimum, T maximum) where T : IComparable<T>
		{
			Contract.Requires(check != null);
			Contract.Requires(minimum == null || minimum.CompareTo(maximum) <= 0);

			return check.IsBetween(minimum, maximum, default(BoundaryType));
		}
		#endregion

		#region Single
		/// <summary>
		/// Checks if the target data is even
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<float> IsEven(this ICheckable<float> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x % 2 == 0);
		}

		/// <summary>
		/// Checks if the target data is odd
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<float> IsOdd(this ICheckable<float> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x % 2 == 1);
		}

		/// <summary>
		/// Checks if the target data is positive (greater than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<float> IsPositive(this ICheckable<float> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x > 0);
		}

		/// <summary>
		/// Checks if the target data is negative (less than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<float> IsNegative(this ICheckable<float> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x < 0);
		}

		/// <summary>
		/// Checks if the target data is not positive (not greater than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<float> IsNotPositive(this ICheckable<float> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x <= 0);
		}

		/// <summary>
		/// Checks if the target data is not negative (not less than 0)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<float> IsNotNegative(this ICheckable<float> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x >= 0);
		}

		/// <summary>
		/// Checks if the target data is a percentage in the range 0-100 (inclusive)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<float> IsAdjustedPercentage(this ICheckable<float> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x >= 0 && x <= 100);
		}

		/// <summary>
		/// Checks if the target data is a percentage in the range 0-1 (inclusive)
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<float> IsLiteralPercentage(this ICheckable<float> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x >= 0 && x <= 1);
		}
		#endregion

		#region String Content
		/// <summary>
		/// Checks if the target data is empty
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<string> IsEmpty(this ICheckable<string> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x == "");
		}

		/// <summary>
		/// Checks if the target data is not empty
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<string> IsNotEmpty(this ICheckable<string> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => x != "");
		}

		/// <summary>
		/// Checks if the target data is null or empty
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<string> IsNullOrEmpty(this ICheckable<string> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => String.IsNullOrEmpty(x));
		}

		/// <summary>
		/// Checks if the target data is not null or empty
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<string> IsNotNullOrEmpty(this ICheckable<string> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => !String.IsNullOrEmpty(x));
		}

		/// <summary>
		/// Checks if the target data is null or whitespace
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<string> IsNullOrWhiteSpace(this ICheckable<string> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => String.IsNullOrWhiteSpace(x));
		}

		/// <summary>
		/// Checks if the target data is not null or whitespace
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<string> IsNotNullOrWhiteSpace(this ICheckable<string> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(x => !String.IsNullOrWhiteSpace(x));
		}

		/// <summary>
		/// Checks if the target data starts with the specified value
		/// </summary>
		/// <param name="check">The base check</param>
		/// <param name="value">The string to compare</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<string> StartsWith(this ICheckable<string> check, string value)
		{
			Contract.Requires(check != null);
			Contract.Requires(value != null);

			return check.IsTrue(x => x != null && x.StartsWith(value));
		}

		/// <summary>
		/// Checks if the target data starts with the specified value using the specified comparison type
		/// </summary>
		/// <param name="check">The base check</param>
		/// <param name="value">The string to compare</param>
		/// <param name="comparisonType">The type of comparison to perform</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<string> StartsWith(this ICheckable<string> check, string value, StringComparison comparisonType)
		{
			Contract.Requires(check != null);
			Contract.Requires(value != null);

			return check.IsTrue(x => x != null && x.StartsWith(value, comparisonType));
		}

		/// <summary>
		/// Checks if the target data starts with the specified value using the specified case rule and culture
		/// </summary>
		/// <param name="check">The base check</param>
		/// <param name="value">The string to compare</param>
		/// <param name="ignoreCase">Whether to ignore case during the comparison</param>
		/// <param name="culture">The rules which govern the comparison</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<string> StartsWith(this ICheckable<string> check, string value, bool ignoreCase, CultureInfo culture)
		{
			Contract.Requires(check != null);
			Contract.Requires(value != null);
			Contract.Requires(culture != null);

			return check.IsTrue(x => x != null && x.StartsWith(value, ignoreCase, culture));
		}

		/// <summary>
		/// Checks if the target data ends with the specified value
		/// </summary>
		/// <param name="check">The base check</param>
		/// <param name="value">The string to compare</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<string> EndsWith(this ICheckable<string> check, string value)
		{
			Contract.Requires(check != null);
			Contract.Requires(value != null);

			return check.IsTrue(x => x != null && x.EndsWith(value));
		}

		/// <summary>
		/// Checks if the target data ends with the specified value using the specified comparison type
		/// </summary>
		/// <param name="check">The base check</param>
		/// <param name="value">The string to compare</param>
		/// <param name="comparisonType">The type of comparison to perform</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<string> EndsWith(this ICheckable<string> check, string value, StringComparison comparisonType)
		{
			Contract.Requires(check != null);
			Contract.Requires(value != null);

			return check.IsTrue(x => x != null && x.EndsWith(value, comparisonType));
		}

		/// <summary>
		/// Checks if the target data ends with the specified value using the specified case rule and culture
		/// </summary>
		/// <param name="check">The base check</param>
		/// <param name="value">The string to compare</param>
		/// <param name="ignoreCase">Whether to ignore case during the comparison</param>
		/// <param name="culture">The rules which govern the comparison</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<string> EndsWith(this ICheckable<string> check, string value, bool ignoreCase, CultureInfo culture)
		{
			Contract.Requires(check != null);
			Contract.Requires(value != null);
			Contract.Requires(culture != null);

			return check.IsTrue(x => x != null && x.EndsWith(value, ignoreCase, culture));
		}

		/// <summary>
		/// Checks if the target data contains the specified value
		/// </summary>
		/// <param name="check">The base check</param>
		/// <param name="value">The string to seek</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<string> Contains(this ICheckable<string> check, string value)
		{
			Contract.Requires(check != null);
			Contract.Requires(value != null);

			return check.IsTrue(x => x != null && x.Contains(value));
		}
		#endregion

		#region String Patterns
		/// <summary>
		/// Checks if the target data matches the specified regular expression
		/// </summary>
		/// <param name="check">The base check</param>
		/// <param name="regex">The regular expression to apply</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<string> Matches(this ICheckable<string> check, Regex regex)
		{
			Contract.Requires(check != null);
			Contract.Requires(regex != null);

			return check.IsTrue(x => x != null && regex.IsMatch(x));
		}

		/// <summary>
		/// Checks if the target data matches the specified regular expression pattern
		/// </summary>
		/// <param name="check">The base check</param>
		/// <param name="pattern">The regular expression pattern to apply</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<string> Matches(this ICheckable<string> check, string pattern)
		{
			Contract.Requires(check != null);
			Contract.Requires(pattern != null);

			return check.IsTrue(x => x != null && Regex.IsMatch(x, pattern));
		}

		/// <summary>
		/// Checks if the target data matches the specified regular expression pattern using the specified options
		/// </summary>
		/// <param name="check">The base check</param>
		/// <param name="pattern">The regular expression pattern to apply</param>
		/// <param name="options">The options to use when matching</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<string> Matches(this ICheckable<string> check, string pattern, RegexOptions options)
		{
			Contract.Requires(check != null);
			Contract.Requires(pattern != null);

			return check.IsTrue(x => x != null && Regex.IsMatch(x, pattern, options));
		}

		/// <summary>
		/// Checks if the target data is a credit card number
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<string> IsCreditCardNumber(this ICheckable<string> check)
		{
			Contract.Requires(check != null);

			return check.IsTrue(target =>
			{
				// Adapted from http://mikehadlow.blogspot.com/2008/05/credit-card-validation-with-linq.html

				var valid = target != null && target.Length >= 13 && target.Length <= 18;

				if(valid)
				{
					var digits =
						(from character in target.Reverse()
						 where Char.IsDigit(character)
						 select Int32.Parse(character.ToString()))
						 .ToList();

					if(digits.Count != target.Length)
					{
						// Invalid character
						valid = false;
					}
					else
					{
						var doubleEvenSum = digits
							.AtEvenPositions()
							.SelectMany(digit => new[] { (digit * 2) % 10, (digit * 2) / 10 })
							.Sum();

						valid = (digits.AtOddPositions().Sum() + doubleEvenSum) % 10 == 0;
					}
				}

				return valid;
			});
		}

		/// <summary>
		/// Checks if the target data is a credit card CVV
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<string> IsCreditCardCvv(this ICheckable<string> check)
		{
			Contract.Requires(check != null);

			return check.Matches(Resources.CreditCardCvvPattern);
		}

		/// <summary>
		/// Checks if the target data is a phone number
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<string> IsPhoneNumber(this ICheckable<string> check)
		{
			Contract.Requires(check != null);

			return check.Matches(Resources.PhoneNumberPattern);
		}

		/// <summary>
		/// Checks if the target data is a social security number
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<string> IsSocialSecurityNumber(this ICheckable<string> check)
		{
			Contract.Requires(check != null);

			return check.Matches(Resources.SocialSecurityNumberPattern);
		}

		/// <summary>
		/// Checks if the target data is a ZIP code
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<string> IsZipCode(this ICheckable<string> check)
		{
			Contract.Requires(check != null);

			return check.Matches(Resources.ZipCodePattern);
		}
		#endregion

		/// <summary>
		/// Checks if the target data is assignable to the specified type
		/// </summary>
		/// <param name="check">The base check</param>
		/// <param name="type">The type to check for assignability</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<T> IsAssignableTo<T>(this ICheckable<T> check, Type type)
		{
			Contract.Requires(check != null);
			Contract.Requires(type != null);

			return check.IsTrue(x => type == typeof(T) || IsAssignableTo(x, type));
		}

		private static bool IsAssignableTo<T>(T target, Type type)
		{
			return (typeof(T).IsValueType || target != null) && type.IsAssignableFrom(target.GetType());
		}
	}
}