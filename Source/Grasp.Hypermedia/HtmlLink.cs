using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Cloak.Http;
using Grasp.Checks;
using Grasp.Knowledge;

namespace Grasp.Hypermedia
{
	public class HtmlLink : Notion
	{
		public static readonly XName RelAttributeName = "rel";
		public static readonly XName HrefAttributeName = "href";
		public static readonly XName TargetAttributeName = "target";

		public static readonly Field<Uri> HrefField = Field.On<HtmlLink>.Backing(x => x.Href);
		public static readonly Field<ItemStack<Relationship>> RelationshipField = Field.On<HtmlLink>.Backing(x => x.Relationship);
		public static readonly Field<string> TargetField = Field.On<HtmlLink>.Backing(x => x.Target);
		public static readonly Field<object> ContentField = Field.On<HtmlLink>.Backing(x => x.Content);

		public HtmlLink(Uri href, Relationship relationship = null, string target = "", object content = null)
		{
			Contract.Requires(href != null);
			Contract.Requires(target != null);

			Href = href;
			Relationship = new ItemStack<Relationship>(relationship ?? Hypermedia.Relationship.Default);
			Target = target;
			Content = content;
		}

		public Uri Href { get { return GetValue(HrefField); } private set { SetValue(HrefField, value); } }
		public ItemStack<Relationship> Relationship { get { return GetValue(RelationshipField); } private set { SetValue(RelationshipField, value); } }
		public string Target { get { return GetValue(TargetField); } private set { SetValue(TargetField, value); } }
		public object Content { get { return GetValue(ContentField); } private set { SetValue(ContentField, value); } }

		public XElement ToHtml(XName elementName)
		{
			return new XElement(elementName, GetHtmlContent());
		}

		private IEnumerable<object> GetHtmlContent()
		{
			if(Relationship != null)
			{
				yield return new XAttribute(RelAttributeName, Relationship);
			}

			yield return new XAttribute(HrefAttributeName, Href);

			if(Check.That(Target).IsNotNullOrEmpty())
			{
				yield return new XAttribute(TargetAttributeName, Target);
			}

			if(Content != null)
			{
				yield return Content;
			}
		}
	}
}