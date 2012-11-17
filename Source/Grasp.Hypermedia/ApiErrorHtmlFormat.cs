using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using Cloak.Http;
using Cloak.Http.Media;
using Grasp.Hypermedia.Linq;

namespace Grasp.Hypermedia
{
	public class ApiErrorHtmlFormat : HtmlFormat<ApiErrorResource>
	{
		public static readonly MediaType MediaType = new MediaType("application/vnd.grasp.error+html");

		public ApiErrorHtmlFormat() : base(MediaType)
		{}

		protected override MClass MediaTypeClass
		{
			get { return "error"; }
		}

		public override bool CanReadType(Type type)
		{
			return type == typeof(IApiError) || base.CanReadType(type);
		}

		protected override ApiErrorResource ConvertToResource(MHeader header, MCompositeContent body)
		{
			return new ApiErrorResource(
				header,
				body.Items.ReadValue<string>("message"),
				body.Items.ReadValue<long>("code"));
		}

		protected override IEnumerable<MContent> ConvertFromResource(ApiErrorResource resource)
		{
			yield return new MValue("message", resource.Message, escaped: true);
			yield return new MValue("code", resource.Code);
		}
	}
}