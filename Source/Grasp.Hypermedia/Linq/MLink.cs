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

		public MLink(Hyperlink value) : base(value.Class)
		{
			Contract.Requires(value != null);

			Hyperlink = value;
		}

		public Hyperlink Hyperlink { get { return GetValue(HyperlinkField); } private set { SetValue(HyperlinkField, value); } }

		internal override object GetHtml()
		{
			// Hyperlink natively supports the class attribute - we don't need to handle the class stack here.
			//
			// MLink is just an MContent host for a hyperlink.

			return Hyperlink.ToHtml("a");
		}

		protected override object GetHtmlWithoutClass()
		{
			throw new NotImplementedException();
		}

		protected override object GetHtmlWithClass(string classStack)
		{
			throw new NotImplementedException();
		}
	}
}