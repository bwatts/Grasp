using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Grasp.Hypermedia.Http.Mesh
{
	public class RootMedia : Notion
	{
		public static readonly Field<string> DescriptionField = Field.On<RootMedia>.Backing(x => x.Description);
		public static readonly Field<Many<MediaType>> MediaTypesField = Field.On<RootMedia>.Backing(x => x.MediaTypes);
		public static readonly Field<Many<Relationship>> RelationshipsField = Field.On<RootMedia>.Backing(x => x.Relationships);

		public string Description { get { return GetValue(DescriptionField); } }
		public Many<MediaType> MediaTypes { get { return GetValue(MediaTypesField); } }
		public Many<Relationship> Relationships { get { return GetValue(RelationshipsField); } }
	}
}