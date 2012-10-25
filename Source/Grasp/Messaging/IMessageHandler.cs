using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Messaging
{
	[ContractClass(typeof(IHandlerContract))]
	public interface IMessageHandler
	{
		Task HandleAsync(Message message);
	}

	[ContractClass(typeof(IHandlerContract<>))]
	public interface IMessageHandler<T> : IMessageHandler where T : Message
	{
		Task HandleAsync(T message);
	}

	[ContractClassFor(typeof(IMessageHandler))]
	internal abstract class IHandlerContract : IMessageHandler
	{
		Task IMessageHandler.HandleAsync(Message message)
		{
			Contract.Requires(message != null);
			Contract.Ensures(Contract.Result<Task>() != null);

			return null;
		}
	}

	[ContractClassFor(typeof(IMessageHandler<>))]
	internal abstract class IHandlerContract<T> : IMessageHandler<T> where T : Message
	{
		Task IMessageHandler<T>.HandleAsync(T message)
		{
			Contract.Requires(message != null);
			Contract.Ensures(Contract.Result<Task>() != null);

			return null;
		}

		Task IMessageHandler.HandleAsync(Message message)
		{
			return null;
		}
	}
}