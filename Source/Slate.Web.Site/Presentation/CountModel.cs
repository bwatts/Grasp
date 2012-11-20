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

namespace Slate.Web.Site.Presentation
{
	public class CountModel : ViewModel
	{
		public static readonly Field<Count> ValueField = Field.On<CountModel>.For(x => x.Value);
		public static readonly Field<Hyperlink> LinkField = Field.On<CountModel>.For(x => x.Link);

		public CountModel(Count value = default(Count), Hyperlink link = null)
		{
			Value = value;
			Link = link ?? Hyperlink.Empty;
		}

		public Count Value { get { return GetValue(ValueField); } private set { SetValue(ValueField, value); } }
		public Hyperlink Link { get { return GetValue(LinkField); } private set { SetValue(LinkField, value); } }
	}
}