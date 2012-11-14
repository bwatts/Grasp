using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using Cloak;
using Grasp.Checks;
using Grasp.Lists;

namespace Grasp.Hypermedia.Server
{
	public sealed class ListPageKeyBinder : IModelBinder
	{
		public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
		{
			var pageKey = new ListPageKey(GetPage(bindingContext), GetPageSize(bindingContext), GetSort(bindingContext));

			if(pageKey == ListPageKey.Empty)
			{
				pageKey = ListPageKey.Default;
			}

			bindingContext.Model = pageKey;

			return true;
		}

		private Number GetPage(ModelBindingContext bindingContext)
		{
			return GetValue(bindingContext, "page", Number.None, value => new Number(Int32.Parse(value)));
		}

		private Count GetPageSize(ModelBindingContext bindingContext)
		{
			return GetValue(bindingContext, "pageSize", Count.None, value => new Count(Int32.Parse(value)));
		}

		private Sort GetSort(ModelBindingContext bindingContext)
		{
			return GetValue(bindingContext, "sort", Sort.Empty, value => Sort.Parse(value));
		}

		private static T GetValue<T>(ModelBindingContext bindingContext, string key, T defaultValue, Func<string, T> parse)
		{
			var value = bindingContext.ValueProvider.GetValue(key);

			return value == null || Check.That(value.AttemptedValue).IsNullOrEmpty()
				? defaultValue
				: parse(value.AttemptedValue);
		}
	}
}