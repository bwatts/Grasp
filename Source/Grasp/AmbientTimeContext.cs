using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak.Time;

namespace Grasp
{
	public static class AmbientTimeContext
	{
		private static readonly ITimeContext _platformTimeContext = new PlatformTimeContext();

		private static Func<ITimeContext> _resolveContext;

		public static bool IsConfigured
		{
			get { return _resolveContext != null; }
		}

		public static void Configure(Func<ITimeContext> resolveContext)
		{
			Contract.Requires(resolveContext != null);

			_resolveContext = resolveContext;
		}

		public static ITimeContext Resolve()
		{
			var context = _resolveContext == null ? null : _resolveContext();

			return context ?? _platformTimeContext;
		}
	}
}