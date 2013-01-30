using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Semantics.Discovery.Reflection
{
	public class ClassBinding : TypeBinding
	{
		public static readonly Field<Many<TraitBinding>> TraitBindingsField = Field.On<ClassBinding>.For(x => x.TraitBindings);

		public ClassBinding(Type type, IEnumerable<TraitBinding> traitBindings = null) : base(type)
		{
			TraitBindings = (traitBindings ?? Enumerable.Empty<TraitBinding>()).ToMany();
		}

		public Many<TraitBinding> TraitBindings { get { return GetValue(TraitBindingsField); } private set { SetValue(TraitBindingsField, value); } }

		public override TypeModel GetTypeModel()
		{
			var traits = TraitBindings.Select(traitBinding => traitBinding.Trait).ToMany();

			return GetClassModel(traits);
		}

		protected virtual ClassModel GetClassModel(IEnumerable<Trait> traits)
		{
			Contract.Requires(traits != null);

			return new ClassModel(Type, traits);
		}
	}
}