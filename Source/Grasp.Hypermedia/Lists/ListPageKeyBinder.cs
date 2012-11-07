using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using Cloak;
using Grasp.Lists;

namespace Grasp.Hypermedia.Lists
{
	public sealed class ListPageKeyBinder : IModelBinder
	{
		public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
		{
			bindingContext.Model = new ListPageKey(GetPage(bindingContext), GetPageSize(bindingContext), GetSort(bindingContext));

			return true;
		}

		private Number GetPage(ModelBindingContext bindingContext)
		{
			var page = bindingContext.ValueProvider.GetValue("page");

			return page == null ? Number.None : new Number(Int32.Parse(page.AttemptedValue));
		}

		private Count GetPageSize(ModelBindingContext bindingContext)
		{
			var pageSize = bindingContext.ValueProvider.GetValue("pageSize");

			return pageSize == null ? Count.None : new Count(Int32.Parse(pageSize.AttemptedValue));
		}

		private Sort GetSort(ModelBindingContext bindingContext)
		{
			var sort = bindingContext.ValueProvider.GetValue("sort");

			return sort == null ? Sort.Empty : Sort.Parse(sort.AttemptedValue);
		}
	}
}