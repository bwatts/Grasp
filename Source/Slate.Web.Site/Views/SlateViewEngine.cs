using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cloak;

namespace Slate.Web.Site.Views
{
	public class SlateViewEngine : RazorViewEngine
	{
		public SlateViewEngine()
		{
			ViewLocationFormats = Params.Of("~/Views/{0}.cshtml", "~/Views/{1}/{0}.cshtml");

			PartialViewLocationFormats = Params.Of("~/Views/{0}.cshtml", "~/Views/{1}/{0}.cshtml", "~/Views/Site/{0}.cshtml");
		}
	}
}