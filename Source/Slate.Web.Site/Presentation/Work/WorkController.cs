using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Cloak.Time;
using Grasp;
using Grasp.Hypermedia;
using Grasp.Work.Items;
using Slate.Web.Site.Presentation.Navigation;

namespace Slate.Web.Site.Presentation.Work
{
	public class WorkController : Controller
	{
		private readonly ITimeContext _timeContext;
		private readonly IWorkService _workService;
		private readonly IWorkMesh _workMesh;
		private readonly ILayoutModelFactory _layoutModelFactory;

		public WorkController(ITimeContext timeContext, IWorkService workService, IWorkMesh workMesh, ILayoutModelFactory layoutModelFactory)
		{
			Contract.Requires(timeContext != null);
			Contract.Requires(workService != null);
			Contract.Requires(workMesh != null);
			Contract.Requires(layoutModelFactory != null);

			_timeContext = timeContext;
			_workService = workService;
			_workMesh = workMesh;
			_layoutModelFactory = layoutModelFactory;
		}

		[HttpGet]
		public async Task<ActionResult> Item(EntityId id)
		{
			var workItem = await _workService.GetWorkItemAsync(id);

			if(workItem == null)
			{
				return new HttpNotFoundResult();
			}
			else if(workItem.Progress == Progress.Complete)
			{
				return new RedirectResult(_workMesh.GetResultUrl(workItem).ToString());
			}
			else
			{
				return View(await CreateLayoutModelAsync(workItem)) as ActionResult;
			}
		}

		private Task<ILayoutModel<WorkItemModel>> CreateLayoutModelAsync(WorkItemResource workItem)
		{
			return _layoutModelFactory.CreateLayoutModelAsync("Slate : Working", CreateWorkItemModel(workItem));
		}

		private WorkItemModel CreateWorkItemModel(WorkItemResource workItem)
		{
			return new WorkItemModel(
				workItem.Header.Title,
				workItem.WhenStarted,
				_timeContext.Now - workItem.WhenStarted,
				workItem.Progress,
				Convert.ToInt32(Math.Ceiling(workItem.RetryInterval.Value.TotalMilliseconds)));
		}
	}
}