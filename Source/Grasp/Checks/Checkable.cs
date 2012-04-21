using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
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
		/// Checks whether the target data passes the specified check
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
		/// Checks whether the target data passed an already-applied check
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
		/// Checks whether the target data fails the specified check
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
		/// Checks whether the target data failed an already-applied check
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
		/// Checks whether the target data is true
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<bool> IsTrue(this ICheckable<bool> check)
		{
			Contract.Requires(check != null);

			return check.Passes(t => t);
		}

		/// <summary>
		/// Checks whether the target data is false
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
		/// Checks whether the target data is a letter or digit
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsLetterOrDigit(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.Passes(c => Char.IsLetterOrDigit(c));
		}

		/// <summary>
		/// Checks whether the target data is a control character
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsControl(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.Passes(c => Char.IsControl(c));
		}

		/// <summary>
		/// Checks whether the target data is a digit
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsDigit(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.Passes(c => Char.IsDigit(c));
		}

		/// <summary>
		/// Checks whether the target data is a high surrogate
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsHighSurrogate(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.Passes(c => Char.IsHighSurrogate(c));
		}

		/// <summary>
		/// Checks whether the target data is a letter
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsLetter(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.Passes(c => Char.IsLetter(c));
		}

		/// <summary>
		/// Checks whether the target data is a lower-case character
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsLowerCase(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.Passes(c => Char.IsLower(c));
		}

		/// <summary>
		/// Checks whether the target data is a low surrogate
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsLowSurrogate(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.Passes(c => Char.IsLowSurrogate(c));
		}

		/// <summary>
		/// Checks whether the target data is a number
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsNumber(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.Passes(c => Char.IsNumber(c));
		}

		/// <summary>
		/// Checks whether the target data is punctuation
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsPunctuation(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.Passes(c => Char.IsPunctuation(c));
		}

		/// <summary>
		/// Checks whether the target data is a separator
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsSeparator(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.Passes(c => Char.IsSeparator(c));
		}

		/// <summary>
		/// Checks whether the target data is a surrogate
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsSurrogate(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.Passes(c => Char.IsSurrogate(c));
		}

		/// <summary>
		/// Checks whether the target data is a symbol
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsSymbol(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.Passes(c => Char.IsSymbol(c));
		}

		/// <summary>
		/// Checks whether the target data is an upper-case character
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsUpperCase(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.Passes(c => Char.IsUpper(c));
		}

		/// <summary>
		/// Checks whether the target data is whitespace
		/// </summary>
		/// <param name="check">The base check</param>
		/// <returns>A check which applies the base check and this check</returns>
		public static Check<char> IsWhiteSpace(this ICheckable<char> check)
		{
			Contract.Requires(check != null);

			return check.Passes(c => Char.IsWhiteSpace(c));
		}
		#endregion
	}
}