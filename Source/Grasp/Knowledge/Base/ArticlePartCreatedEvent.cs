using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Work.Items;

namespace Grasp.Knowledge.Base
{
	public class ArticlePartCreatedEvent : ArticleEditedEvent
	{
		public static readonly Field<FullName> NameField = Field.On<ArticlePartCreatedEvent>.For(x => x.Name);
		public static readonly Field<PartType> TypeField = Field.On<ArticlePartCreatedEvent>.For(x => x.Type);

		public ArticlePartCreatedEvent(EntityId workItemId, FullName articleName, FullName name, PartType type) : base(workItemId, articleName)
		{
			Contract.Requires(name != null);

			Name = name;
			Type = type;
		}

		public FullName Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }
		public PartType Type { get { return GetValue(TypeField); } private set { SetValue(TypeField, value); } }
	}
}