using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;

namespace Grasp.Work
{
	/// <summary>
	/// Describes a worker which responds to the specified command type
	/// </summary>
	/// <typeparam name="TCommand">The type of command in response to which the actor does work</typeparam>
	[ContractClass(typeof(IActorContract<>))]
	public interface IActor<TCommand> where TCommand : Command
	{
		/// <summary>
		/// Performs work in response to the specified command
		/// </summary>
		/// <param name="command">The command in response to which the actor performs work</param>
		void PerformWork(TCommand command);
	}

	[ContractClassFor(typeof(IActor<>))]
	internal abstract class IActorContract<TCommand> : IActor<TCommand> where TCommand : Command
	{
		void IActor<TCommand>.PerformWork(TCommand command)
		{
			Contract.Requires(command != null);
		}
	}
}