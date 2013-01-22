using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;

namespace Grasp.Knowledge.Base
{
	public class StartArticleCommand : ArticleCommand
	{
		public StartArticleCommand(FullName workItemName, FullName articleName) : base(workItemName, articleName)
		{}
	}
}