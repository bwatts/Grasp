using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Hypermedia.Http.Media;
using Grasp.Knowledge;

namespace Grasp.Hypermedia.Http.Mesh
{
	public class RootResource : Notion
	{
		public static readonly Field<string> DescriptionField = Field.On<RootResource>.Backing(x => x.Description);
		public static readonly Field<Many<MediaType>> MediaTypesField = Field.On<RootResource>.Backing(x => x.MediaTypes);
		public static readonly Field<Many<Relationship>> RelationshipsField = Field.On<RootResource>.Backing(x => x.Relationships);

		public string Description { get { return GetValue(DescriptionField); } }
		public Many<MediaType> MediaTypes { get { return GetValue(MediaTypesField); } }
		public Many<Relationship> Relationships { get { return GetValue(RelationshipsField); } }
	}
}