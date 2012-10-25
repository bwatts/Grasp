using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Grasp.Semantics
{
	public class EntityModel : TypeModel
	{
		public static readonly Field<Many<Field>> FieldsField = Field.On<EntityModel>.For(x => x.Fields);

		public Many<Field> Fields { get { return GetValue(FieldsField); } }
	}
}