using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Grasp.Semantics.Discovery.Reflection
{
	public class TraitBinding : MetatadataBinding
	{
		public static readonly Field<Trait> TraitField = Field.On<TraitBinding>.For(x => x.Trait);

		public TraitBinding(Trait trait, MemberInfo member) : base(member)
		{
			Contract.Requires(trait != null);

			Trait = trait;
		}

		public Trait Trait { get { return GetValue(TraitField); } private set { SetValue(TraitField, value); } }

		public override string ToString()
		{
			return Trait.ToString();
		}
	}
}