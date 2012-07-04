using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grasp.Hypermedia.Http.Mesh
{
	public interface IHttpMesh
	{
		HttpLink GetHttpLink(Link link);
	}
}