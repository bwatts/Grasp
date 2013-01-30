﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Optimization;
using Cloak.Autofac;

namespace Slate.Web.Site.Composition.UI
{
	public class JQueryModule : BuilderModule
	{
		public JQueryModule(BundleCollection bundles)
		{
			Contract.Requires(bundles != null);

			bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Content/Scripts/jquery-{version}.js"));
			bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include("~/Content/Scripts/jquery-ui-{version}.js"));
			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Content/Scripts/jquery.unobtrusive*", "~/Content/Scripts/jquery.validate*"));
		}
	}
}