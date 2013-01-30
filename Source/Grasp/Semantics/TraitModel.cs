using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Semantics
{
	public class TraitModel : Notion
	{
		public static readonly Field<Trait> TraitField = Field.On<TraitModel>.For(x => x.Trait);

		public TraitModel(Trait trait)
		{
			Contract.Requires(trait != null);

			Trait = trait;
		}

		public Trait Trait { get { return GetValue(TraitField); } private set { SetValue(TraitField, value); } }
	}
}