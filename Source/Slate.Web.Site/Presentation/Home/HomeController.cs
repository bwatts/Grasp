using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Cloak;
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

		public async Task<ActionResult> Index(ListViewKey formPageKey, ListViewKey issuePageKey)
		{
			return View(await CreateIndexModelAsync(formPageKey, issuePageKey));
		}

		private async Task<object> CreateIndexModelAsync(ListViewKey formPageKey, ListViewKey issuePageKey)
		{
			// TODO: Determine how to set this convention at a  more fundamental level
			formPageKey = new ListViewKey(new Count(1), new Count(3), formPageKey.Sort);

			var indexModel = await _indexModelFactory.CreateIndexModelAsync(formPageKey, issuePageKey);

			return await _layoutModelFactory.CreateLayoutModelAsync("Slate : Home", indexModel, "home");
		}
	}
}