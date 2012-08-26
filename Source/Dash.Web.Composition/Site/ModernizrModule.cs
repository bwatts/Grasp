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
	public class ModernizrModule : BuilderModule
	{
		public ModernizrModule(BundleCollection bundles)
		{
			Contract.Requires(bundles != null);

			bundles.Add(new ScriptBundle("~/site/bundles/modernizr").Include("~/Site/Scripts/modernizr-*"));
		}
	}
}