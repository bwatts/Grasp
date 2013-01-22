using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Work.Data
{
	public abstract class KnowledgeSession : Notion, IDisposable
	{
		public static readonly Field<KnowledgeBase> KnowledgeBaseField = Field.On<KnowledgeSession>.For(x => x.KnowledgeBase);

		private bool _disposed;

		protected KnowledgeSession(KnowledgeBase knowledgeBase)
		{
			Contract.Requires(knowledgeBase != null);

			KnowledgeBase = knowledgeBase;
		}

		public KnowledgeBase KnowledgeBase { get { return GetValue(KnowledgeBaseField); } private set { SetValue(KnowledgeBaseField, value); } }

		public abstract Task<Timeline> GetTimelineAsync(FullName name);

		public abstract Task SaveTimelinesAsync();

		public void Dispose()
		{
			if(!_disposed)
			{
				_disposed = true;

				Dispose(true);
			}

			GC.SuppressFinalize(this);
		}

		protected void Dispose(bool disposing)
		{}
	}
}