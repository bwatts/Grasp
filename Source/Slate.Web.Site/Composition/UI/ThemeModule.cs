using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Optimization;
using Cloak.Autofac;

namespace Slate.Web.Site.Composition.UI
{
	public class ThemeModule : BuilderModule
	{
		public ThemeModule(BundleCollection bundles)
		{
			Contract.Requires(bundles != null);

			bundles.Add(new StyleBundle("~/content/themes/base/css").Include(
				"~/Content/Themes/Base/Site.css",
				"~/Content/Themes/Base/Lists.css",
				"~/Content/Themes/Base/Home.css",
				"~/Content/Themes/Base/Explore.css",
				"~/Content/Themes/Base/jQuery/jquery.ui.core.css",
				"~/Content/Themes/Base/jQuery/jquery.ui.resizable.css",
				"~/Content/Themes/Base/jQuery/jquery.ui.selectable.css",
				"~/Content/Themes/Base/jQuery/jquery.ui.accordion.css",
				"~/Content/Themes/Base/jQuery/jquery.ui.autocomplete.css",
				"~/Content/Themes/Base/jQuery/jquery.ui.button.css",
				"~/Content/Themes/Base/jQuery/jquery.ui.dialog.css",
				"~/Content/Themes/Base/jQuery/jquery.ui.slider.css",
				"~/Content/Themes/Base/jQuery/jquery.ui.tabs.css",
				"~/Content/Themes/Base/jQuery/jquery.ui.datepicker.css",
				"~/Content/Themes/Base/jQuery/jquery.ui.progressbar.css",
				"~/Content/Themes/Base/jQuery/jquery.ui.theme.css"));
		}
	}
}