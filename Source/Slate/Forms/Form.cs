using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Grasp;
using Grasp.Messaging;
using Grasp.Work;
using Grasp.Work.Persistence;

namespace Slate.Forms
{
	public class Form : Aggregate,
		IActor<StartTestingCommand>,
		IActor<ResumeDraftCommand>,
		IActor<GoLiveCommand>
	{
		public static readonly Field<string> NameField = Field.On<Form>.For(x => x.Name);
		public static readonly Field<FormPhase> PhaseField = Field.On<Form>.For(x => x.Phase);

		public Form(EntityId workItemId, EntityId id, string name)
		{
			Announce(new FormStartedEvent(workItemId, id, name));
		}

		public string Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }
		public FormPhase Phase { get { return GetValue(PhaseField); } private set { SetValue(PhaseField, value); } }

		public void Act(StartTestingCommand command)
		{
			// TODO: CommandFailedEvent

			//Contract.Requires(command.FormId == Id);

			// TODO: InvalidTransitionEvent

			//if(Phase == FormPhase.Testing)
			//{
			//	throw new InvalidOperationException(Resources.FormAlreadyTesting.FormatInvariant(Name));
			//}

			// TODO: InvalidTransitionEvent

			//if(Phase == FormPhase.Live)
			//{
			//	throw new InvalidOperationException(Resources.CannotTestLiveForm.FormatInvariant(Name));
			//}

			Announce(new TestingStartedEvent(Id));
		}

		public void Act(ResumeDraftCommand command)
		{
			// TODO: CommandFailedEvent

			//Contract.Requires(command.FormId == Id);

			// TODO: InvalidTransitionEvent

			//if(Phase == FormPhase.Draft)
			//{
			//	throw new InvalidOperationException(Resources.FormAlreadyDraft.FormatInvariant(Name));
			//}

			// TODO: InvalidTransitionEvent

			//if(Phase == FormPhase.Live)
			//{
			//	throw new InvalidOperationException(Resources.CannotResumeDraftOnLiveForm.FormatInvariant(Name));
			//}

			Announce(new DraftResumedEvent(Id));
		}

		public void Act(GoLiveCommand command)
		{
			// TODO: CommandFailedEvent

			//Contract.Requires(command.FormId == Id);

			// TODO: InvalidTransitionEvent

			//if(Phase == FormPhase.Live)
			//{
			//	throw new InvalidOperationException(Resources.FormAlreadyLive.FormatInvariant(Name));
			//}

			Announce(new WentLiveEvent(Id));
		}

		private void Observe(FormStartedEvent e)
		{
			OnCreated(e.FormId, e.When);

			Name = e.Name;
			Phase = FormPhase.Draft;
		}

		private void Observe(TestingStartedEvent e)
		{
			OnModified(e.When);

			Phase = FormPhase.Testing;
		}

		private void Observe(DraftResumedEvent e)
		{
			OnModified(e.When);

			Phase = FormPhase.Draft;
		}

		private void Observe(WentLiveEvent e)
		{
			OnModified(e.When);

			Phase = FormPhase.Live;
		}
	}
}