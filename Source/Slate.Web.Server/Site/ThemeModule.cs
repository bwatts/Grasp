using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Optimization;
using Cloak.Autofac;

namespace Slate.Web.Server.Site
{
	public class ThemeModule : BuilderModule
	{
		public ThemeModule(BundleCollection bundles)
		{
			Contract.Requires(bundles != null);

			bundles.Add(new StyleBundle("~/content/css").Include("~/Site/Content/Site.css"));

			bundles.Add(new StyleBundle("~/content/themes/base/css").Include(
				"~/Site/Content/Themes/Base/jquery.ui.core.css",
				"~/Site/Content/Themes/Base/jquery.ui.resizable.css",
				"~/Site/Content/Themes/Base/jquery.ui.selectable.css",
				"~/Site/Content/Themes/Base/jquery.ui.accordion.css",
				"~/Site/Content/Themes/Base/jquery.ui.autocomplete.css",
				"~/Site/Content/Themes/Base/jquery.ui.button.css",
				"~/Site/Content/Themes/Base/jquery.ui.dialog.css",
				"~/Site/Content/Themes/Base/jquery.ui.slider.css",
				"~/Site/Content/Themes/Base/jquery.ui.tabs.css",
				"~/Site/Content/Themes/Base/jquery.ui.datepicker.css",
				"~/Site/Content/Themes/Base/jquery.ui.progressbar.css",
				"~/Site/Content/Themes/Base/jquery.ui.theme.css"));
		}
	}
}