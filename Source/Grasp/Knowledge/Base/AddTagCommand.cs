using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Base
{
	public class AddTagCommand : EditArticleCommand
	{
		public static readonly Field<FullName> TagNameField = Field.On<AddTagCommand>.For(x => x.TagName);

		public AddTagCommand(EntityId workItemId, FullName articleName, FullName tagName) : base(workItemId, articleName)
		{
			Contract.Requires(tagName != null);

			TagName = tagName;
		}

		public FullName TagName { get { return GetValue(TagNameField); } private set { SetValue(TagNameField, value); } }
	}
}