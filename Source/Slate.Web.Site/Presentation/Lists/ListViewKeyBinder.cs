using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Cloak;
using Grasp.Checks;
using Grasp.Lists;

namespace Slate.Web.Site.Presentation.Lists
{
	public sealed class ListViewKeyBinder : IModelBinder
	{
		public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			return new ListViewKey(GetStart(bindingContext), GetSize(bindingContext), GetSort(bindingContext));
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