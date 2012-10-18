using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Optimization;
using Cloak.Autofac;

namespace Slate.Web.Server.Site
{
	public class JQueryModule : BuilderModule
	{
		public JQueryModule(BundleCollection bundles)
		{
			Contract.Requires(bundles != null);

			bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Site/Scripts/jquery-{version}.js"));
			bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include("~/Site/Scripts/jquery-ui-{version}.js"));
			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Site/Scripts/jquery.unobtrusive*", "~/Site/Scripts/jquery.validate*"));
		}
	}
}