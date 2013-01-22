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
		public AggregateModel(Type type, Many<Field> attachedFields, Many<Field> fields) : base(type, attachedFields, fields)
		{
			Contract.Requires(typeof(IAggregate).IsAssignableFrom(type));
		}
	}
}