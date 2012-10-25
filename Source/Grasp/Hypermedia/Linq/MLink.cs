using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Grasp.Hypermedia.Linq
{
	public sealed class MLink : MContent
	{
		public static readonly Field<HtmlLink> ValueField = Field.On<MLink>.For(x => x.Value);

		public static MLink WithClassIfTemplate(MClass templateClass, HtmlLink link)
		{
			Contract.Requires(link != null);
			Contract.Requires(templateClass != null);

			return link.IsTemplate ? new MLink(link, templateClass) : new MLink(link);
		}

		public MLink(HtmlLink value, MClass @class) : base(@class)
		{
			Contract.Requires(value != null);

			Value = value;
		}

		public MLink(HtmlLink value) : this(value, MClass.Empty)
		{}

		public HtmlLink Value { get { return GetValue(ValueField); } private set { SetValue(ValueField, value); } }

		protected override object GetHtmlWithoutClass()
		{
			return Value.ToHtml("a");
		}

		protected override object GetHtmlWithClass(string classStack)
		{
			var html = Value.ToHtml("a");

			html.Add(new XAttribute("class", classStack));

			return html;
		}
	}
}