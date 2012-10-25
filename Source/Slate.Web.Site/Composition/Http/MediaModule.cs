using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Cloak.Autofac;
using Cloak.Http.Media;

namespace Slate.Web.Site.Composition.Http
{
	public class MediaModule : BuilderModule
	{
		public MediaModule()
		{
			Register(c => new MediaFormats(c.Resolve<IEnumerable<MediaFormat>>().ToList())).SingleInstance();
		}
	}
}