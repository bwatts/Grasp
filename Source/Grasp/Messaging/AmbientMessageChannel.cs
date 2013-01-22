using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;

namespace Grasp.Messaging
{
	public static class AmbientMessageChannel
	{
		private static Func<IMessageChannel> _resolveChannel;

		public static bool IsConfigured
		{
			get { return _resolveChannel != null; }
		}

		public static void Configure(Func<IMessageChannel> resolveChannel)
		{
			Contract.Requires(resolveChannel != null);

			_resolveChannel = resolveChannel;
		}

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