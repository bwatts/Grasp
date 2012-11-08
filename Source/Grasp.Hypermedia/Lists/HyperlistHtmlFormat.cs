using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Cloak.Http.Media;
using Grasp.Hypermedia.Linq;
using Grasp.Lists;

namespace Grasp.Hypermedia.Lists
{
	public class HyperlistHtmlFormat : HtmlFormat<Hyperlist>
	{
		public static readonly MediaType MediaType = new MediaType("application/vnd.grasp.list+html");

		public HyperlistHtmlFormat() : base(MediaType)
		{}

		protected override MRepresentation ConvertToRepresentation(Hyperlist media)
		{
			return new MRepresentation(GetHeader(media), GetBody(media));
		}

		protected override Hyperlist ConvertFromRepresentation(MRepresentation representation, IFormatterLogger formatterLogger)
		{
			var body = representation.Body as MCompositeContent;

			if(body == null)
			{
				throw new FormatException(Resources.ExpectingCompositeBodyContent);
			}

			var query = ReadQuery(body);
			var page = ReadPage(body);

			var pageKey = new ListPageKey(page.Number, page.Size, query.Sort);

			return new Hyperlist(ReadHeader(representation.Head), ReadPageLink(body), query, ReadContext(body, pageKey), page);
		}

		#region Serialization

		private static MHead GetHeader(Hyperlist list)
		{
			return new MHead(list.Header.Title, list.Header.BaseLink, list.Header.Links);
		}

		private static MContent GetBody(Hyperlist list)
		{
			var content = GetBodyContent(list).ToList();

			content.Insert(0, MLink.WithClassIfTemplate("href-template", list.PageLink));

			return new MCompositeContent(content);
		}

		private static IEnumerable<MContent> GetBodyContent(Hyperlist list)
		{
			yield return new MDivision(
				"query",
				new MValue("page", list.Query.Number),
				new MValue("page-size", list.Query.Size),
				new MValue("sort", list.Query.Sort));

			yield return new MDivision(
				"context",
				new MValue("page-count", list.Context.PageCount),
				new MValue("item-count", list.Context.ItemCount),
				new MValue("previous-page", list.Context.PreviousPage),
				new MValue("next-page", list.Context.NextPage));

			yield return new MDivision(
				"page",
				new MValue("number", list.Page.Number),
				new MValue("size", list.Page.Size),
				new MValue("first-item", list.Page.FirstItemNumber),
				new MValue("last-item", list.Page.LastItemNumber),
				new MDivision("items", GetItemSchema(list), GetItems(list)));
		}

		private static MDefinitionList GetItemSchema(Hyperlist list)
		{
			return new MDefinitionList(
				"schema",
				list.Page.Items.Schema.ToDictionary(
					field => new MValue(field.Key),
					field => new MValue(GetTypeString(field.Value)) as MContent));
		}

		private static MList GetItems(Hyperlist list)
		{
			return new MList(MClass.Empty, GetItemsContent(list));
		}

		private static IEnumerable<MContent> GetItemsContent(Hyperlist list)
		{
			foreach(var item in list.Page.Items)
			{
				yield return MLink.WithClassIfTemplate("href-template", item.Link);

				foreach(var itemValue in item.ListItem.Bindings)
				{
					yield return new MValue(itemValue.Key, itemValue.Value);
				}
			}
		}

		private static string GetTypeString(Type type)
		{
			// TODO: Does this need to be more complex? Custom types?
			//
			// This could evolve to something nice with forms.

			if(type == typeof(string))
			{
				return "string";
			}
			else if(type == typeof(char))
			{
				return "char";
			}
			else if(type == typeof(int))
			{
				return "int";
			}
			else if(type == typeof(double))
			{
				return "double";
			}
			else if(type == typeof(decimal))
			{
				return "decimal";
			}
			else if(type == typeof(bool))
			{
				return "bool";
			}
			else if(type == typeof(DateTime))
			{
				return "datetime";
			}
			else
			{
				return "object";
			}
		}
		#endregion

		#region Deserialization

		private static HttpResourceHeader ReadHeader(MHead head)
		{
			return new HttpResourceHeader(head.Title, head.BaseLink, head.Links);
		}

		private static Hyperlink ReadPageLink(MCompositeContent body)
		{
			var listTemplateRelationship = new Relationship("grasp:list-template");

			return body.Items.OfType<MLink>().First(link => link.Hyperlink.Relationship == listTemplateRelationship).Hyperlink;
		}

		private static ListPageKey ReadQuery(MCompositeContent body)
		{
			var query = ReadBodyDivision(body, "query");

			var pageClass = new MClass("page");
			var pageSizeClass = new MClass("page-size");
			var sortClass = new MClass("sort");

			return new ListPageKey(
				new Number(query.Children.OfType<MValue>().First(value => value.Class == pageClass)),
				new Count(query.Children.OfType<MValue>().First(value => value.Class == pageSizeClass)),
				Sort.Parse(query.Children.OfType<MValue>().First(value => value.Class == sortClass)));
		}

		private static ListPageContext ReadContext(MCompositeContent body, ListPageKey pageKey)
		{
			var context = ReadBodyDivision(body, "context");

			return new ListPageContext(pageKey, new Count(ReadValue(context, "page-count")), new Count(ReadValue(context, "item-count")));
		}

		private static HyperlistPage ReadPage(MCompositeContent body)
		{
			var page = ReadBodyDivision(body, "page");

			return new HyperlistPage(
				new Number(ReadValue(page, "number")),
				new Count(ReadValue(page, "size")),
				new Number(ReadValue(page, "first-item")),
				new Number(ReadValue(page, "last-item")),
				new HyperlistItems(ReadSchema(page), ReadItems(page)));
		}




		private static ListSchema ReadSchema(MDivision page)
		{
			var schemaClass = new MClass("schema");

			var schema = page.Children.OfType<MDefinitionList>().First(definitionList => definitionList.Class == schemaClass);

			return new ListSchema(schema.Definitions.Select(definition => new KeyValuePair<string, Type>((string) definition.Key.Object, ReadType(definition.Value))));
		}

		private static Type ReadType(MContent definition)
		{

		}

		private static IEnumerable<HyperlistItem> ReadItems(MDivision page)
		{

		}





		private static MDivision ReadBodyDivision(MCompositeContent body, MClass @class)
		{
			return body.Items.OfType<MDivision>().First(division => division.Class == queryClass);
		}

		private static int ReadValue(MDivision division, MClass @class)
		{
			return division.Children.OfType<MValue>().First(value => value.Class == @class);
		}
		#endregion
	}
}