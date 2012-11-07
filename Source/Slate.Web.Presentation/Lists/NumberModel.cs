using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Grasp;
using Grasp.Hypermedia;
using Grasp.Lists;

namespace Slate.Web.Presentation.Lists
{
	public class NumberModel : ViewModel
	{
		public static readonly Field<Number> ValueField = Field.On<NumberModel>.For(x => x.Value);
		public static readonly Field<Hyperlink> LinkField = Field.On<NumberModel>.For(x => x.Link);

		public NumberModel(Number value = default(Number), Hyperlink link = null)
		{
			Value = value;
			Link = link ?? Hyperlink.Empty;
		}

		public NumberModel(Count value = default(Count), Hyperlink link = null) : this(new Number(value.Value), link)
		{}

		public Number Value { get { return GetValue(ValueField); } private set { SetValue(ValueField, value); } }
		public Hyperlink Link { get { return GetValue(LinkField); } private set { SetValue(LinkField, value); } }
	}
}