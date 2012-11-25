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
using Slate.Forms.Structure;

namespace Slate.Forms
{
	public class Form : Aggregate,
		IActor<StartTestingCommand>,
		IActor<ResumeDraftCommand>,
		IActor<GoLiveCommand>,
		IActor<AddSectionCommand>
	{
		public static readonly Field<string> NameField = Field.On<Form>.For(x => x.Name);
		public static readonly Field<FormPhase> PhaseField = Field.On<Form>.For(x => x.Phase);
		public static readonly Field<Section> RootSectionField = Field.On<Form>.For(x => x.RootSection);

		public Form(EntityId workItemId, EntityId id, string name)
		{
			Announce(new FormStartedEvent(workItemId, id, name));
		}

		public string Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }
		public FormPhase Phase { get { return GetValue(PhaseField); } private set { SetValue(PhaseField, value); } }
		public Section RootSection { get { return GetValue(RootSectionField); } private set { SetValue(RootSectionField, value); } }

		public void PerformWork(StartTestingCommand command)
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

		public void PerformWork(ResumeDraftCommand command)
		{
			Contract.Requires(command != null);
			Contract.Requires(command.FormId == Id);

			if(Phase == FormPhase.Draft)
			{
				throw new InvalidOperationException(Resources.FormAlreadyDraft.FormatInvariant(Name));
			}

			if(Phase == FormPhase.Live)
			{
				throw new InvalidOperationException(Resources.CannotResumeDraftOnLiveForm.FormatInvariant(Name));
			}

			Announce(new DraftResumedEvent(Id));
		}

		public void PerformWork(GoLiveCommand command)
		{
			Contract.Requires(command != null);
			Contract.Requires(command.FormId == Id);

			if(Phase == FormPhase.Live)
			{
				throw new InvalidOperationException(Resources.FormAlreadyLive.FormatInvariant(Name));
			}

			Announce(new WentLiveEvent(Id));
		}

		public void PerformWork(AddSectionCommand command)
		{
			Contract.Requires(command != null);
			Contract.Requires(command.FormId == Id);

			if(Phase == FormPhase.Live)
			{
				throw new InvalidOperationException(Resources.CannotModifyLiveForm.FormatInvariant(Name));
			}

			if(!RootSection.ContainsSection(command.ParentSectionId))
			{
				throw new ArgumentException(Resources.UnknownSection.FormatInvariant(command.ParentSectionId, Name), "command");
			}

			Announce(new SectionAddedEvent(Id, command.ParentSectionId, EntityId.Generate(), command.Title));
		}

		private void Observe(FormStartedEvent e)
		{
			OnCreated(e.FormId, e.When);

			Name = e.Name;
			Phase = FormPhase.Draft;
			RootSection = new Section("");
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

		private void Observe(SectionAddedEvent e)
		{
			OnModified(e.When);

			var section = new Section(e.Title);

			PersistentId.ValueField.Set(section, e.SectionId);

			RootSection.FindSection(e.ParentSectionId).AddSection(section);
		}
	}
}