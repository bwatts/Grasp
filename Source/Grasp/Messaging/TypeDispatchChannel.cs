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
		public static readonly Field<ManyKeyed<Type, IMessageChannel>> _channelsByTypeField = Field.On<TypeDispatchChannel>.For(x => x._channelsByType);

		private ManyKeyed<Type, IMessageChannel> _channelsByType { get { return GetValue(_channelsByTypeField); } set { SetValue(_channelsByTypeField, value); } }

		public TypeDispatchChannel(IEnumerable<KeyValuePair<Type, IMessageChannel>> channelsByType)
		{
			Contract.Requires(channelsByType != null);

			_channelsByType = channelsByType.ToManyKeyed();
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