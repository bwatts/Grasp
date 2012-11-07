using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Cloak;

namespace Grasp.Hypermedia.Linq
{
	public abstract class MContent : Notion
	{
		protected MContent(MClass @class)
		{
			Contract.Requires(@class != null);

			MClass.ValueField.Set(this, @class);
		}

		internal object GetHtml()
		{
			var @class = GetValue(MClass.ValueField);

			return @class.ItemsFromHere().Any(item => !item.IsEmpty)
				? GetHtmlWithClass(@class.ToString())
				: GetHtmlWithoutClass();
		}

		protected abstract object GetHtmlWithoutClass();

		protected abstract object GetHtmlWithClass(string classStack);
	}
}