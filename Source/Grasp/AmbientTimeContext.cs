using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak.Time;

namespace Grasp
{
	/// <summary>
	/// The default effective time context in the current execution context
	/// </summary>
	public static class AmbientTimeContext
	{
		private static readonly ITimeContext _platformTimeContext = new PlatformTimeContext();

		private static Func<ITimeContext> _resolveContext;

		/// <summary>
		/// Gets whether a resolution function has been configured in the current execution context
		/// </summary>
		public static bool IsConfigured
		{
			get { return _resolveContext != null; }
		}

		/// <summary>
		/// Specifies the function which resolves the default effective time context in the current execution context
		/// </summary>
		/// <param name="resolveContext">The function which resolves the default effective time context in the current execution context</param>
		public static void Configure(Func<ITimeContext> resolveContext)
		{
			Contract.Requires(resolveContext != null);

			_resolveContext = resolveContext;
		}

		/// <summary>
		/// Resolves the default effective time context in the current execution context
		/// </summary>
		/// <returns>The default effective time context in the current execution context</returns>
		public static ITimeContext Resolve()
		{
			var context = _resolveContext == null ? null : _resolveContext();

			return context ?? _platformTimeContext;
		}
	}
}