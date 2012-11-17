using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Slate.Web.Site.Presentation.Navigation;

namespace Slate.Web.Site.Presentation.Explore.Forms
{
	public class FormsController : Controller
	{
		private readonly ILayoutModelFactory _layoutModelFactory;
		private readonly IFormService _formService;

		public FormsController(ILayoutModelFactory layoutModelFactory, IFormService formService)
		{
			Contract.Requires(layoutModelFactory != null);
			Contract.Requires(formService != null);

			_layoutModelFactory = layoutModelFactory;
			_formService = formService;
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
			var workItem = await _formService.StartFormAsync(name);

			return RedirectToAction("Item", "Work", new { id = workItem.Id.ToString("N").ToUpper() });
		}
	}
}