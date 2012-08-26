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
	public class JQueryModule : BuilderModule
	{
		public JQueryModule(BundleCollection bundles)
		{
			Contract.Requires(bundler != null);

			bundles.Add(new ScriptBundle("~/bundles/jquery").Include("jquery-1.*"));
			bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include("jquery-1.*"));
			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("jquery-unobtrusive*"));
			bundles.Add(new ScriptBundle("~/bundles/jquery-unobtrusive").Include("jquery-unobtrusive*", "jquery.validate*"));
		}
	}
}