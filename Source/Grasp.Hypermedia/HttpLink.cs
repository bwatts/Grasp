using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Grasp.Hypermedia
{
	public class HttpLink : Notion
	{
		public static readonly Field<Uri> HrefField = Field.On<HttpLink>.Backing(x => x.Href);
		public static readonly Field<string> RelationshipField = Field.On<HttpLink>.Backing(x => x.Relationship);
		public static readonly Field<string> TargetField = Field.On<HttpLink>.Backing(x => x.Target);
		public static readonly Field<string> TextField = Field.On<HttpLink>.Backing(x => x.Text);

		public HttpLink(Uri href, string relationship = "", string target = "", string text = "")
		{
			Contract.Requires(href != null);
			Contract.Requires(relationship != null);
			Contract.Requires(target != null);
			Contract.Requires(text != null);

			Href = href;
			Relationship = relationship;
			Target = target;
			Text = text;
		}

		public Uri Href { get { return GetValue(HrefField); } private set { SetValue(HrefField, value); } }
		public string Relationship { get { return GetValue(RelationshipField); } private set { SetValue(RelationshipField, value); } }
		public string Target { get { return GetValue(TargetField); } private set { SetValue(TargetField, value); } }
		public string Text { get { return GetValue(TextField); } private set { SetValue(TextField, value); } }
	}
}