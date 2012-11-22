using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using Grasp.Checks;

namespace Grasp.Hypermedia.Server
{
	public sealed class EntityIdBinder : IModelBinder
	{
		public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
		{
			var id = bindingContext.ValueProvider.GetValue("id");

			bindingContext.Model = id == null || Check.That(id.AttemptedValue).IsNullOrEmpty()
				? EntityId.Unassigned
				: new EntityId(id.AttemptedValue);

			return true;
		}
	}
}