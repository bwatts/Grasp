using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Cloak.Http;
using Grasp.Knowledge;

namespace Grasp.Hypermedia
{
	public abstract class MContent : Notion
	{
		public static readonly Field<ItemStack<MClass>> ClassField = Field.On<MContent>.Backing(x => x.Class);

		protected MContent(MClass @class)
		{
			Contract.Requires(@class != null);

			Class = new ItemStack<MClass>(@class);
		}

		public ItemStack<MClass> Class { get { return GetValue(ClassField); } private set { SetValue(ClassField, value); } }

		internal object GetHtmlContent()
		{
			return HasClass() ? GetHtmlContentWithClass(Class.ToString()) : GetHtmlContentWithoutClass();
		}

		private bool HasClass()
		{
			return Class.GetAllItemsFromHere().Any(item => !item.IsNone);
		}

		protected abstract object GetHtmlContentWithoutClass();

		protected abstract object GetHtmlContentWithClass(string classStack);
	}
}