using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grasp.Hypermedia.Apis
{
	public interface IHttpMesh
	{
		HtmlLink GetHtmlLink(Link link);
	}
}