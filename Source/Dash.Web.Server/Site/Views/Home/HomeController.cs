using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dash.Web.Server.Site.Views.Home
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}