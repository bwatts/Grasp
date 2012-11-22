using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Slate.Web.Site.Presentation.Lists;

namespace Slate.Web.Site.Views
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class ModelRendering
	{
		public static void RenderDescriptionList(this HtmlHelper html, ListModel list, Func<ItemModel, string> descriptionSelector)
		{
			Contract.Requires(html != null);
			Contract.Requires(list != null);
			Contract.Requires(descriptionSelector != null);

			html.RenderPartial("_DescriptionList", new ListWithItemDescriptionsModel(list, descriptionSelector));
		}
	}
}