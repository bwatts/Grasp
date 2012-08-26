using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Grasp.Hypermedia.Api
{
	public class HttpApi : HypermediaApi
	{
		public static readonly Field<Many<MediaType>> MediaTypesField = Field.On<HttpApi>.Backing(x => x.MediaTypes);

		public Many<MediaType> MediaTypes { get { return GetValue(MediaTypesField); } }
	}
}