using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Grasp.Hypermedia.Http
{
	public class HttpLink : Notion
	{
		public static readonly Field<Uri> HrefField = Field.On<HttpLink>.Backing(x => x.Href);
		public static readonly Field<string> TitleField = Field.On<HttpLink>.Backing(x => x.Title);
		public static readonly Field<string> RelationshipField = Field.On<HttpLink>.Backing(x => x.Relationship);

		public Uri Href { get { return GetValue(HrefField); } }
		public string Title { get { return GetValue(TitleField); } }
		public string Relationship { get { return GetValue(RelationshipField); } }
	}
}