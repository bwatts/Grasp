using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;

namespace Grasp.Knowledge.Base
{
	public class ArticleCreatedEvent : Event
	{
		public static readonly Field<FullName> NameField = Field.On<ArticleCreatedEvent>.For(x => x.Name);
		public static readonly Field<string> TitleField = Field.On<ArticleCreatedEvent>.For(x => x.Title);

		public ArticleCreatedEvent(EntityId workItemId, FullName name, string title) : base(workItemId)
		{
			Contract.Requires(name != null);
			Contract.Requires(title != null);

			Name = name;
			Title = title;
		}

		public FullName Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }
		public string Title { get { return GetValue(TitleField); } private set { SetValue(TitleField, value); } }
	}
}