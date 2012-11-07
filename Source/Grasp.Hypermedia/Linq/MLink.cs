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
		public static readonly Field<Hyperlink> ValueField = Field.On<MLink>.For(x => x.Value);

		public static MLink WithClassIfTemplate(MClass templateClass, Hyperlink link)
		{
			Contract.Requires(link != null);
			Contract.Requires(templateClass != null);

			return link.IsTemplate ? new MLink(link, templateClass) : new MLink(link);
		}

		public MLink(Hyperlink value, MClass @class) : base(@class)
		{
			Contract.Requires(value != null);

			Value = value;
		}

		public MLink(Hyperlink value) : this(value, MClass.Empty)
		{}

		public Hyperlink Value { get { return GetValue(ValueField); } private set { SetValue(ValueField, value); } }

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