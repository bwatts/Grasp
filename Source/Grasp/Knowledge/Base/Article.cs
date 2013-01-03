using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Grasp.Work;

namespace Grasp.Knowledge.Base
{
	public class Article : Aggregate
	{
		public static readonly Field<ManyInOrder<ArticlePart>> PartsField = Field.On<Article>.For(x => x.Parts);

		public Article(EntityId workItemId, FullName name, string title)
		{
			Announce(new ArticleCreatedEvent(workItemId, name, title));
		}

		public FullName Name { get { return GetValue(FullName.NameField); } private set { SetValue(FullName.NameField, value); } }
		public ManyInOrder<ArticlePart> Parts { get { return GetValue(PartsField); } private set { SetValue(PartsField, value); } }

		// TODO: Patch command (http://code.google.com/p/google-diff-match-patch/)

		private void Handle(AddSectionCommand command)
		{
			// TODO: CommandFailedEvent for "Contract.Requires(command.ArticleName == Name);"

			Announce(new ArticlePartCreatedEvent(command.WorkItemId, command.ArticleName, command.SectionName, PartType.Section));
		}

		private void Handle(AddTagCommand command)
		{
			// TODO: CommandFailedEvent for "Contract.Requires(command.ArticleName == Name);"

			Announce(new ArticlePartCreatedEvent(command.WorkItemId, command.ArticleName, command.TagName, PartType.Tag));
		}

		private void Observe(ArticleCreatedEvent e)
		{
			Parts = Params.Of<ArticlePart>(new Title(e.Title)).ToManyInOrder();
		}

		private void Observe(ArticlePartCreatedEvent e)
		{
			Parts.AsWriteable().Add(GetPart(e));
		}

		private ArticlePart GetPart(ArticlePartCreatedEvent e)
		{
			switch(e.Type)
			{
				case PartType.Section:
					return new Section(e.Name);
				case PartType.Tag:
					return new Tag(e.Name);
				default:
					throw new NotSupportedException(Resources.UnsupportedArticlePartType.FormatInvariant(e.Type));
			}
		}
	}
}