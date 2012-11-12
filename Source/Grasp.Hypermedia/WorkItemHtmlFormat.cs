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
		public static readonly MediaType MediaType = new MediaType("application/vnd.grasp.work.item+html");

		public WorkItemHtmlFormat() : base(MediaType)
		{}

		protected override MRepresentation ConvertToRepresentation(WorkItemResource media)
		{
			return new MRepresentation(GetHeader(media), GetBody(media));
		}

		protected override WorkItemResource ConvertFromRepresentation(MRepresentation representation, IFormatterLogger formatterLogger)
		{
			var body = representation.Body as MCompositeContent;

			if(body == null)
			{
				throw new FormatException(Resources.ExpectingCompositeBodyContent);
			}

			var header = ReadHeader(representation.Head);

			var id = body.Items.ReadValue<Guid>("id");
			var status = body.Items.ReadValue<string>("status");

			if(status.Equals("Accepted", StringComparison.CurrentCultureIgnoreCase))
			{
				return new WorkItemResource(header, id, status, body.Items.ReadValue<TimeSpan>("retry-interval"));
			}
			else if(status.Equals("In progress", StringComparison.CurrentCultureIgnoreCase))
			{
				return new WorkItemResource(header, id, status, body.Items.ReadValue<TimeSpan>("retry-interval"), body.Items.ReadValue<Progress>("progress"));
			}
			else
			{
				return new WorkItemResource(header, id, status, body.Items.ReadLink("grasp:work-result").Hyperlink);
			}
		}

		private static MHead GetHeader(WorkItemResource item)
		{
			return new MHead(item.Header.Title, item.Header.BaseLink, item.Header.Links);
		}

		private static MContent GetBody(WorkItemResource item)
		{
			return new MCompositeContent(GetBodyContent(item));
		}

		private static IEnumerable<MContent> GetBodyContent(WorkItemResource item)
		{
			yield return new MValue("id", item.Id);
			yield return new MValue("status", item.Status);

			if(item.RetryInterval != null)
			{
				yield return new MValue("retry-interval", item.RetryInterval);
			}

			if(item.Progress != null)
			{
				yield return new MValue("progress", item.Progress);
			}

			if(item.ResultLink != null)
			{
				yield return new MLink(item.ResultLink);
			}
		}

		private static HttpResourceHeader ReadHeader(MHead head)
		{
			return new HttpResourceHeader(head.Title, head.BaseLink, head.Links);
		}
	}
}