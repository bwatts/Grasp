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
	public class Article : Topic
	{
		public static readonly Field<ManyInOrder<ArticlePart>> _partsField = Field.On<Article>.For(x => x._parts);

		private ManyInOrder<ArticlePart> _parts { get { return GetValue(_partsField); } set { SetValue(_partsField, value); } }

		public Article(FullName workItemName, FullName name, string title)
		{
			Announce(new ArticleStartedEvent(workItemName, name, title));
		}

		// TODO: Patch command (http://code.google.com/p/google-diff-match-patch/)

		private void Handle(AddSectionCommand c)
		{
			// TODO: CommandFailedEvent for "Contract.Requires(c.ArticleName == Name);"

			Announce(new ArticlePartCreatedEvent(c.WorkItemName, c.ArticleName, c.SectionName, PartType.Section));
		}

		private void Handle(AddTagCommand c)
		{
			// TODO: CommandFailedEvent for "Contract.Requires(c.ArticleName == Name);"

			Announce(new ArticlePartCreatedEvent(c.WorkItemName, c.ArticleName, c.TagName, PartType.Tag));
		}

		private void Observe(ArticleStartedEvent e)
		{
			OnCreated(e.ArticleName, e.When);

			_parts = new ManyInOrder<ArticlePart>(new Title(e.Title));
		}

		private void Observe(ArticlePartCreatedEvent e)
		{
			OnModified(e.When);

			_parts.AsWriteable().Add(GetPart(e));
		}

		private static ArticlePart GetPart(ArticlePartCreatedEvent e)
		{
			switch(e.Type)
			{
				case PartType.Section:
					return new Section(e.PartName);
				case PartType.Tag:
					return new Tag(e.PartName);
				default:
					throw new NotSupportedException(Resources.UnsupportedArticlePartType.FormatInvariant(e.Type));
			}
		}
	}
}