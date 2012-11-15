using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Raven;
using Raven.Client;
using Slate.Forms;
using Slate.Http.Api;

namespace Slate.Http.Persistence
{
	public sealed class FormByIdQuery : RavenContext, IFormByIdQuery
	{
		public FormByIdQuery(IDocumentStore documentStore) : base(documentStore)
		{}

		public Task<Form> GetFormAsync(Guid id)
		{
			return ExecuteReadAsync(session => session.Load<Form>("Forms/" + id.ToString("N").ToUpper()));
		}
	}
}