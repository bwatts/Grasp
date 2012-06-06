using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Grasp.Hypermedia
{
	public class HypermediaApi : Notion
	{
		public static readonly Field<Many<MediaType>> MediaTypesField = Field.On<HypermediaApi>.For(x => x.MediaTypes);
		public static readonly Field<Many<Relationship>> RelationshipsField = Field.On<HypermediaApi>.For(x => x.Relationships);

		public Many<MediaType> MediaTypes { get { return GetValue(MediaTypesField); } }
		public Many<Relationship> Relationships { get { return GetValue(RelationshipsField); } }
	}
}