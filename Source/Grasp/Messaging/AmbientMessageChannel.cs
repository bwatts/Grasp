using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;

namespace Grasp.Messaging
{
	/// <summary>
	/// The default effective message channel in the current execution context
	/// </summary>
	public static class AmbientMessageChannel
	{
		private static Func<IMessageChannel> _resolveChannel;

		/// <summary>
		/// Gets whether a resolution function has been configured in the current execution context
		/// </summary>
		public static bool IsConfigured
		{
			get { return _resolveChannel != null; }
		}

		/// <summary>
		/// Specifies the function which resolves the default effective message channel in the current execution context
		/// </summary>
		/// <param name="resolveContext">The function which resolves the default effective message channel in the current execution context</param>
		public static void Configure(Func<IMessageChannel> resolveChannel)
		{
			Contract.Requires(resolveChannel != null);

			_resolveChannel = resolveChannel;
		}

		/// <summary>
		/// Resolves the default effective message channel in the current execution context
		/// </summary>
		/// <returns>The default effective message channel in the current execution context</returns>
		public static IMessageChannel Resolve()
		{
			var channel = _resolveChannel == null ? null : _resolveChannel();

			if(channel == null)
			{
				throw new InvalidOperationException(Resources.AmbientMessageChannelNotConfigured);
			}

			return channel;
		}
	}
}