using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Grasp.Hypermedia.Http.Media;
using Grasp.Knowledge;

namespace Grasp.Hypermedia.Formats.Xthml
{
	public class RelationshipsXhtmlFormat : XhtmlFormat<IEnumerable<Relationship>>
	{
		public static readonly MediaType MediaType = new MediaType("application/vnd.grasp.meta.relationships+xhtml");

		public RelationshipsXhtmlFormat() : base(MediaType)
		{}

		protected override IEnumerable<Relationship> ConvertFromMedia(XElement media)
		{
			// TODO: Ensure element is named "relationships"

			return media.Elements().Select(element => new Relationship((string) element));
		}

		protected override XElement ConvertToMedia(IEnumerable<Relationship> resource)
		{
			return new XElement("relationships", resource.Select(relationship => new XElement("relationship", relationship.Name)));
		}
	}
}