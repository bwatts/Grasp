using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Http;
using Grasp.Hypermedia;

namespace Slate.Http.Api
{
	public class HomeController : ApiController
	{
		private readonly IHttpResourceContext _resourceContext;

		public HomeController(IHttpResourceContext resourceContext)
		{
			Contract.Requires(resourceContext != null);

			_resourceContext = resourceContext;
		}

		[HttpGet]
		public ApiResource GetApi()
		{
			var x = _resourceContext.CreateHeader("Slate Hypermedia API", "/");

			return new ApiResource(
				_resourceContext.CreateHeader("Slate Hypermedia API", "/"),
				new Hyperlink("forms", content: "Forms", title: "The set of available forms", relationship: "grasp:entity-set", @class: "forms"),
				new Hyperlink("forms/{id}", content: "Form {id}", title: "The form with the specified identifier", relationship: "grasp:entity", @class: "form"),
				new Hyperlink("work/{id}", content: "Work item {id}", title: "The work item with the specified identifier", relationship: "grasp:entity", @class: "work"));
		}
	}
}