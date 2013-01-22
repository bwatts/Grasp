using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Base
{
	public class AddTagCommand : ArticleCommand
	{
		public static readonly Field<FullName> TagNameField = Field.On<AddTagCommand>.For(x => x.TagName);

		public AddTagCommand(FullName workItemName, FullName articleName, FullName tagName) : base(workItemName, articleName)
		{
			Contract.Requires(tagName != null);

			TagName = tagName;
		}

		public FullName TagName { get { return GetValue(TagNameField); } private set { SetValue(TagNameField, value); } }
	}
}