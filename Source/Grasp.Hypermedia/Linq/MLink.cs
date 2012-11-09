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
		public static readonly Field<Hyperlink> HyperlinkField = Field.On<MLink>.For(x => x.Hyperlink);

		public MLink(Hyperlink value, MClass @class) : base(@class)
		{
			Contract.Requires(value != null);

			Hyperlink = value;
		}

		public MLink(Hyperlink value) : this(value, MClass.Empty)
		{}

		public Hyperlink Hyperlink { get { return GetValue(HyperlinkField); } private set { SetValue(HyperlinkField, value); } }

		protected override object GetHtmlWithoutClass()
		{
			return Hyperlink.ToHtml("a");
		}

		protected override object GetHtmlWithClass(string classStack)
		{
			var html = Hyperlink.ToHtml("a");

			html.Add(new XAttribute("class", classStack));

			return html;
		}
	}
}