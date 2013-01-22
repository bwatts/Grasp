using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Base
{
	public class ArticlePartCreatedEvent : ArticleEvent
	{
		public static readonly Field<FullName> PartNameField = Field.On<ArticlePartCreatedEvent>.For(x => x.PartName);
		public static readonly Field<PartType> TypeField = Field.On<ArticlePartCreatedEvent>.For(x => x.Type);

		public ArticlePartCreatedEvent(FullName workItemName, FullName articleName, FullName partName, PartType type) : base(workItemName, articleName)
		{
			Contract.Requires(partName != null);

			PartName = partName;
			Type = type;
		}

		public FullName PartName { get { return GetValue(PartNameField); } private set { SetValue(PartNameField, value); } }
		public PartType Type { get { return GetValue(TypeField); } private set { SetValue(TypeField, value); } }
	}
}