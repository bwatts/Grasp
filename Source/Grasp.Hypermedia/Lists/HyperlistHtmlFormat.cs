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
			yield return new MSection("query", GetListLink(resource), GetQueryDescription(resource.Query));
			yield return new MSection("pages", GetPagesValues(resource));
			yield return new MSection("items", GetTotal(resource), GetSchema(resource), GetItemList(resource));
		}

		protected override Hyperlist ConvertToResource(MHeader header, MCompositeContent body)
		{
			return new Hyperlist(header, ReadQuery(body), ReadPages(body), ReadItems(body));
		}

		#region From Resource

		private static MLink GetListLink(Hyperlist list)
		{
			return new MLink(list.Query.ListLink.Override(relationship: "grasp:list"));
		}

		private static MDescriptionList GetQueryDescription(HyperlistQuery query)
		{
			return new MDescriptionList(
				new MDescriptionItem(new MValue("start-parameter", query.StartParameter), new MValue(query.Key.Start)),
				new MDescriptionItem(new MValue("size-parameter", query.SizeParameter), new MValue(query.Key.Size)),
				new MDescriptionItem(new MValue("sort-parameter", query.SortParameter), new MValue(query.Key.Sort)));
		}

		private static IEnumerable<MValue> GetPagesValues(Hyperlist list)
		{
			yield return new MValue("count", list.Pages.Count);
			yield return new MValue("current", list.Pages.Current);
			yield return new MValue("previous", list.Pages.Previous);
			yield return new MValue("next", list.Pages.Next);
		}

		private static MValue GetTotal(Hyperlist list)
		{
			return new MValue("total", list.Items.Total);
		}

		private static MDescriptionList GetSchema(Hyperlist list)
		{
			return new MDescriptionList(
				from item in list.Items.Schema
				select new MDescriptionItem(
					new MValue(item.Key),
					new MValue(GetTypeString(item.Value))));
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

		private static HyperlistQuery ReadQuery(MCompositeContent body)
		{
			var query = body.Items.ReadContent<MSection>("query");

			var description = query.Items.ReadContent<MDescriptionList>();

			var startItem = description.Items.First(item => item.Term.Class == "start-parameter");
			var sizeItem = description.Items.First(item => item.Term.Class == "size-parameter");
			var sortItem = description.Items.First(item => item.Term.Class == "sort-parameter");

			return new HyperlistQuery(
				query.Items.ReadLink("grasp:list").Hyperlink,
				new ListViewKey(
					new Count(startItem.Description.Read<int>()),
					new Count(sizeItem.Description.Read<int>()),
					Sort.Parse(sortItem.Description.Read<string>())),
				startItem.Term.Read<string>(),
				sizeItem.Term.Read<string>(),
				sortItem.Term.Read<string>());
		}

		private static ListViewPages ReadPages(MCompositeContent body)
		{
			var section = body.Items.ReadContent<MSection>("pages");

			var pages = NotionActivator.GetUninitialized<ListViewPages>();

			ListViewPages.CountField.Set(pages, new Count(section.Items.ReadValue<int>("count")));
			ListViewPages.CurrentField.Set(pages, new Count(section.Items.ReadValue<int>("current")));
			ListViewPages.PreviousField.Set(pages, new Count(section.Items.ReadValue<int>("previous")));
			ListViewPages.NextField.Set(pages, new Count(section.Items.ReadValue<int>("next")));

			return pages;
		}

		private static HyperlistItems ReadItems(MCompositeContent body)
		{
			var items = body.Items.ReadContent<MSection>("items");

			var schema = ReadSchema(items);

			return new HyperlistItems(
				new Count(items.Items.ReadValue<int>("total")),
				ReadSchema(items),
				ReadItems(schema, items));
		}

		private static ListSchema ReadSchema(MSection section)
		{
			var schema = section.Items.ReadContent<MDescriptionList>();

			return new ListSchema(
				from item in schema.Items
				select new KeyValuePair<string, Type>(item.Term.Read<string>(), ReadType(item.Description)));
		}

		private static Type ReadType(MContent description)
		{
			var typeValue = description as MValue;

			if(typeValue == null)
			{
				throw new FormatException(Resources.ExpectingValue.FormatInvariant(typeof(MValue), description.GetType()));
			}

			var type = typeValue.Read<string>();

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

		private static IEnumerable<HyperlistItem> ReadItems(ListSchema schema, MSection section)
		{
			var list = section.Items.ReadContent<MList>(MClass.Empty);

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

			return ChangeType.To(type, value.Object);
		}
		#endregion
	}
}