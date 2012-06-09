using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Grasp.Hypermedia.Http.Media;
using Grasp.Knowledge;

namespace Grasp.Hypermedia.Formats.Xthml
{
	public class MediaTypeXhtmlFormat : XhtmlFormat<MediaType>
	{
		public static readonly MediaType MediaType = new MediaType("application/vnd.grasp.meta.media-type+xhtml");

		public MediaTypeXhtmlFormat() : base(MediaType)
		{}

		protected override MediaType ConvertFromMedia(XElement media)
		{
			// TODO: Ensure element is named "mediaType"

			return new MediaType((string) media);
		}

		protected override XElement ConvertToMedia(MediaType resource)
		{
			return new XElement("mediaType", resource.Value);
		}
	}
}