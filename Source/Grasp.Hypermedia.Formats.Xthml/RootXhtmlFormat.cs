using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Grasp.Hypermedia.Http;
using Grasp.Hypermedia.Http.Media;
using Grasp.Hypermedia.Http.Mesh;
using Grasp.Knowledge;

namespace Grasp.Hypermedia.Formats.Xthml
{
	// http://www.amundsen.com/blog/archives/1041

	public class RootXhtmlFormat : XhtmlFormat<RootResource>
	{
		public static readonly MediaType MediaType = new MediaType("application/vnd.grasp.api+xhtml");

		public RootXhtmlFormat() : base(MediaType)
		{}

		protected override RootResource ConvertFromMedia(XElement media)
		{
			// TODO: Ensure element is named "api"

			var resource = new RootResource();

			RootResource.DescriptionField.Set(resource, (string) media.Attribute("description"));
			RootResource.MediaTypesField.Set(resource, new Many<MediaType>());
			RootResource.RelationshipsField.Set(resource, new Many<Relationship>());

			return resource;
		}

		protected override XElement ConvertToMedia(RootResource resource)
		{
			return new XElement("api", new XAttribute("description", resource.Description));
		}
	}
}