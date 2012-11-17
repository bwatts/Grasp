using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Cloak.Http.Media;
using Grasp.Hypermedia.Linq;

namespace Grasp.Hypermedia
{
	public class ApiHtmlFormat : HtmlFormat<ApiResource>
	{
		public static readonly MediaType MediaType = new MediaType("application/vnd.grasp.api+html");

		public ApiHtmlFormat() : base(MediaType)
		{}

		protected override MClass MediaTypeClass
		{
			get { return "api"; }
		}

		protected override IEnumerable<MContent> ConvertFromResource(ApiResource resource)
		{
			return resource.Links.Select(link => new MLink(link));
		}

		protected override ApiResource ConvertToResource(MHeader header, MCompositeContent body)
		{
			return new ApiResource(header, body.Items.OfType<MLink>().Select(link => link.Hyperlink));
		}
	}
}