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
	public sealed class ListViewKeyBinder : IModelBinder
	{
		public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
		{
			var key = new ListViewKey(GetStart(bindingContext), GetSize(bindingContext), GetSort(bindingContext));

			if(key == ListViewKey.Empty)
			{
				key = ListViewKey.Default;
			}

			bindingContext.Model = key;

			return true;
		}

		private Count GetStart(ModelBindingContext bindingContext)
		{
			return GetValue(bindingContext, "start", Count.None, value => new Count(Int32.Parse(value)));
		}

		private Count GetSize(ModelBindingContext bindingContext)
		{
			return GetValue(bindingContext, "size", Count.None, value => new Count(Int32.Parse(value)));
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