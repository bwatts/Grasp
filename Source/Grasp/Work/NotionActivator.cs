using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Work
{
	public sealed class NotionActivator : INotionActivator
	{
		private readonly Func<INotionContext> _contextFactory;

		public NotionActivator(Func<INotionContext> contextFactory)
		{
			Contract.Requires(contextFactory != null);

			_contextFactory = contextFactory;
		}

		public Notion ActivateUninitializedNotion(Type type)
		{
			var notion = (Notion) FormatterServices.GetUninitializedObject(type);

			notion.Context = _contextFactory();

			return notion;
		}

		public T ActivateUninitializedNotion<T>() where T : Notion
		{
			return (T) ActivateUninitializedNotion(typeof(T));
		}
	}
}