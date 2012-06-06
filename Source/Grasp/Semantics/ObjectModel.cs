using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Grasp.Semantics
{
	public class ObjectModel : TypeModel
	{
		public static Field<Many<Field>> FieldsField = Field.On<ObjectModel>.Backing(x => x.Fields);

		public Many<Field> Fields { get { return GetValue(FieldsField); } }
	}
}