using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Knowledge.Persistence;

namespace Grasp.Knowledge.Work
{
	public sealed class FieldChange : Change
	{
		public static readonly Field<Field> FieldField = Field.On<FieldChange>.For(x => x.Field);
		public static readonly Field<object> OldValueField = Field.On<FieldChange>.For(x => x.OldValue);
		public static readonly Field<object> NewValueField = Field.On<FieldChange>.For(x => x.NewValue);

		public Field Field { get { return GetValue(FieldField); } }
		public object OldValue { get { return GetValue(OldValueField); } }
		public object NewValue { get { return GetValue(NewValueField); } }

		internal FieldChange() : base(ChangeType.Field)
		{}
	}
}