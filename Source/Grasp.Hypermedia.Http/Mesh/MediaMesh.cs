using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Grasp.Hypermedia.Http.Mesh
{
	public sealed class MediaMesh : Notion
	{
		public static readonly Field<HttpLink> SelfLinkField = Field.AttachedTo<Notion>.By<MediaMesh>.Backing(() => SelfLinkField);
		public static readonly Field<Many<HttpLink>> LinksField = Field.AttachedTo<Notion>.By<MediaMesh>.Backing(() => LinksField);
	}
}