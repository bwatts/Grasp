using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Grasp.Hypermedia;

namespace Slate.Web.Site.Presentation.Work
{
	public sealed class WorkMesh : IWorkMesh
	{
		// TODO: This is a great place to use UriTemplate.Match

		public Uri GetItemUri(WorkItemResource apiItem)
		{
			var uri = apiItem.Header.SelfLink.ToUri();

			var parts = SplitSelfLinkPathAndQuery(uri);

			if(parts.Length != 2 || parts[0] != "work")
			{
				throw new NotSupportedException("Unsupported work item link: " + uri.ToString()); 
			}

			return new Uri("~/work/" + parts[1], UriKind.Relative);
		}

		public Uri GetResultUrl(WorkItemResource apiItem)
		{
			var resultUri = apiItem.ResultLink.ToUri();

			var parts = SplitSelfLinkPathAndQuery(resultUri);

			if(parts.Length != 2 || parts[0] != "forms")
			{
				throw new NotSupportedException("Unsupported result link: " + resultUri.ToString()); 
			}

			return new Uri("~/explore/forms/" + parts[1], UriKind.Relative);
		}

		private static string[] SplitSelfLinkPathAndQuery(Uri selfUri)
		{
			return (selfUri.IsAbsoluteUri ? selfUri.PathAndQuery : selfUri.ToString()).Split('/');
		}
	}
}