using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp;
using Slate.Services;

namespace Slate.Http.Api
{
	public sealed class FormMesh : Notion, IFormMesh
	{
		public Uri GetFormLocation(EntityId formId)
		{
			return new Uri("forms/" + formId.ToString(), UriKind.Relative);
		}
	}
}