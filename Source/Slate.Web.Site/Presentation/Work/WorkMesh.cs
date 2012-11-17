using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Grasp.Hypermedia;

namespace Slate.Web.Site.Presentation.Work
{
	public sealed class WorkMesh : IWorkMesh
	{
		public Uri GetResultUrl(WorkItemResource item)
		{
			// TODO: This is a great place to use UriTemplate.Match

			var resultUri = item.ResultLink.ToUri();

			var parts = resultUri.PathAndQuery.Split('/');

			if(parts.Length == 2 && parts[0] == "forms")
			{
				return new Uri("explore/forms/" + parts[1]);
			}

			throw new NotSupportedException("Unsupported result link: " + resultUri.ToString());
		}
	}
}