using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grasp.Checks
{
	/// <summary>
	/// Creates checks which apply to pieces of data
	/// </summary>
	public static class Check
	{
		/// <summary>
		/// Gets a check which applies to the specified target data and whose result defaults to the specified value
		/// </summary>
		/// <typeparam name="T">The type of data to which the check applies</typeparam>
		/// <param name="target">The data to check</param>
		/// <param name="defaultResult">The default result of invoking the check</param>
		/// <returns>A check which applies to the specified target data</returns>
		public static ICheckable<T> That<T>(T target, bool defaultResult = true)
		{
			return new ThatCheck<T>(target, defaultResult);
		}

		private sealed class ThatCheck<T> : Check<T>
		{
			private readonly bool _defaultResult;

			internal ThatCheck(T target, bool defaultResult) : base(target)
			{
				_defaultResult = defaultResult;
			}

			public override bool Apply()
			{
				return _defaultResult;
			}
		}
	}

	/// <summary>
	/// A boolean-valued function applied to a piece of data of the specified type. Implicitly converts to boolean.
	/// </summary>
	/// <typeparam name="T">The type of data to which the boolean-valued function is applied</typeparam>
	public abstract class Check<T> : ICheckable<T>
	{
		/// <summary>
		/// Applies the specified check
		/// </summary>
		/// <param name="invokedCheck">The check to apply</param>
		/// <returns>The result of applying the specified check</returns>
		public static implicit operator bool(Check<T> invokedCheck)
		{
			return invokedCheck.Apply();
		}

		/// <summary>
		/// Initializes a check with the specified target data
		/// </summary>
		/// <param name="target">The data to check</param>
		protected Check(T target)
		{
			Target = target;
		}

		/// <summary>
		/// Gets the type of target data
		/// </summary>
		public Type TargetType
		{
			get { return typeof(T); }
		}

		/// <summary>
		/// Gets the target data
		/// </summary>
		public T Target { get; private set; }

		object ICheckable.Target
		{
			get { return Target; }
		}

		/// <summary>
		/// When implemented by a derived class, applies the boolean-valued function to the target data
		/// </summary>
		/// <returns>The result of applying the boolean-valued function to the target data</returns>
		public abstract bool Apply();
	}
}