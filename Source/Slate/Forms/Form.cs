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
	public class Form : Aggregate
	{
		public static readonly Field<string> NameField = Field.On<Form>.For(x => x.Name);
		public static readonly Field<FormPhase> PhaseField = Field.On<Form>.For(x => x.Phase);

		public Form(Guid workItemId, Guid id, string name)
		{
			Announce(new FormStartedEvent(workItemId, id, name));
		}

		public string Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }
		public FormPhase Phase { get { return GetValue(PhaseField); } private set { SetValue(PhaseField, value); } }

		public void Handle(StartTestingCommand command)
		{
			Contract.Requires(command != null);
			Contract.Requires(command.FormId == Id);

			if(Phase == FormPhase.Testing)
			{
				throw new InvalidOperationException(Resources.FormAlreadyTesting.FormatInvariant(Name));
			}

			if(Phase == FormPhase.Live)
			{
				throw new InvalidOperationException(Resources.CannotTestLiveForm.FormatInvariant(Name));
			}

			Announce(new TestingStartedEvent(Id));
		}

		public void Handle(ResumeDraftCommand command)
		{
			Contract.Requires(command != null);
			Contract.Requires(command.FormId == Id);

			if(Phase == FormPhase.Live)
			{
				throw new InvalidOperationException(Resources.CannotResumeDraftOnLiveForm.FormatInvariant(Name));
			}

			Announce(new DraftResumedEvent(Id));
		}

		public void Handle(GoLiveCommand command)
		{
			Contract.Requires(command != null);
			Contract.Requires(command.FormId == Id);

			if(Phase == FormPhase.Live)
			{
				throw new InvalidOperationException(Resources.FormAlreadyLive.FormatInvariant(Name));
			}

			Announce(new WentLiveEvent(Id));
		}

		private void Observe(FormStartedEvent e)
		{
			SetId(e.FormId);

			Name = e.Name;

			Phase = FormPhase.Draft;

			SetWhenCreated(e.When);
			SetWhenModified(e.When);
		}

		private void Observe(TestingStartedEvent e)
		{
			Phase = FormPhase.Testing;

			SetWhenModified(e.When);
		}

		private void Observe(DraftResumedEvent e)
		{
			Phase = FormPhase.Draft;

			SetWhenModified(e.When);
		}

		private void Observe(WentLiveEvent e)
		{
			Phase = FormPhase.Live;

			SetWhenModified(e.When);
		}
	}
}