using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Grasp.Hypermedia.Http.Media;
using Grasp.Knowledge;

namespace Grasp.Hypermedia.Formats.Xthml
{
	public class RelationshipXhtmlFormat : XhtmlFormat<Relationship>
	{
		public static readonly MediaType MediaType = new MediaType("application/vnd.grasp.meta.relationship+xhtml");

		public RelationshipXhtmlFormat() : base(MediaType)
		{}

		protected override Relationship ConvertFromMedia(XElement media)
		{
			// TODO: Ensure element is named "relationship"

			return new Relationship((string) media);
		}

		protected override XElement ConvertToMedia(Relationship resource)
		{
			return new XElement("relationship", resource.Name);
		}
	}
}