using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Cloak.Autofac;
using Microsoft.Web.Optimization;

namespace Dash.Web.Composition.Site
{
	public class ThemeModule : BuilderModule
	{
		public ThemeModule(BundleCollection bundles)
		{
			Contract.Requires(bundles != null);

			bundles.Add(new StyleBundle("~/site/css").Include("~/Site/Site.css"));

			bundles.Add(new StyleBundle("~/site/themes/base/css").Include("~/Sites/Themes/Base/jquery.ui.*.css"));
		}
	}
}