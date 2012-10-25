using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp
{
	public static class ItemStack
	{
		public static readonly Field<Notion> ParentField = Field.AttachedTo<Notion>.By.Static(typeof(ItemStack)).For(() => ParentField);

		public static T GetParent<T>(this IStackable<T> item) where T : Notion, IStackable<T>
		{
			Contract.Requires(item != null);

			return (T) ParentField.Get((Notion) item);
		}

		public static bool TryGetParent<T>(this IStackable<T> item, out T parent) where T : Notion, IStackable<T>
		{
			Contract.Requires(item != null);

			Notion untypedParent;

			var hasParent = ParentField.TryGet((Notion) item, out untypedParent);

			parent = hasParent ? (T) untypedParent : default(T);

			return hasParent;
		}

		public static void Append<T>(this IStackable<T> item, T nextItem) where T : Notion, IStackable<T>
		{
			Contract.Requires(item != null);
			Contract.Requires(nextItem != null);
			Contract.Requires(item != nextItem);

			ParentField.Set(nextItem, (T) item);
		}

		public static IEnumerable<T> ItemsFromHere<T>(this IStackable<T> item) where T : Notion, IStackable<T>
		{
			yield return (T) item;

			T parent;

			while(item.TryGetParent(out parent))
			{
				yield return parent;

				item = (T) parent;
			}
		}

		public static IEnumerable<T> ItemsFromRoot<T>(this IStackable<T> item) where T : Notion, IStackable<T>
		{
			return item.ItemsFromHere().Reverse();
		}

		public static string ToStackString<T>(this IStackable<T> item) where T : Notion, IStackable<T>
		{
			return item.ToStackString(" ");
		}

		public static string ToStackString<T>(this IStackable<T> item, string separator) where T : Notion, IStackable<T>
		{
			return item.ToStackString(separator, ancestor => ancestor.ToString());
		}

		public static string ToStackString<T>(this IStackable<T> item, Func<T, string> itemTextSelector) where T : Notion, IStackable<T>
		{
			return item.ToStackString(" ", itemTextSelector);
		}

		public static string ToStackString<T>(this IStackable<T> item, string separator, Func<T, string> itemTextSelector) where T : Notion, IStackable<T>
		{
			return String.Join(separator, item.ItemsFromRoot().Select(itemTextSelector));
		}
	}
}