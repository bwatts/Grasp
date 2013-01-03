using System;
using System.Collections.Generic;
using System.Linq;
using Cloak.Http.Media;
using Grasp.Hypermedia.Linq;

namespace Dash.Http
{
	public class ApplicationHtmlFormat : HtmlFormat<ApplicationResource>
	{
		public static readonly MediaType MediaType = new MediaType("application/vnd.dash.application+html");

		public ApplicationHtmlFormat() : base(MediaType)
		{}

		protected override MClass MediaTypeClass
		{
			get { return "application"; }
		}

		protected override ApplicationResource ConvertToResource(MHeader header, MCompositeContent body)
		{
			return new ApplicationResource(header);
		}

		protected override IEnumerable<MContent> ConvertFromResource(ApplicationResource resource)
		{
			yield break;
		}
	}
}