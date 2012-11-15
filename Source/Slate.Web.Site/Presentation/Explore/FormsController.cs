using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Slate.Web.Site.Presentation.Navigation;

namespace Slate.Web.Site.Presentation.Explore
{
	public class FormsController : Controller
	{
		private readonly ILayoutModelFactory _layoutModelFactory;
		private readonly IStartFormService _startFormService;

		public FormsController(ILayoutModelFactory layoutModelFactory, IStartFormService startFormService)
		{
			Contract.Requires(layoutModelFactory != null);
			Contract.Requires(startFormService != null);

			_layoutModelFactory = layoutModelFactory;
			_startFormService = startFormService;
		}

		[HttpGet]
		public async Task<ActionResult> List()
		{
			return View(await _layoutModelFactory.CreateLayoutModelAsync("Slate : Explore : Forms", new FormListModel(), "explore", "forms"));
		}

		[HttpGet]
		public async Task<ActionResult> Details(Guid id)
		{
			return View(await _layoutModelFactory.CreateLayoutModelAsync("Slate : Explore : Form : Details", new FormDetailsModel(), "explore", "forms"));
		}

		[HttpGet]
		public async Task<ActionResult> Start()
		{
			return View(await _layoutModelFactory.CreateLayoutModelAsync("Slate : Explore : Form : Start", new StartFormModel(), "explore", "forms"));
		}

		[HttpPost]
		public async Task<RedirectToRouteResult> Start(string name)
		{
			var id = await _startFormService.StartFormAsync(name);

			// HTTP 202? Is that relevant in an MVC site?

			return RedirectToRoute(new { controller = "Forms", action = "Details", id = id });
		}
	}
}