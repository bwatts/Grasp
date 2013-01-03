using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Hypermedia;
using Grasp.Hypermedia.Linq;

namespace Dash.Http
{
	public class ApplicationResource : HttpResource
	{
		public ApplicationResource(MHeader header) : base(header)
		{

		}
	}
}