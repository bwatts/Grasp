using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Cloak.Http.Media;
using Grasp.Hypermedia.Linq;
using Grasp.Work;
using Grasp.Work.Items;

namespace Grasp.Hypermedia
{
	public class WorkItemHtmlFormat : HtmlFormat<WorkItemResource>
	{
		public static readonly MediaType MediaType = new MediaType("application/vnd.grasp.work-item+html");

		public WorkItemHtmlFormat() : base(MediaType)
		{}

		protected override MClass MediaTypeClass
		{
			get { return "work-item"; }
		}

		protected override WorkItemResource ConvertToResource(MHeader header, MCompositeContent body)
		{
			var id = body.Items.ReadValue<Guid>("id");
			var whenStarted = body.Items.ReadValue<DateTime>("when-started");
			var progress = body.Items.ReadValue<Progress>("progress");

			return progress == Progress.Complete
				? new WorkItemResource(header, id, whenStarted, body.Items.ReadLink("grasp:work-result").Hyperlink)
				: new WorkItemResource(header, id, whenStarted, progress, body.Items.ReadValue<TimeSpan>("retry-interval"));
		}

		protected override IEnumerable<MContent> ConvertFromResource(WorkItemResource resource)
		{
			yield return new MValue("when-started", resource.WhenStarted);
			yield return new MValue("progress", resource.Progress);

			yield return resource.RetryInterval != null
				? new MValue("retry-interval", resource.RetryInterval)
				: new MLink(resource.ResultLink) as MContent;
		}
	}
}