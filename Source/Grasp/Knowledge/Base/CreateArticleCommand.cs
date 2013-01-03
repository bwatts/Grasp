using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;

namespace Grasp.Knowledge.Base
{
	public class CreateArticleCommand : Command
	{
		public static readonly Field<FullName> NameField = Field.On<CreateArticleCommand>.For(x => x.Name);

		public CreateArticleCommand(EntityId workItemId, FullName name) : base(workItemId)
		{
			Contract.Requires(name != null);

			Name = name;
		}

		public FullName Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }
	}
}