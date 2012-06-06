using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Semantics.Relationships;

namespace Grasp.Knowledge
{
	public class Many<T> : ISet<T>
	{
		public static readonly Field<Cardinality> UpperLimitField = Field.On<Many<T>>.Backing(x => x.UpperLimit);

		public Many(CardinalityLimit upperLimit)
		{
			UpperLimit = upperLimit;
		}

		public CardinalityLimit UpperLimit { get { return GetValue(UpperLimitField); } }
	}
}