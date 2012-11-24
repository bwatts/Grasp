using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Grasp.Messaging;
using Grasp.Work;
using Grasp.Work.Items;
using Slate.Forms;

namespace Slate.Services
{
	public sealed class FormStartedSubscriber : Publisher, ISubscriber<FormStartedEvent>
	{
		public static readonly Field<IFormMesh> _meshField = Field.On<FormStartedSubscriber>.For(x => x._mesh);

		private IFormMesh _mesh { get { return GetValue(_meshField); } set { SetValue(_meshField, value); } }

		public FormStartedSubscriber(IFormMesh mesh)
		{
			Contract.Requires(mesh != null);

			_mesh = mesh;
		}

		public async Task ObserveAsync(FormStartedEvent e)
		{
			await IssueAsync(new ReportProgressCommand(e.WorkItemId, _mesh.GetFormLocation(e.FormId)));
		}
	}
}