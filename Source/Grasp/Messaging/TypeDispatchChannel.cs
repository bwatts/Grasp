using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Messaging
{
	public sealed class TypeDispatchChannel : Notion, IMessageChannel
	{
		public static readonly Field<IReadOnlyDictionary<Type, IMessageChannel>> _channelsByTypeField = Field.On<TypeDispatchChannel>.For(x => x._channelsByType);

		private IReadOnlyDictionary<Type, IMessageChannel> _channelsByType { get { return GetValue(_channelsByTypeField); } set { SetValue(_channelsByTypeField, value); } }

		public TypeDispatchChannel(IReadOnlyDictionary<Type, IMessageChannel> channelsByType)
		{
			Contract.Requires(channelsByType != null);

			_channelsByType = channelsByType;
		}

		public async Task PublishAsync(Message message)
		{
			IMessageChannel typeChannel;

			if(_channelsByType.TryGetValue(message.GetType(), out typeChannel))
			{
				await typeChannel.PublishAsync(message);
			}
		}
	}
}