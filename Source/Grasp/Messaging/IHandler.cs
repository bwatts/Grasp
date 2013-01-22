using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Messaging
{
	/// <summary>
	/// Describes a handler for commands of the specified type
	/// </summary>
	/// <typeparam name="TCommand">The type of handled command</typeparam>
	[ContractClass(typeof(IHandlerContract<>))]
	public interface IHandler<TCommand> where TCommand : Command
	{
		/// <summary>
		/// Handles the the specified command
		/// </summary>
		/// <param name="command">The command to handle</param>
		/// <returns>The work of handling the command</returns>
		Task HandleAsync(TCommand command);
	}

	[ContractClassFor(typeof(IHandler<>))]
	internal abstract class IHandlerContract<TCommand> : IHandler<TCommand> where TCommand : Command
	{
		Task IHandler<TCommand>.HandleAsync(TCommand command)
		{
			Contract.Requires(command != null);
			Contract.Ensures(Contract.Result<Task>() != null);

			return null;
		}
	}
}