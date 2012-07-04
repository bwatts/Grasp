using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Grasp.Hypermedia
{
	public class HypermediaApi : Notion
	{
		public static readonly Field<Many<Relationship>> RelationshipsField = Field.On<HypermediaApi>.Backing(x => x.Relationships);

		public Many<Relationship> Relationships { get { return GetValue(RelationshipsField); } }
	}
}