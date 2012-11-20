using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Cloak.Http.Media;
using Grasp.Checks;
using Grasp.Hypermedia.Linq;
using Grasp.Lists;
using Grasp.Work.Items;
using Grasp.Work.Persistence;

namespace Grasp.Hypermedia.Lists
{
	public class HyperlistHtmlFormat : HtmlFormat<Hyperlist>
	{
		public static readonly MediaType MediaType = new MediaType("application/vnd.grasp.list+html");

		public HyperlistHtmlFormat() : base(MediaType)
		{}

		protected override MClass MediaTypeClass
		{
			get { return "list"; }
		}

		protected override IEnumerable<MContent> ConvertFromResource(Hyperlist resource)
		{
			var content = GetBodyContent(resource).ToList();

			content.Insert(0, new MLink(resource.PageLink.Override(relationship: "grasp:list-template")));

			return content;
		}

		protected override Hyperlist ConvertToResource(MHeader header, MCompositeContent body)
		{
			return new Hyperlist(header, ReadPageLink(body), ReadQuery(body), ReadPages(body), ReadItems(body));
		}

		#region From Resource

		private static IEnumerable<MContent> GetBodyContent(Hyperlist list)
		{
			yield return new MDivision(
				"query",
				new MValue("start", list.Query.Start),
				new MValue("size", list.Query.Size),
				new MValue("sort", list.Query.Sort));

			yield return new MDivision(
				"pages",
				new MValue("count", list.Pages.Count),
				new MValue("current", list.Pages.Current),
				new MValue("previous", list.Pages.Previous),
				new MValue("next", list.Pages.Next));

			yield return new MDivision("items", GetTotal(list), GetSchema(list), GetItemList(list));
		}

		private static MValue GetTotal(Hyperlist list)
		{
			return new MValue("total", list.Items.Total);
		}

		private static MDefinitionList GetSchema(Hyperlist list)
		{
			return new MDefinitionList(
				"schema",
				list.Items.Schema.ToDictionary(
					field => new MValue(field.Key),
					field => new MValue(GetTypeString(field.Value)) as MContent));
		}

		private static MList GetItemList(Hyperlist list)
		{
			return new MList(MClass.Empty, GetItemsContent(list));
		}

		private static IEnumerable<MContent> GetItemsContent(Hyperlist list)
		{
			return
				from item in list.Items
				let link = new MLink(item.Link.Override(relationship: "grasp:list-item"))
				let bindings = item.ListItem.Bindings.Select(binding => new MValue(binding.Key, binding.Value))
				select new MCompositeContent(new MContent[] { link }.Concat(bindings));
		}

		private static string GetTypeString(Type type)
		{
			// TODO: Does this need to be more robust? Custom types?
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
			else if(type == typeof(Guid))
			{
				return "guid";
			}
			else if(type == typeof(DateTime))
			{
				return "datetime";
			}
			else if(type == typeof(EntityId))
			{
				return "id";
			}
			else
			{
				return "object";
			}
		}
		#endregion

		#region To Resource

		private static Hyperlink ReadPageLink(MCompositeContent body)
		{
			return body.Items.ReadLink("grasp:list-template").Hyperlink;
		}

		private static ListViewKey ReadQuery(MCompositeContent body)
		{
			var query = body.Items.ReadContent<MDivision>("query");

			return new ListViewKey(
				new Count(query.Children.ReadValue<int>("start")),
				new Count(query.Children.ReadValue<int>("size")),
				Sort.Parse(query.Children.ReadValue<string>("sort")));
		}

		private static ListViewPages ReadPages(MCompositeContent body)
		{
			var pagesDiv = body.Items.ReadContent<MDivision>("pages");

			var pages = NotionActivator.GetUninitialized<ListViewPages>();

			ListViewPages.CountField.Set(pages, new Count(pagesDiv.Children.ReadValue<int>("count")));
			ListViewPages.CurrentField.Set(pages, new Count(pagesDiv.Children.ReadValue<int>("current")));
			ListViewPages.PreviousField.Set(pages, new Count(pagesDiv.Children.ReadValue<int>("previous")));
			ListViewPages.NextField.Set(pages, new Count(pagesDiv.Children.ReadValue<int>("next")));

			return pages;
		}

		private static HyperlistItems ReadItems(MCompositeContent body)
		{
			var items = body.Items.ReadContent<MDivision>("items");

			var schema = ReadSchema(items);

			return new HyperlistItems(
				new Count(items.Children.ReadValue<int>("total")),
				ReadSchema(items),
				ReadItems(schema, items));
		}

		private static ListSchema ReadSchema(MDivision items)
		{
			var schema = items.Children.ReadContent<MDefinitionList>("schema");

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
			else if(IsType(type, "guid"))
			{
				return typeof(Guid);
			}
			else if(IsType(type, "datetime"))
			{
				return typeof(DateTime);
			}
			else if(IsType(type, "id"))
			{
				return typeof(EntityId);
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

		private static IEnumerable<HyperlistItem> ReadItems(ListSchema schema, MDivision items)
		{
			var list = items.Children.ReadContent<MList>(MClass.Empty);

			return list.Items.Select(item => ReadItem(schema, item));
		}

		private static HyperlistItem ReadItem(ListSchema schema, MContent item)
		{
			var content = item as MCompositeContent;

			if(content == null)
			{
				throw new FormatException(Resources.ExpectingCompositeListItemContent);
			}

			var link = content.Items.ReadLink("grasp:list-item");

			var listItem = new ListItem(
				new Count(Convert.ToInt32(link.Hyperlink.Content)),
				new ListItemBindings(ReadItemBindings(schema, content.Items.OfType<MValue>())));

			return new HyperlistItem(link.Hyperlink, listItem);
		}

		private static IEnumerable<KeyValuePair<string, object>> ReadItemBindings(ListSchema schema, IEnumerable<MValue> values)
		{
			return values.Select(value => new KeyValuePair<string, object>(value.Class, ReadItemValue(schema, value)));
		}

		private static object ReadItemValue(ListSchema schema, MValue value)
		{
			var type = schema[value.Class];

			return Conversion.To(type, value.Object);
		}
		#endregion
	}
}