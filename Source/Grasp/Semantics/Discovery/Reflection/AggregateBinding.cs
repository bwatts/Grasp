using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Work;

namespace Grasp.Semantics.Discovery.Reflection
{
	public class AggregateBinding : NotionBinding
	{
		public AggregateBinding(Type type, IEnumerable<TraitBinding> traitBindings = null, IEnumerable<FieldBinding> fieldBindings = null) : base(type, traitBindings, fieldBindings)
		{
			Contract.Requires(typeof(IAggregate).IsAssignableFrom(type));
		}

		protected override NotionModel GetNotionModel(IEnumerable<Trait> traits, IEnumerable<Field> fields)
		{
			return new AggregateModel(Type, fields.ToMany(), traits);
		}
	}
}