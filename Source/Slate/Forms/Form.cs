using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Grasp.Messaging;
using Grasp.Work;

namespace Slate.Forms
{
	public class Form : Aggregate
	{
		public static readonly Field<string> NameField = Field.On<Form>.For(x => x.Name);
		public static readonly Field<FormVisibility> VisibilityField = Field.On<Form>.For(x => x.Visibility);

		public Form(Guid id, string name)
		{
			Contract.Requires(name != null);

			Announce(new FormCreatedEvent(id, name));
		}

		public string Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }
		public FormVisibility Visibility { get { return GetValue(VisibilityField); } private set { SetValue(VisibilityField, value); } }

		public void AllowPreview()
		{
			if(Visibility == FormVisibility.Preview)
			{
				throw new InvalidOperationException(Resources.FormAlreadyAllowsPreview);
			}

			Announce(new FormAllowedPreviewEvent(Id));
		}

		public void DisallowPreview()
		{
			if(Visibility != FormVisibility.Preview)
			{
				throw new InvalidOperationException(Resources.FormDoesNotAllowPreview);
			}

			Announce(new FormDisallowedPreviewEvent(Id));
		}

		public void Publish()
		{
			if(Visibility == FormVisibility.Published)
			{
				throw new InvalidOperationException(Resources.FormIsAlreadyPublished);
			}

			Announce(new FormPublishedEvent(Id));
		}

		public void Unpublish()
		{
			if(Visibility != FormVisibility.Published)
			{
				throw new InvalidOperationException(Resources.FormIsNotPublished);
			}

			Announce(new FormUnpublishedEvent(Id));
		}

		private void Handle(FormCreatedEvent e)
		{
			PersistentId.ValueField.Set(this, e.FormId);

			Name = e.Name;
			Visibility = FormVisibility.Draft;

			SetWhenCreated(e.When);
			SetWhenModified(e.When);
		}

		private void Handle(FormAllowedPreviewEvent e)
		{
			Visibility = FormVisibility.Preview;

			SetWhenModified(e.When);
		}

		private void Handle(FormDisallowedPreviewEvent e)
		{
			Visibility = FormVisibility.Published;

			SetWhenModified(e.When);
		}

		private void Handle(FormPublishedEvent e)
		{
			Visibility = FormVisibility.Published;

			SetWhenModified(e.When);
		}

		private void Handle(FormUnpublishedEvent e)
		{
			Visibility = FormVisibility.Draft;

			SetWhenModified(e.When);
		}
	}
}