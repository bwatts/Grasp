using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Autofac;
using Autofac.Builder;
using Autofac.Features.Scanning;
using Grasp.Hypermedia.Http.Mesh;

namespace Grasp.Hypermedia.Http.Composition
{
	public static class ResourceRegistration
	{
		public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> RegisterHttpResources(this ContainerBuilder builder, params Assembly[] assemblies)
		{
			return builder.RegisterAssemblyTypes(assemblies).Where(type => typeof(HttpResource).IsAssignableFrom(type) && type.Name.EndsWith("Resource"));
		}
	}
}