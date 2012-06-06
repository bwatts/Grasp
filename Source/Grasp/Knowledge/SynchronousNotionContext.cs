using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Cloak;

namespace Grasp.Knowledge
{
	/// <summary>
	/// A synchronous context which maintains the state of a <see cref="Notion"/>
	/// </summary>
	public sealed class SynchronousNotionContext : INotionContext
	{
		private readonly INotionContext _baseContext;
		private readonly ThreadAffinity _threadAffinity;

		/// <summary>
		/// Initializes a notion with the specified base context and thread affinity
		/// </summary>
		/// <param name="baseContext">The synchronized context</param>
		/// <param name="threadAffinity">The thread affinity of this context</param>
		public SynchronousNotionContext(INotionContext baseContext, ThreadAffinity threadAffinity)
		{
			Contract.Requires(baseContext != null);
			Contract.Requires(threadAffinity != null);

			_baseContext = baseContext;
			_threadAffinity = threadAffinity;
		}

		/// <summary>
		/// Gets the value of the specified field for the specified notion. Enforces thread affinity.
		/// </summary>
		/// <param name="notion">The notion containing the value of the specified field</param>
		/// <param name="field">The field of the value to retrieve</param>
		/// <returns>The value of the specified field associated with the specified notion</returns>
		/// <exception cref="ThreadAffinityException">Thrown if this context is accessed by a thread other than the one for which it has affinity</exception>
		public object GetValue(Notion notion, Field field)
		{
			_threadAffinity.Enforce();

			return _baseContext.GetValue(notion, field);
		}

		/// <summary>
		/// Associates the specified value with the specified notion and field. Enforces thread affinity.
		/// </summary>
		/// <param name="notion">The notion containing the value of the specified field</param>
		/// <param name="field">The field to set to the specified value</param>
		/// <param name="value">The value of the specified field to set for the specified notion</param>
		/// <exception cref="ThreadAffinityException">Thrown if this context is accessed by a thread other than the one for which it has affinity</exception>
		public void SetValue(Notion notion, Field field, object value)
		{
			_threadAffinity.Enforce();

			_baseContext.SetValue(notion, field, value);
		}
	}
}