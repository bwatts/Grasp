using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Grasp.Hypermedia;
using Slate.Services;

namespace Slate.Http.Api
{
	public sealed class FormMesh : Notion, IFormMesh
	{
		public static readonly Field<IHttpResourceContext> _resourceContextField = Field.On<FormMesh>.For(x => x._resourceContext);

		private IHttpResourceContext _resourceContext { get { return GetValue(_resourceContextField); } set { SetValue(_resourceContextField, value); } }

		public FormMesh(IHttpResourceContext resourceContext)
		{
			Contract.Requires(resourceContext != null);

			_resourceContext = resourceContext;
		}

		public Uri GetFormLocation(Guid formId)
		{
			return _resourceContext.GetAbsoluteUrl("forms/" + formId.ToString("N").ToUpper());
		}
	}
}