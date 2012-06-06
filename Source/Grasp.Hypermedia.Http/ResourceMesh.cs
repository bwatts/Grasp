using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Grasp.Hypermedia.Http
{
	public class ResourceMesh : Notion
	{
		public static readonly Field<HttpLink> SelfLinkField = Field.On<ResourceMesh>.Backing(x => x.SelfLink);
		public static readonly Field<Many<HttpLink>> LinksField = Field.On<ResourceMesh>.Backing(x => x.Links);

		public HttpLink SelfLink { get { return GetValue(SelfLinkField); } }
		public Many<HttpLink> Links { get { return GetValue(LinksField); } }
	}
}