using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;

namespace Grasp.Knowledge.Base
{
	public abstract class EditArticleCommand : Command
	{
		public static readonly Field<FullName> ArticleNameField = Field.On<EditArticleCommand>.For(x => x.ArticleName);

		protected EditArticleCommand(EntityId workItemId, FullName articleName) : base(workItemId)
		{
			Contract.Requires(articleName != null);

			ArticleName = articleName;
		}

		public FullName ArticleName { get { return GetValue(ArticleNameField); } private set { SetValue(ArticleNameField, value); } }
	}
}