using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Work;

namespace Grasp
{
	/// <summary>
	/// A notion with a persistent <see cref="Guid"/> identifier
	/// </summary>
	public abstract class UniqueNotion : PersistentNotion<Guid>
	{
		// TODO: Is it possible to avoid generating an unused GUID when reconstituting?

		protected UniqueNotion(Guid? id = null) : base(id ?? Guid.NewGuid())
		{
			Contract.Requires(id != Guid.Empty);
		}
	}
}