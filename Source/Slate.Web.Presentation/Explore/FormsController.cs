using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Slate.Web.Presentation.Navigation;

namespace Slate.Web.Presentation.Explore
{
	public class ExploreController : Controller
	{
		private readonly ILayoutFactory _layoutFactory;

		public ExploreController(ILayoutFactory layoutFactory)
		{
			Contract.Requires(layoutFactory != null);

			_layoutFactory = layoutFactory;
		}

		public async Task<ActionResult> Index()
		{
			return View(await _layoutFactory.CreateLayoutAsync("Slate : Explore", new IndexModel(), "explore", "all"));
		}

		public async Task<ActionResult> Forms()
		{
			return View(await _layoutFactory.CreateLayoutAsync("Slate : Explore : Forms", new FormsModel(), "explore", "forms"));
		}

		public async Task<ActionResult> Responses()
		{
			return View(await _layoutFactory.CreateLayoutAsync("Slate : Explore : Responses", new ResponsesModel(), "explore", "responses"));
		}
	}
}