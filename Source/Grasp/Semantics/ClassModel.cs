using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Semantics
{
	public class ClassModel : TypeModel
	{
		public static readonly Field<Many<Trait>> TraitsField = Field.On<ClassModel>.For(x => x.Traits);

		public ClassModel(Type type, IEnumerable<Trait> traits = null) : base(type)
		{
			Contract.Requires(type.IsClass);

			Traits = (traits ?? Enumerable.Empty<Trait>()).ToMany();
		}

		public Many<Trait> Traits { get { return GetValue(TraitsField); } private set { SetValue(TraitsField, value); } }
	}
}