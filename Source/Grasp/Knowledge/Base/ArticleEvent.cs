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
	public abstract class ArticleEvent : WorkItemEvent
	{
		public static readonly Field<FullName> ArticleNameField = Field.On<ArticleEvent>.For(x => x.ArticleName);

		protected ArticleEvent(FullName workItemName, FullName articleName) : base(workItemName)
		{
			Contract.Requires(articleName != null);

			ArticleName = articleName;
		}

		public FullName ArticleName { get { return GetValue(ArticleNameField); } private set { SetValue(ArticleNameField, value); } }
	}
}