using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Grasp.Lists;
using Slate.Web.Presentation.Navigation;

namespace Slate.Web.Presentation.Home
{
	public class HomeController : Controller
	{
		private readonly ILayoutFactory _layoutFactory;
		private readonly IIndexFactory _indexFactory;

		public HomeController(ILayoutFactory layoutFactory, IIndexFactory indexFactory)
		{
			Contract.Requires(layoutFactory != null);
			Contract.Requires(indexFactory != null);

			_layoutFactory = layoutFactory;
			_indexFactory = indexFactory;
		}

		public async Task<ActionResult> Index(ListPageKey formPageKey, ListPageKey issuePageKey)
		{
			return View(await CreateIndexModelAsync(formPageKey, issuePageKey));
		}

		private async Task<object> CreateIndexModelAsync(ListPageKey formPageKey, ListPageKey issuePageKey)
		{
			var indexModel = await _indexFactory.CreateIndexAsync(formPageKey, issuePageKey);

			return await _layoutFactory.CreateLayoutAsync("Slate : Home", indexModel, "home");
		}
	}
}