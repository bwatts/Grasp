using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Slate.Web.Site.Presentation.Navigation;

namespace Slate.Web.Site.Presentation.Issues
{
	public class IssuesController : Controller
	{
		private readonly ILayoutModelFactory _layoutModelFactory;

		public IssuesController(ILayoutModelFactory layoutModelFactory)
		{
			Contract.Requires(layoutModelFactory != null);

			_layoutModelFactory = layoutModelFactory;
		}

		public async Task<ActionResult> Index()
		{
			return View(await _layoutModelFactory.CreateLayoutModelAsync("Slate : Issues", new IndexModel(), "issues", "all"));
		}

		public async Task<ActionResult> Open()
		{
			return View(await _layoutModelFactory.CreateLayoutModelAsync("Slate : Issues : Open", new OpenModel(), "issues", "open"));
		}

		public async Task<ActionResult> Mine()
		{
			return View(await _layoutModelFactory.CreateLayoutModelAsync("Slate : Issues : Mine", new MineModel(), "issues", "mine"));
		}
	}
}