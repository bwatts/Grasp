using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak.Reflection;
using Grasp.Messaging;
using Grasp.Work.Persistence;

namespace Grasp.Work
{
	/// <summary>
	/// A versioned consistency boundary in which events occur on the same timeline
	/// </summary>
	public abstract class Aggregate : Entity
	{
		public static readonly Field<EntityId> RevisionIdField = Field.On<Aggregate>.For(x => x.RevisionId);
		public static readonly Field<ManyInOrder<Event>> _unobservedEventsField = Field.On<Aggregate>.For(x => x._unobservedEvents);

		private ManyInOrder<Event> _unobservedEvents { get { return GetValue(_unobservedEventsField); } set { SetValue(_unobservedEventsField, value); } }

		protected Aggregate()
		{
			_unobservedEvents = new ManyInOrder<Event>();
		}

		public EntityId RevisionId { get { return GetValue(RevisionIdField); } private set { SetValue(RevisionIdField, value); } }

		public void HandleCommand(Command command)
		{
			Contract.Requires(command != null);

			DispatchToImplicitHandler(command);
		}

		public IEnumerable<Event> ObserveEvents()
		{
			var events = _unobservedEvents.ToList();

			ClearUnobservedEvents();

			return events;
		}

		public void SetVersion(AggregateVersion version)
		{
			Contract.Requires(version != null);

			ClearUnobservedEvents();

			foreach(var @event in version)
			{
				ApplyEvent(@event, isNew: false);
			}

			RevisionId = version.RevisionId;
		}

		protected void OnCreated(EntityId id, DateTime when)
		{
			SetValue(PersistentId.ValueField, id);

			SetValue(Lifetime.WhenCreatedField, when);

			OnModified(when);
		}

		protected void OnModified(DateTime when)
		{
			// We have the option of detecting field changes and setting this value automagically.
			//
			// However, it seems safer and more consistent to treat events as authoritative.
			//
			// It would be nice, though, to not require a call to this method within Observe methods.

			SetValue(Lifetime.WhenModifiedField, when);
		}

		protected void AnnounceCommandFailed(Command command, Identifier causeIdentifier, string causeDescription)
		{
			Contract.Requires(causeIdentifier != null);

			// Apply event directly instead of announcing to avoid having these events become part of the aggregate's history
			//
			// TODO: Determine any scenarios that may benefit from recording command failure events

			ApplyEvent(new CommandFailedEvent(command, GetCauseName(causeIdentifier), causeDescription), isNew: false);
		}

		protected void AnnounceCommandFailed(Command command, string causeIdentifier, string causeDescription)
		{
			AnnounceCommandFailed(command, new Identifier(causeIdentifier), causeDescription);
		}

		protected FullName GetCauseName(Identifier rootCauseIdentifier)
		{
			return new Namespace(GetType().FullName) + rootCauseIdentifier;
		}

		protected void Announce(Event @event)
		{
			ApplyEvent(@event, isNew: true);
		}

		private void ApplyEvent(Event @event, bool isNew)
		{
			DispatchToImplicitObserver(@event);

			if(isNew)
			{
				RecordUnobservedEvent(@event);
			}
		}

		private void DispatchToImplicitObserver(Event @event)
		{
			// This is some neat trickery from a CQRS tutorial repository:
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

			DynamicReflector.For(this).Observe(@event);
		}

		private void DispatchToImplicitHandler(Command command)
		{
			DynamicReflector.For(this).Handle(command);
		}

		private void RecordUnobservedEvent(Event @event)
		{
			_unobservedEvents.AsWriteable().Add(@event);
		}

		private void ClearUnobservedEvents()
		{
			_unobservedEvents.AsWriteable().Clear();
		}
	}
}