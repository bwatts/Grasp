using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Autofac;
using Autofac.Builder;
using Autofac.Features.Scanning;

namespace Grasp.Hypermedia.Http.Composition
{
	public static class ResourceRegistration
	{
		public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> RegisterHttpResources(this ContainerBuilder builder, params Assembly[] assemblies)
		{
			return builder.RegisterAssemblyTypes(assemblies).Where(IsResourceType);
		}

		private static bool IsResourceType(Type type)
		{
			return typeof(HttpResource).IsAssignableFrom(type) && !type.IsAbstract && type.Name.EndsWith("Resource");
		}
	}
}