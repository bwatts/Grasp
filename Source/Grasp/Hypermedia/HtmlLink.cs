using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Cloak;
using Grasp.Checks;
using Grasp.Hypermedia.Linq;

namespace Grasp.Hypermedia
{
	public class HtmlLink : Notion
	{
		public static readonly XName ClassAttributeName = "class";
		public static readonly XName HrefAttributeName = "href";
		public static readonly XName TitleAttributeName = "title";
		public static readonly XName RelAttributeName = "rel";

		public static readonly Field<Href> HrefField = Field.On<HtmlLink>.For(x => x.Href);
		public static readonly Field<object> ContentField = Field.On<HtmlLink>.For(x => x.Content);
		public static readonly Field<string> TitleField = Field.On<HtmlLink>.For(x => x.Title);
		public static readonly Field<Relationship> RelationshipField = Field.On<HtmlLink>.For(x => x.Relationship);

		public static readonly HtmlLink Empty = new HtmlLink("");

		public HtmlLink(Href href, object content = null, string title = null, Relationship relationship = null)
		{
			Contract.Requires(href != null);

			Href = href;
			Content = content;
			Relationship = relationship ?? Relationship.Empty;
			Title = title ?? "";
		}

		public HtmlLink(string relativeHref, object content = null, string title = "", Relationship relationship = null)
			: this(new Href(relativeHref), content, title, relationship)
		{}

		public Href Href { get { return GetValue(HrefField); } private set { SetValue(HrefField, value); } }
		public object Content { get { return GetValue(ContentField); } private set { SetValue(ContentField, value); } }
		public string Title { get { return GetValue(TitleField); } private set { SetValue(TitleField, value); } }
		public Relationship Relationship { get { return GetValue(RelationshipField); } private set { SetValue(RelationshipField, value); } }

		public bool IsTemplate
		{
			get { return Href.IsTemplate || Href.HasParameters(Title) || (Content is string && Href.HasParameters(((string) Content))); }
		}

		public override string ToString()
		{
			return Href.ToString();
		}

		public HtmlLink BindParameter(string parameterName, string value)
		{
			return new HtmlLink(
				Href.BindParameter(parameterName, value),
				BindContent(parameterName, value),
				BindParameter(Title, parameterName, value),
				BindRelationship(parameterName, value));
		}

		public HtmlLink WithClass(MClass @class)
		{
			Contract.Requires(@class != null);

			MClass currentClass;

			if(MClass.ValueField.TryGet(this, out currentClass))
			{
				currentClass.Append(@class);
			}
			else
			{
				currentClass = @class;
			}

			var link = new HtmlLink(Href, Content, Title, Relationship);

			MClass.ValueField.Set(link, currentClass);

			return link;
		}

		public HtmlLink Override(object content = null, string title = null, Relationship relationship = null)
		{
			return new HtmlLink(Href, content ?? Content, title ?? Title, relationship ?? Relationship);
		}

		public XElement ToHtml(XName elementName, bool allowTemplate = false)
		{
			return new XElement(elementName, GetHtmlContent(allowTemplate));
		}

		private IEnumerable<object> GetHtmlContent(bool allowTemplate)
		{
			MClass @class;

			if(MClass.ValueField.TryGet(this, out @class))
			{
				yield return new XAttribute(ClassAttributeName, @class.ToStackString());
			}

			if(Check.That(Title).IsNotNullOrEmpty())
			{
				yield return new XAttribute(TitleAttributeName, Title);
			}

			if(Relationship != null && Relationship != Relationship.Empty)
			{
				yield return new XAttribute(RelAttributeName, Relationship);
			}

			yield return new XAttribute(HrefAttributeName, Href.ToUri(allowTemplate));

			if(Content != null)
			{
				yield return Content;
			}
		}

		private object BindContent(string parameterName, string value)
		{
			return Content is string ? BindParameter((string) Content, parameterName, value) : Content;
		}

		private string BindParameter(string template, string parameterName, string value)
		{
			return Check.That(template).IsNullOrEmpty()
				? template
				: template.Replace(HrefPart.ParameterText(parameterName), value);
		}

		private Relationship BindRelationship(string parameterName, string value)
		{
			var relationship = Relationship;

			if(Relationship != null && Relationship != Relationship.Empty)
			{
				var boundRelationship = BindParameter(Relationship.Name, parameterName, value);

				if(boundRelationship != Relationship.Name)
				{
					relationship = new Relationship(boundRelationship);
				}
			}

			return relationship;
		}
	}
}