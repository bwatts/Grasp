using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Grasp.Work;

namespace Grasp.Semantics
{
	public class AggregateModel : NotionModel
	{
		public AggregateModel(Type type, Many<Field> fields, IEnumerable<Trait> traits = null) : base(type, fields, traits)
		{
			Contract.Requires(typeof(IAggregate).IsAssignableFrom(type));
		}
	}
}