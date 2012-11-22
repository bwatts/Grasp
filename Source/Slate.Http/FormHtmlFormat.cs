using System;
using System.Collections.Generic;
using System.Linq;
using Cloak.Http.Media;
using Grasp.Hypermedia.Linq;

namespace Slate.Http
{
	public class FormHtmlFormat : HtmlFormat<FormResource>
	{
		public static readonly MediaType MediaType = new MediaType("application/vnd.slate.form+html");

		public FormHtmlFormat() : base(MediaType)
		{}

		protected override MClass MediaTypeClass
		{
			get { return "form"; }
		}

		protected override FormResource ConvertToResource(MHeader header, MCompositeContent body)
		{
			return new FormResource(header);
		}

		protected override IEnumerable<MContent> ConvertFromResource(FormResource resource)
		{
			yield break;
		}
	}
}