using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Grasp.Knowledge;

namespace Grasp.Semantics.Discovery.Reflection
{
	public abstract class MemberBinding : Notion
	{
		public static Field<MemberInfo> InfoField = Field.On<MemberBinding>.Backing(x => x.Info);
		public static Field<Field> FieldField = Field.On<MemberBinding>.Backing(x => x.Field);

		public MemberInfo Info { get { return GetValue(InfoField); } }
		public Field Field { get { return GetValue(FieldField); } }

		public override string ToString()
		{
			return Info.ToString();
		}
	}

	public abstract class MemberBinding<TInfo> : MemberBinding where TInfo : MemberInfo
	{
		//public static new Field<TInfo> InfoField = MemberBinding.InfoField.ShadowedBy<MemberBinding<TInfo>>.For(x => x.Info);

		//public new TInfo Info
		//{
		//  get { return GetValue(InfoField); }
		//}
	}
}