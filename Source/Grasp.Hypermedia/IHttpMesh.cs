using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grasp.Hypermedia
{
	public interface IHttpMesh
	{
		HttpLink GetHttpLink(Link link);
	}
}