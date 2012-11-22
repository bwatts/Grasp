using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Grasp;
using Grasp.Checks;

namespace Slate.Web.Site.Presentation
{
	public sealed class EntityIdBinder : IModelBinder
	{
		public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			var id = bindingContext.ValueProvider.GetValue("id");

			return id == null || Check.That(id.AttemptedValue).IsNullOrEmpty()
				? EntityId.Unassigned
				: new EntityId(id.AttemptedValue);
		}
	}
}