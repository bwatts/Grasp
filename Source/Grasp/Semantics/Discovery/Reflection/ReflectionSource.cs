using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Semantics.Discovery.Reflection
{
	public sealed class ReflectionSource : Notion, IDomainModelSource
	{
		public static readonly Field<string> NameField = Field.On<ReflectionSource>.For(x => x.Name);
		public static readonly Field<Many<Assembly>> AssembliesField = Field.On<ReflectionSource>.For(x => x.Assemblies);

		public ReflectionSource(string name, IEnumerable<Assembly> assemblies)
		{
			Contract.Requires(name != null);
			Contract.Requires(assemblies != null);

			Name = name;
			Assemblies = assemblies.ToMany();
		}

		public string Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }
		public Many<Assembly> Assemblies { get { return GetValue(AssembliesField); } private set { SetValue(AssembliesField, value); } }

		public IEnumerable<IDomainModelBinding> BindDomainModels()
		{
			yield return Assemblies.BindDomainModel(Name);
		}
	}
}