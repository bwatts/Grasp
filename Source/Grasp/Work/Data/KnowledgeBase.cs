using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Work.Data
{
	public abstract class KnowledgeBase : NamedNotion
	{
		protected KnowledgeBase(FullName name) : base(name)
		{}

		public abstract Task<KnowledgeSession> OpenSessionAsync();
	}
}