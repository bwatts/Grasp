using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Hypermedia.Linq;

namespace Grasp.Hypermedia
{
	public class ApiResource : HttpResource
	{
		public static readonly Field<Many<Hyperlink>> LinksField = Field.On<ApiResource>.For(x => x.Links);

		public ApiResource(MHeader header, IEnumerable<Hyperlink> links) : base(header)
		{
			Contract.Requires(links != null);

			Links = links.ToMany();
		}

		public ApiResource(MHeader header, params Hyperlink[] links) : this(header, links as IEnumerable<Hyperlink>)
		{}

		public Many<Hyperlink> Links { get { return GetValue(LinksField); } private set { SetValue(LinksField, value); } }
	}
}