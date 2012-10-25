using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using Cloak.Http.Media;
using Grasp.Hypermedia.Linq;

namespace Grasp.Hypermedia.Lists
{
	public class ListHtmlFormat : HtmlFormat<ListResource>
	{
		public static readonly MediaType MediaType = new MediaType("application/vnd.grasp.list+html");

		public ListHtmlFormat() : base(MediaType)
		{}

		protected override MRepresentation ConvertToRepresentation(ListResource media)
		{
			return new MRepresentation(GetHeader(media), GetBody(media));
		}

		protected override ListResource ConvertFromRepresentation(MRepresentation representation, IFormatterLogger formatterLogger)
		{
			throw new NotImplementedException();
		}

		#region Serialization

		private static MHead GetHeader(ListResource list)
		{
			return new MHead(list.Header.Title, list.Header.BaseLink, list.Header.Links);
		}

		private static MContent GetBody(ListResource list)
		{
			var content = GetBodyContent(list).ToList();

			var link = Mesh.LinkField.Get(list);

			if(link != null)
			{
				content.Insert(0, MLink.WithClassIfTemplate("href-template", link));
			}

			return new MCompositeContent(content);
		}

		private static IEnumerable<MContent> GetBodyContent(ListResource list)
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
				new MValue("first-item", list.Page.FirstItem),
				new MValue("last-item", list.Page.LastItem),
				new MDivision("items", GetItemSchema(list), GetItems(list)));
		}

		private static MDefinitionList GetItemSchema(ListResource list)
		{
			return new MDefinitionList(
				"schema",
				list.Page.Items.Schema.ToDictionary(
					field => new MValue(field.Key),
					field => new MValue(GetTypeString(field.Value)) as MContent));
		}

		private static MList GetItems(ListResource list)
		{
			return new MList(MClass.Empty, GetItemsContent(list));
		}

		private static IEnumerable<MContent> GetItemsContent(ListResource list)
		{
			foreach(var item in list.Page.Items)
			{
				var link = Mesh.LinkField.Get(item);

				if(link != null)
				{
					yield return MLink.WithClassIfTemplate("href-template", link);
				}

				foreach(var itemValue in item.Values)
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
	}
}