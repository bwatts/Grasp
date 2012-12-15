using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Cloak.Http.Media;
using Grasp.Hypermedia.Linq;
using Grasp.Hypermedia.Linq.Forms;
using Grasp.Knowledge;
using Grasp.Knowledge.Forms;

namespace Grasp.Hypermedia.Forms
{
	public class HyperformHtmlFormat : HtmlFormat<Hyperform>
	{
		public static readonly MediaType MediaType = new MediaType("application/vnd.grasp.form+html");

		public HyperformHtmlFormat() : base(MediaType)
		{}

		protected override MClass MediaTypeClass
		{
			get { return "form"; }
		}

		protected override IEnumerable<MContent> ConvertFromResource(Hyperform resource)
		{
			yield return new MForm(
				"",
				resource.Action,
				resource.Name,
				resource.Method,
				resource.MediaType,
				resource.AcceptedMediaTypes,
				GetInputItems(resource.Inputs));
		}

		protected override Hyperform ConvertToResource(MHeader header, MCompositeContent body)
		{
			var form = body.Items.OfType<MForm>().First();

			return new Hyperform(
				header,
				form.Action,
				form.Name,
				form.Method,
				form.MediaType,
				form.AcceptedMediaTypes,
				ReadInputs(form));
		}

		#region From Resource

		private IEnumerable<MContent> GetInputItems(IEnumerable<HyperformInput> inputs)
		{
			// TODO

			yield break;
		}
		#endregion

		#region To Resource

		private static IEnumerable<HyperformInput> ReadInputs(MForm form)
		{
			// TODO

			yield break;
		}
		#endregion
	}
}