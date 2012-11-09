using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Grasp.Lists;
using Slate.Web.Site.Presentation.Navigation;

namespace Slate.Web.Site.Presentation.Home
{
	public class HomeController : Controller
	{
		private readonly ILayoutModelFactory _layoutModelFactory;
		private readonly IIndexModelFactory _indexModelFactory;

		public HomeController(ILayoutModelFactory layoutModelFactory, IIndexModelFactory indexModelFactory)
		{
			Contract.Requires(layoutModelFactory != null);
			Contract.Requires(indexModelFactory != null);

			_layoutModelFactory = layoutModelFactory;
			_indexModelFactory = indexModelFactory;
		}

		public async Task<ActionResult> Index(ListPageKey formPageKey, ListPageKey issuePageKey)
		{
			return View(await CreateIndexModelAsync(formPageKey, issuePageKey));
		}

		private async Task<object> CreateIndexModelAsync(ListPageKey formPageKey, ListPageKey issuePageKey)
		{
			var indexModel = await _indexModelFactory.CreateIndexModelAsync(formPageKey, issuePageKey);

			return await _layoutModelFactory.CreateLayoutModelAsync("Slate : Home", indexModel, "home");
		}
	}
}