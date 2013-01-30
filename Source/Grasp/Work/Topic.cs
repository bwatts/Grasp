using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Cloak.Reflection;
using Grasp.Checks;
using Grasp.Messaging;

namespace Grasp.Work
{
	/// <summary>
	/// A persistent item on a timeline of work
	/// </summary>
	public abstract class Topic : NamedNotion, IAggregate
	{
		public static readonly Field<ManyInOrder<Event>> _newEventsField = Field.On<Topic>.For(x => x._newEvents);

		private ManyInOrder<Event> _newEvents { get { return GetValue(_newEventsField); } set { SetValue(_newEventsField, value); } }

		/// <summary>
		/// Initializes an anonymous topic with an empty timeline
		/// </summary>
		protected Topic()
		{
			_newEvents = new ManyInOrder<Event>();
		}

		void IAggregate.HandleCommand(Command command)
		{
			HandleCommand(command);
		}

		void IAggregate.ObserveEvent(Event @event)
		{
			ObserveEvent(@event);
		}

		IList<Event> IAggregate.RetrieveNewEvents()
		{
			return RetrieveNewEvents();
		}

		/// <summary>
		/// Handles the specified command by dispatching it to an implicit Handle method (if any)
		/// </summary>
		/// <param name="command">The command to dipatch to an implicit Handle method (if any)</param>
		protected void HandleCommand(Command command)
		{
			Contract.Requires(command != null);

			DispatchToImplicitHandler(command);
		}

		/// <summary>
		/// Handles the specified command by dispatching it to an implicit Observe method (if any)
		/// </summary>
		/// <param name="@event">The event to observe</param>
		protected void ObserveEvent(Event @event)
		{
			Contract.Requires(@event != null);

			// TODO: The "isNew" flag needs to be re-thought if this method will also handle new events off the bus

			ApplyEvent(@event, isNew: false);
		}

		/// <summary>
		/// Gets the events observed since the last retrieval and clears them from this topic
		/// </summary>
		/// <returns>The events observed since the last retrieval</returns>
		protected IList<Event> RetrieveNewEvents()
		{
			return _newEvents;
		}

		/// <summary>
		/// Announces that the specified event occurred in relation to this topic
		/// </summary>
		/// <param name="@event">The event which occurred</param>
		protected void Announce(Event @event)
		{
			Contract.Requires(@event != null);

			ApplyEvent(@event, isNew: true);
		}

		// This is some neat trickery from the Simple CQRS tutorial:
		//
		// https://github.com/gregoryyoung/m-r/blob/master/SimpleCQRS/Domain.cs
		//
		// It performs overload resolution, at runtime, on the methods of this instance. As this includes private methods,
		// we can define a simple convention that aggregates respond to specific event types via private methods named "Handle",
		// differentiated by event type.
		//
		// It may require some abstraction in the future, but works just fine for now.
		//
		// The original inspiration can be found at https://github.com/davidebbo/ReflectionMagic and is reflected (pun intended) in the DynamicReflector class.

		private void ApplyEvent(Event e, bool isNew)
		{
			DispatchToImplicitObserver(e);

			if(isNew)
			{
				_newEvents.AsWriteable().Add(e);
			}
		}

		private void DispatchToImplicitHandler(Command c)
		{
			DynamicReflector.For(this).Handle(c);
		}

		private void DispatchToImplicitObserver(Event e)
		{
			DynamicReflector.For(this).Observe(e);
		}

		/// <summary>
		/// Sets <see cref="Grasp.FullName.NameField"/> and <see cref="Grasp.Lifetime.CreatedEventField"/>
		/// </summary>
		/// <param name="name">The unique hierarchical name of the topic</param>
		/// <param name="when">The date and time at which this topic was created</param>
		protected void OnCreated(FullName name, DateTime when)
		{
			SetValue(FullName.NameField, name);

			SetValue(Lifetime.CreatedEventField, when);
		}

		/// <summary>
		/// Sets <see cref="Grasp.Lifetime.ModifiedEventField"/>
		/// </summary>
		/// <param name="when">The date and time at which this topic was modified</param>
		protected void OnModified(DateTime when)
		{
			SetValue(Lifetime.ModifiedEventField, when);
		}

		/// <summary>
		/// Announces that this topic failed to handle the specified command
		/// </summary>
		/// <param name="command">The command which this topic failed to handle</param>
		/// <param name="issue">The issue which caused the failure</param>
		protected void AnnounceCommandFailed(Command command, Issue issue)
		{
			Contract.Requires(command != null);
			Contract.Requires(issue != null);

			// Apply event directly instead of announcing to avoid having these events become part of the aggregate's history
			//
			// TODO: Determine any scenarios that may benefit from recording command failure events

			ApplyEvent(new CommandFailedEvent(command, issue), isNew: false);
		}

		/// <summary>
		/// Announces that this topic failed to handle the specified command
		/// </summary>
		/// <param name="command">The command which this topic failed to handle</param>
		/// <param name="issueIdentifier">The identifier of the issue which caused the failure</param>
		/// <param name="issueDescription">A detailed description of the issue</param>
		protected void AnnounceCommandFailed(Command command, Identifier issueIdentifier, string issueDescription)
		{
			AnnounceCommandFailed(command, new Issue(GetIssueName(issueIdentifier), issueDescription));
		}

		/// <summary>
		/// Announces that this topic failed to handle the specified command
		/// </summary>
		/// <param name="command">The command which this topic failed to handle</param>
		/// <param name="issueIdentifier">The identifier of the issue which caused the failure</param>
		/// <param name="issueDescription">A detailed description of the issue</param>
		protected void AnnounceCommandFailed(Command command, string issueIdentifier, string issueDescription)
		{
			AnnounceCommandFailed(command, new Identifier(issueIdentifier), issueDescription);
		}

		/// <summary>
		/// Qualifies the specified cause with the full name of this topic class
		/// </summary>
		/// <param name="causeIdentifier">The identifier of the root cause of the failure</param>
		/// <returns>The specified cause qualified with the full name of this topic class</returns>
		protected FullName GetIssueName(Identifier causeIdentifier)
		{
			return new Namespace(GetType().FullName) + causeIdentifier;
		}
	}
}