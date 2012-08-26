using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Dash.Web.Composition.Site
{
	public sealed class SiteViewEngine : RazorViewEngine
	{
		public SiteViewEngine()
		{
			ViewLocationFormats = new[]
			{
				"~/Site/Views/{0}.cshtml",
				"~/Site/Views/{2}/{1}/{0}.cshtml"
			};
		}
	}
}