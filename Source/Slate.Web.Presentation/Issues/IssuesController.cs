using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Slate.Web.Presentation.Navigation;

namespace Slate.Web.Presentation.Issues
{
	public class IssuesController : Controller
	{
		private readonly ILayoutFactory _layoutFactory;

		public IssuesController(ILayoutFactory layoutFactory)
		{
			Contract.Requires(layoutFactory != null);

			_layoutFactory = layoutFactory;
		}

		public async Task<ActionResult> Index()
		{
			return View(await _layoutFactory.CreateLayoutAsync("Slate : Issues", new IndexModel(), "issues", "all"));
		}

		public async Task<ActionResult> Open()
		{
			return View(await _layoutFactory.CreateLayoutAsync("Slate : Issues : Open", new OpenModel(), "issues", "open"));
		}

		public async Task<ActionResult> Mine()
		{
			return View(await _layoutFactory.CreateLayoutAsync("Slate : Issues : Mine", new MineModel(), "issues", "mine"));
		}
	}
}