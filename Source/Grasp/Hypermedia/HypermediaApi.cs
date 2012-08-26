using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Grasp.Hypermedia
{
	public class HypermediaApi : Notion
	{
		public static readonly Field<string> NameField = Field.On<HypermediaApi>.Backing(x => x.Name);
		public static readonly Field<Many<Relationship>> RelationshipsField = Field.On<HypermediaApi>.Backing(x => x.Relationships);

		public string Name { get { return GetValue(NameField); } }
		public Many<Relationship> Relationships { get { return GetValue(RelationshipsField); } }
	}
}