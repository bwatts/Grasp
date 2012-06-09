using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Grasp.Hypermedia.Http.Media;

namespace Grasp.Hypermedia.Formats.Xthml
{
	public class MediaTypesXhtmlFormat : XhtmlFormat<IEnumerable<MediaType>>
	{
		public static readonly MediaType MediaType = new MediaType("application/vnd.grasp.meta.media-types+xhtml");

		public MediaTypesXhtmlFormat() : base(MediaType)
		{}

		protected override IEnumerable<MediaType> ConvertFromMedia(XElement media)
		{
			// TODO: Ensure element is named "mediaTypes"

			return media.Elements().Select(element => new MediaType((string) element));
		}

		protected override XElement ConvertToMedia(IEnumerable<MediaType> resource)
		{
			return new XElement("mediaTypes", resource.Select(mediaType => new XElement("mediaType", mediaType.Name)));
		}
	}
}