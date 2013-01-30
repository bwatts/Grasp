using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Grasp.Semantics.Discovery.Reflection
{
	public class FieldBinding : MetatadataBinding
	{
		public static readonly Field<Field> FieldField = Field.On<FieldBinding>.For(x => x.Field);

		public FieldBinding(Field field, MemberInfo member) : base(member)
		{
			Contract.Requires(field != null);

			Field = field;
		}

		public Field Field { get { return GetValue(FieldField); } private set { SetValue(FieldField, value); } }

		public override string ToString()
		{
			return Field.ToString();
		}
	}
}