﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Slate.Web.Site.Presentation.Navigation;

namespace Slate.Web.Site.Presentation.Explore
{
	public class ExploreController : Controller
	{
		private readonly ILayoutModelFactory _layoutModelFactory;
		
		public ExploreController(ILayoutModelFactory layoutModelFactory)
		{
			Contract.Requires(layoutModelFactory != null);

			_layoutModelFactory = layoutModelFactory;
		}

		[HttpGet]
		public async Task<ActionResult> Index()
		{
			return View(await _layoutModelFactory.CreateLayoutModelAsync("Slate : Explore", new IndexModel(), "explore", "all"));
		}
	}
}