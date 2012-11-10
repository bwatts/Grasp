using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Grasp.Semantics.Discovery.Reflection
{
	public class FieldBinding : Notion
	{
		public static readonly Field<Field> FieldField = Field.On<FieldBinding>.For(x => x.Field);
		public static readonly Field<MemberInfo> MemberField = Field.On<FieldBinding>.For(x => x.Member);

		public FieldBinding(Field field, MemberInfo member)
		{
			Contract.Requires(field != null);
			Contract.Requires(member != null);

			Field = field;
			Member = member;
		}

		public Field Field { get { return GetValue(FieldField); } private set { SetValue(FieldField, value); } }
		public MemberInfo Member { get { return GetValue(MemberField); } private set { SetValue(MemberField, value); } }

		public override string ToString()
		{
			return Member.ToString();
		}
	}
}