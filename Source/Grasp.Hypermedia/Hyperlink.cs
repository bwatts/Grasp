using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Cloak;
using Cloak.Linq;
using Grasp.Checks;
using Grasp.Hypermedia.Linq;

namespace Grasp.Hypermedia
{
	public class Hyperlink : Notion
	{
		public static readonly XName ClassAttributeName = "class";
		public static readonly XName TitleAttributeName = "title";
		public static readonly XName RelAttributeName = "rel";
		public static readonly XName HrefAttributeName = "href";

		public static readonly Field<UriTemplate> HrefField = Field.On<Hyperlink>.For(x => x.Href);
		public static readonly Field<object> ContentField = Field.On<Hyperlink>.For(x => x.Content);
		public static readonly Field<string> TitleField = Field.On<Hyperlink>.For(x => x.Title);
		public static readonly Field<Relationship> RelationshipField = Field.On<Hyperlink>.For(x => x.Relationship);
		public static readonly Field<MClass> ClassField = Field.On<Hyperlink>.For(x => x.Class);

		public static readonly Hyperlink Empty = new Hyperlink("");

		public Hyperlink(UriTemplate href, object content = null, string title = null, Relationship relationship = null, MClass @class = null)
		{
			Contract.Requires(href != null);

			Href = href;
			Content = content;
			Relationship = relationship ?? Relationship.Empty;
			Title = title ?? "";
			Class = @class ?? MClass.Empty;
		}

		public Hyperlink(Uri href, object content = null, string title = "", Relationship relationship = null, MClass @class = null)
			: this(new UriTemplate(href.ToString()), content, title, relationship, @class)
		{}

		public Hyperlink(string href, object content = null, string title = "", Relationship relationship = null, MClass @class = null)
			: this(new UriTemplate(href), content, title, relationship, @class)
		{}

		public UriTemplate Href { get { return GetValue(HrefField); } private set { SetValue(HrefField, value); } }
		public object Content { get { return GetValue(ContentField); } private set { SetValue(ContentField, value); } }
		public string Title { get { return GetValue(TitleField); } private set { SetValue(TitleField, value); } }
		public Relationship Relationship { get { return GetValue(RelationshipField); } private set { SetValue(RelationshipField, value); } }
		public MClass Class { get { return GetValue(ClassField); } private set { SetValue(ClassField, value); } }

		public bool IsTemplate
		{
			get { return Check.That(Href).HasVariables(); }
		}

		public Hyperlink WithClass(MClass @class)
		{
			return new Hyperlink(Href, Content, Title, Relationship, @class);
		}

		public Hyperlink Override(object content = null, string title = null, Relationship relationship = null, MClass @class = null)
		{
			return new Hyperlink(Href, content ?? Content, title ?? Title, relationship ?? Relationship, @class ?? Class);
		}

		public Uri BindHrefVariables(IEnumerable<KeyValuePair<string, string>> bindings)
		{
			Contract.Requires(bindings != null);

			var baseUriStub = new Uri("http://x");

			var boundUri = BindHrefVariables(baseUriStub, bindings);

			return baseUriStub.MakeRelativeUri(boundUri);
		}

		public Uri BindHrefVariables(IEnumerable<KeyValuePair<string, object>> bindings)
		{
			Contract.Requires(bindings != null);

			return BindHrefVariables(bindings.ToDictionary(
				binding => binding.Key,
				binding => binding.Value == null ? "" : binding.Value.ToString()));
		}

		public Uri BindHrefVariable(string name, string value)
		{
			Contract.Requires(!String.IsNullOrEmpty(name));

			return BindHrefVariables(new Dictionary<string, string> { { name, value } });
		}

		public Uri BindHrefVariable(string name, object value)
		{
			Contract.Requires(!String.IsNullOrEmpty(name));

			return BindHrefVariables(new Dictionary<string, object> { { name, value } });
		}

		public Hyperlink BindVariables(IEnumerable<KeyValuePair<string, string>> bindings)
		{
			Contract.Requires(bindings != null);

			bindings = bindings.ToReadOnlyDictionary();

			return new Hyperlink(
				BindHrefVariables(bindings),
				Content is string ? BindTemplateVariables((string) Content, bindings) : Content,
				BindTemplateVariables(Title, bindings),
				BindTemplateVariables(Relationship, bindings),
				BindTemplateVariables(Class, bindings));
		}

		public Hyperlink BindVariables(IEnumerable<KeyValuePair<string, object>> bindings)
		{
			Contract.Requires(bindings != null);

			return BindVariables(bindings.ToDictionary(
				binding => binding.Key,
				binding => binding.Value == null ? "" : binding.Value.ToString()));
		}

		public Hyperlink BindVariable(string name, string value)
		{
			Contract.Requires(!String.IsNullOrEmpty(name));

			return BindVariables(new Dictionary<string, string> { { name, value } });
		}

		public Hyperlink BindVariable(string name, object value)
		{
			Contract.Requires(!String.IsNullOrEmpty(name));

			return BindVariables(new Dictionary<string, object> { { name, value } });
		}

		public Hyperlink AppendQuery(string query)
		{
			Contract.Requires(query != null);

			var href = Href.ToString();

			if(Check.That(query).IsNotNullOrEmpty())
			{
				href += (href.Contains('?') ? "&" : "?") + query;
			}

			return new Hyperlink(href, Content, Title, Relationship, Class);
		}

		public Uri ToUri()
		{
			if(IsTemplate)
			{
				throw new InvalidOperationException(Resources.HyperlinkIsTemplate);
			}

			return BindHrefVariables(new Dictionary<string, string>());
		}

		public override string ToString()
		{
			return ToHtml("a", allowTemplate: true).ToString();
		}

		public XElement ToHtml(XName elementName, bool allowTemplate = true)
		{
			return new XElement(elementName, GetHtmlContent(allowTemplate));
		}

		public string ToHttpHeader()
		{
			var header = new StringBuilder();

			header.Append(Href);

			if(Relationship != null && Relationship != Relationship.Empty)
			{
				header.Append("; rel=\"").Append(Relationship).Append("\"");
			}

			return header.ToString();
		}

		private Uri BindHrefVariables(Uri baseHrefStub, IEnumerable<KeyValuePair<string, string>> bindings)
		{
			var variables = Href.PathSegmentVariableNames.Concat(Href.QueryValueVariableNames);

			var effectiveBindings =
				from variable in variables
				from binding in bindings
				where binding.Key.Equals(variable, StringComparison.InvariantCultureIgnoreCase)
				select binding;

			return Href.BindByName(baseHrefStub, effectiveBindings.ToDictionary());
		}

		private static string BindTemplateVariables(string template, IEnumerable<KeyValuePair<string, string>> bindings)
		{
			foreach(var binding in bindings)
			{
				template = template.Replace("{" + binding.Key + "}", binding.Value);
			}

			return template;
		}

		private IEnumerable<object> GetHtmlContent(bool allowTemplate)
		{
			if(!allowTemplate && IsTemplate)
			{
				throw new HypermediaException(Resources.HrefDoesNotAllowVariables.FormatCurrent(Href));
			}

			if(Relationship != null && Relationship != Relationship.Empty)
			{
				yield return new XAttribute(RelAttributeName, Relationship);
			}

			if(Check.That(Title).IsNotNullOrEmpty())
			{
				yield return new XAttribute(TitleAttributeName, Title);
			}

			if(Class != MClass.Empty)
			{
				yield return new XAttribute(ClassAttributeName, Class.ToStackString());
			}

			yield return new XAttribute(HrefAttributeName, Href);

			if(Content != null)
			{
				yield return Content;
			}
		}
	}
}