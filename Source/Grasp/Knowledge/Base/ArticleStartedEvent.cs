using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;
using Grasp.Work;

namespace Grasp.Knowledge.Base
{
	public class ArticleStartedEvent : WorkItemEvent
	{
		public static readonly Field<FullName> ArticleNameField = Field.On<ArticleStartedEvent>.For(x => x.ArticleName);
		public static readonly Field<string> TitleField = Field.On<ArticleStartedEvent>.For(x => x.Title);

		public ArticleStartedEvent(FullName workItemName, FullName articleName, string title) : base(workItemName)
		{
			Contract.Requires(articleName != null);
			Contract.Requires(title != null);

			ArticleName = articleName;
			Title = title;
		}

		public FullName ArticleName { get { return GetValue(ArticleNameField); } private set { SetValue(ArticleNameField, value); } }
		public string Title { get { return GetValue(TitleField); } private set { SetValue(TitleField, value); } }
	}
}