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
	public class Hyperlink : Notion
	{
		public static readonly XName ClassAttributeName = "class";
		public static readonly XName HrefAttributeName = "href";
		public static readonly XName TitleAttributeName = "title";
		public static readonly XName RelAttributeName = "rel";

		public static readonly Field<UriTemplate> UriField = Field.On<Hyperlink>.For(x => x.Uri);
		public static readonly Field<object> ContentField = Field.On<Hyperlink>.For(x => x.Content);
		public static readonly Field<string> TitleField = Field.On<Hyperlink>.For(x => x.Title);
		public static readonly Field<Relationship> RelationshipField = Field.On<Hyperlink>.For(x => x.Relationship);

		public static readonly Hyperlink Empty = new Hyperlink("");

		public Hyperlink(UriTemplate uri, object content = null, string title = null, Relationship relationship = null)
		{
			Contract.Requires(uri != null);

			Uri = uri;
			Content = content;
			Relationship = relationship ?? Relationship.Empty;
			Title = title ?? "";
		}

		public Hyperlink(Uri uri, object content = null, string title = "", Relationship relationship = null)
			: this(new UriTemplate(uri.ToString()), content, title, relationship)
		{}

		public Hyperlink(string uri, object content = null, string title = "", Relationship relationship = null)
			: this(new UriTemplate(uri), content, title, relationship)
		{}

		public UriTemplate Uri { get { return GetValue(UriField); } private set { SetValue(UriField, value); } }
		public object Content { get { return GetValue(ContentField); } private set { SetValue(ContentField, value); } }
		public string Title { get { return GetValue(TitleField); } private set { SetValue(TitleField, value); } }
		public Relationship Relationship { get { return GetValue(RelationshipField); } private set { SetValue(RelationshipField, value); } }

		public bool IsTemplate
		{
			get { return Check.That(Uri).HasVariables(); }
		}

		public override string ToString()
		{
			return ToHtml("a", allowTemplate: true).ToString();
		}

		public Hyperlink WithClass(MClass @class)
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

			var link = new Hyperlink(Uri, Content, Title, Relationship);

			MClass.ValueField.Set(link, currentClass);

			return link;
		}

		public Hyperlink Override(object content = null, string title = null, Relationship relationship = null)
		{
			return new Hyperlink(Uri, content ?? Content, title ?? Title, relationship ?? Relationship);
		}

		public Hyperlink BindVariables(IDictionary<string, string> bindings)
		{
			Contract.Requires(bindings != null);

			return new Hyperlink(Uri.BindByName(new Uri(""), bindings), Content, Title, Relationship);
		}

		public Hyperlink BindVariables(IDictionary<string, object> bindings)
		{
			return BindVariables(bindings.ToDictionary(
				binding => binding.Key,
				binding => binding.Value == null ? "" : binding.Value.ToString()));
		}

		public XElement ToHtml(XName elementName, bool allowTemplate = true)
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

			if(!allowTemplate && IsTemplate)
			{
				throw new HypermediaException(Resources.LinkUriDoesNotAllowVariables.FormatCurrent(Uri));
			}

			yield return new XAttribute(HrefAttributeName, Uri);

			if(Content != null)
			{
				yield return Content;
			}
		}
	}
}