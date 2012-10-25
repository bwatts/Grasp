using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Grasp;
using Grasp.Hypermedia;
using Grasp.Hypermedia.Lists;

namespace Slate.Web.Presentation.Lists
{
	public class NumberModel : ViewModel
	{
		public static readonly Field<Number> ValueField = Field.On<NumberModel>.For(x => x.Value);
		public static readonly Field<HtmlLink> LinkField = Field.On<NumberModel>.For(x => x.Link);

		public NumberModel(Number value = default(Number), HtmlLink link = null)
		{
			Value = value;
			Link = link ?? HtmlLink.Empty;
		}

		public NumberModel(Count value = default(Count), HtmlLink link = null) : this(new Number(value.Value), link)
		{}

		public Number Value { get { return GetValue(ValueField); } private set { SetValue(ValueField, value); } }
		public HtmlLink Link { get { return GetValue(LinkField); } private set { SetValue(LinkField, value); } }
	}
}