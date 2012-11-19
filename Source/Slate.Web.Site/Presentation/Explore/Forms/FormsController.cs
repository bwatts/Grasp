using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Grasp.Hypermedia;
using Grasp.Work.Items;
using Slate.Web.Site.Presentation.Navigation;
using Slate.Web.Site.Presentation.Work;

namespace Slate.Web.Site.Presentation.Explore.Forms
{
	public class FormsController : Controller
	{
		private readonly ILayoutModelFactory _layoutModelFactory;
		private readonly IFormService _formService;
		private readonly IWorkMesh _workMesh;

		public FormsController(ILayoutModelFactory layoutModelFactory, IFormService formService, IWorkMesh workMesh)
		{
			Contract.Requires(layoutModelFactory != null);
			Contract.Requires(formService != null);
			Contract.Requires(workMesh != null);

			_layoutModelFactory = layoutModelFactory;
			_formService = formService;
			_workMesh = workMesh;
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
		public async Task<ActionResult> Start(string name)
		{
			var workItem = await _formService.StartFormAsync(name);

			// Instead of redirecting to a generic handler when the work is not complete, we could return the page in a polling state. This would
			// maintain UX context and allow us to portray it in a manner specific to the work, such as a progress bar or explict set of steps.

			var redirectUri = workItem.Progress == Progress.Complete
				? _workMesh.GetResultUrl(workItem)
				: _workMesh.GetItemUri(workItem);

			return Redirect(redirectUri.ToString());
		}
	}
}