using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Optimization;
using Cloak.Autofac;

namespace Slate.Web.Site.Composition.UI
{
	public class ModernizrModule : BuilderModule
	{
		public ModernizrModule(BundleCollection bundles)
		{
			Contract.Requires(bundles != null);

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.

			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Content/Scripts/modernizr-*"));
		}
	}
}