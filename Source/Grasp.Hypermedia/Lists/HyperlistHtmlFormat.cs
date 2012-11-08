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

			content.Insert(0, MLink.WithClassIfTemplate("grasp:list-template", list.PageLink));

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
				yield return MLink.WithClassIfTemplate("grasp:list-item", item.Link);

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
			return body.Items.ReadLink(new Relationship("grasp:list-template")).Hyperlink;
		}

		private static ListPageKey ReadQuery(MCompositeContent body)
		{
			var query = body.Items.ReadContent<MDivision>("query");

			return new ListPageKey(
				new Number(query.Children.ReadValue<int>("page")),
				new Count(query.Children.ReadValue<int>("page-size")),
				Sort.Parse(query.Children.ReadValue<string>("sort")));
		}

		private static ListPageContext ReadContext(MCompositeContent body, ListPageKey pageKey)
		{
			var context = body.Items.ReadContent<MDivision>("context");

			return new ListPageContext(
				pageKey,
				new Count(context.Children.ReadValue<int>("page-count")),
				new Count(context.Children.ReadValue<int>("item-count")));
		}

		private static HyperlistPage ReadPage(MCompositeContent body)
		{
			var page = body.Items.ReadContent<MDivision>("page");

			var schema = ReadSchema(page);

			return new HyperlistPage(
				new Number(page.Children.ReadValue<int>("number")),
				new Count(page.Children.ReadValue<int>("size")),
				new Number(page.Children.ReadValue<int>("first-item")),
				new Number(page.Children.ReadValue<int>("last-item")),
				new HyperlistItems(schema, ReadItems(schema, page)));
		}

		private static ListSchema ReadSchema(MDivision page)
		{
			var schema = page.Children.ReadContent<MDefinitionList>("schema");

			return new ListSchema(schema.Definitions.Select(definition => new KeyValuePair<string, Type>(definition.Key.ReadValue<string>(), ReadType(definition.Value))));
		}

		private static Type ReadType(MContent definitionValue)
		{
			var typeValue = definitionValue as MValue;

			if(typeValue == null)
			{
				throw new FormatException(Resources.ExpectingValue.FormatInvariant(typeof(MValue), definitionValue.GetType()));
			}

			var type = typeValue.ReadValue<string>();

			if(IsType(type, "string"))
			{
				return typeof(string);
			}
			else if(IsType(type, "char"))
			{
				return typeof(char);
			}
			else if(IsType(type, "int"))
			{
				return typeof(int);
			}
			else if(IsType(type, "double"))
			{
				return typeof(double);
			}
			else if(IsType(type, "decimal"))
			{
				return typeof(decimal);
			}
			else if(IsType(type, "bool"))
			{
				return typeof(bool);
			}
			else if(IsType(type, "datetime"))
			{
				return typeof(DateTime);
			}
			else
			{
				return typeof(object);
			}
		}

		private static bool IsType(string type, string expectedType)
		{
			return type.Equals(expectedType, StringComparison.InvariantCultureIgnoreCase);
		}

		private static IEnumerable<HyperlistItem> ReadItems(ListSchema schema, MDivision page)
		{
			return page.Children.Select(item => ReadItem(schema, item));
		}

		private static HyperlistItem ReadItem(ListSchema schema, MContent content)
		{
			var compositeContent = content as MCompositeContent;

			if(compositeContent == null)
			{
				throw new FormatException(Resources.ExpectingCompositeListItemContent);
			}

			var link = compositeContent.Items.ReadLink(new Relationship("grasp:list-item"));

			var listItem = new ListItem(
				new Number(Convert.ToInt32(link.Hyperlink.Content)),
				new ListItemBindings(ReadItemBindings(schema, compositeContent)));

			return new HyperlistItem(link.Hyperlink, listItem);
		}

		private static IEnumerable<KeyValuePair<string, object>> ReadItemBindings(ListSchema schema, MCompositeContent content)
		{
			return content.Items.OfType<MValue>().Select(value => new KeyValuePair<string, object>(value.Class, ReadItemValue(schema, value)));
		}

		private static object ReadItemValue(ListSchema schema, MValue value)
		{
			var type = schema[value.Class];

			if(type == typeof(string))
			{
				return Convert.ToString(value.Object);
			}
			else if(type == typeof(char))
			{
				return Convert.ToChar(value.Object);
			}
			else if(type == typeof(int))
			{
				return Convert.ToInt32(value.Object);
			}
			else if(type == typeof(double))
			{
				return Convert.ToDouble(value.Object);
			}
			else if(type == typeof(decimal))
			{
				return Convert.ToDecimal(value.Object);
			}
			else if(type == typeof(bool))
			{
				return Convert.ToBoolean(value.Object);
			}
			else if(type == typeof(DateTime))
			{
				return Convert.ToDateTime(value.Object);
			}
			else
			{
				return value.Object;
			}
		}
		#endregion
	}
}