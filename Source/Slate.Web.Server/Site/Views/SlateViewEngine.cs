using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Slate.Web.Server.Site.Views
{
	public class SlateViewEngine : RazorViewEngine
	{
		public SlateViewEngine(IEnumerable<string> viewLocationFormats)
		{
			Contract.Requires(viewLocationFormats != null);

			ViewLocationFormats = viewLocationFormats.ToArray();
		}

		public SlateViewEngine(params string[] viewLocationFormats) : this(viewLocationFormats as IEnumerable<string>)
		{}
	}
}