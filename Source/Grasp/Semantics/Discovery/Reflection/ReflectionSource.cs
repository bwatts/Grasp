using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Semantics.Discovery.Reflection
{
	public sealed class ReflectionSource : NamedNotion, IDomainModelSource
	{
		public static readonly Field<Many<Assembly>> AssembliesField = Field.On<ReflectionSource>.For(x => x.Assemblies);

		public ReflectionSource(FullName name, IEnumerable<Assembly> assemblies) : base(name)
		{
			Contract.Requires(assemblies != null);

			Assemblies = assemblies.ToMany();
		}

		public Many<Assembly> Assemblies { get { return GetValue(AssembliesField); } private set { SetValue(AssembliesField, value); } }

		public IEnumerable<IDomainModelBinding> BindDomainModels()
		{
			yield return Assemblies.BindDomainModel(Name);
		}
	}
}