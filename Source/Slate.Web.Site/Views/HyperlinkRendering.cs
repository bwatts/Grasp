﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Grasp.Checks;
using Grasp.Hypermedia;
using Grasp.Hypermedia.Linq;
using Slate.Web.Site.Presentation;
using Slate.Web.Site.Presentation.Lists;
using Slate.Web.Site.Presentation.Navigation;

namespace Slate.Web.Site.Views
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class HyperlinkRendering
	{
		public static IHtmlString RenderLink(this HtmlHelper html, Hyperlink link, object content = null, string title = null, Relationship relationship = null, MClass @class = null)
		{
			Contract.Requires(html != null);
			Contract.Requires(link != null);

			if(content != null)
			{
				link = link.Override(content, title, relationship, @class);
			}

			return html.Raw(link.ToHtml("a").ToString());
		}

		public static IHtmlString RenderCount(this HtmlHelper html, CountModel count, object content = null, string title = null, Relationship relationship = null, MClass @class = null)
		{
			Contract.Requires(html != null);
			Contract.Requires(count != null);

			return html.RenderLink(count.Link.Override(content ?? count.Link.Content ?? count.Value, title, relationship, @class));
		}

		public static IHtmlString RenderNavigationAreaLink(this HtmlHelper html, NavigationArea area, bool isCurrent, string currentClass)
		{
			Contract.Requires(html != null);
			Contract.Requires(area != null);

			return html.RenderLink(isCurrent && Check.That(currentClass).IsNotNullOrEmpty()
				? area.Link.WithClass(new MClass(currentClass))
				: area.Link);
		}
	}
}