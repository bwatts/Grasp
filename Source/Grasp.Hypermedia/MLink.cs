using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Grasp.Knowledge;

namespace Grasp.Hypermedia
{
	public sealed class MLink : MContent
	{
		public static readonly Field<HtmlLink> ValueField = Field.On<MLink>.Backing(x => x.Value);

		public MLink(HtmlLink value, MClass @class) : base(@class)
		{
			Contract.Requires(value != null);

			Value = value;
		}

		public MLink(HtmlLink value) : this(value, MClass.None)
		{}

		public HtmlLink Value { get { return GetValue(ValueField); } private set { SetValue(ValueField, value); } }

		protected override object GetHtmlContentWithoutClass()
		{
			return Value.ToHtml("a");
		}

		protected override object GetHtmlContentWithClass(string classStack)
		{
			var html = Value.ToHtml("a");

			html.Add(new XAttribute("class", classStack));

			return html;
		}
	}
}