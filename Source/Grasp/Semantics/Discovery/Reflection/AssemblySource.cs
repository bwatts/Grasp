using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Grasp.Semantics.Discovery.Reflection
{
	public sealed class AssemblySource : IDomainModelSource
	{
		private readonly IEnumerable<Assembly> _assemblies;

		public AssemblySource(IEnumerable<Assembly> assemblies)
		{
			Contract.Requires(assemblies != null);

			_assemblies = assemblies;
		}

		public IEnumerable<DomainModelBinding> GetDomainModelBindings()
		{
			yield return _assemblies.Bind();
		}
	}
}