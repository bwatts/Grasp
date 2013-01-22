using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Knowledge.Base;
using Grasp.Messaging;

namespace Grasp.Work
{
	/// <summary>
	/// Describes a versioned consistency boundary in which events occur on the same timeline
	/// </summary>
	[ContractClass(typeof(IAggregateContract))]
	public interface IAggregate
	{
		/// <summary>
		/// Gets the unique hierarchical name of this aggregate
		/// </summary>
		FullName Name { get; }

		/// <summary>
		/// Handles the specified command
		/// </summary>
		/// <param name="command">The command to handle</param>
		void HandleCommand(Command command);

		/// <summary>
		/// Observes the specified event
		/// </summary>
		/// <param name="@event">The event to observe</param>
		void ObserveEvent(Event @event);

		/// <summary>
		/// Gets the events observed since the last retrieval and clears them from this aggregate
		/// </summary>
		/// <returns>The events observed since the last retrieval</returns>
		IList<Event> RetrieveNewEvents();
	}

	[ContractClassFor(typeof(IAggregate))]
	internal abstract class IAggregateContract : IAggregate
	{
		FullName IAggregate.Name
		{
			get
			{
				Contract.Ensures(Contract.Result<FullName>() != null);

				return null;
			}
		}

		void IAggregate.HandleCommand(Command command)
		{
			Contract.Requires(command != null);
		}

		void IAggregate.ObserveEvent(Event @event)
		{
			Contract.Requires(@event != null);
		}

		IList<Event> IAggregate.RetrieveNewEvents()
		{
			Contract.Ensures(Contract.Result<IList<Event>>() != null);

			return null;
		}
	}
}