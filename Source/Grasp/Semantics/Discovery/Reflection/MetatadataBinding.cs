using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Semantics.Discovery.Reflection
{
	public abstract class MetatadataBinding : Notion
	{
		public static readonly Field<MemberInfo> MemberField = Field.On<MetatadataBinding>.For(x => x.Member);

		public MetatadataBinding(MemberInfo member)
		{
			Contract.Requires(member != null);

			Member = member;
		}

		public MemberInfo Member { get { return GetValue(MemberField); } private set { SetValue(MemberField, value); } }

		public override string ToString()
		{
			return Member.ToString();
		}
	}
}