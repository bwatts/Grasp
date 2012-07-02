using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Knowledge;
using Grasp.Knowledge.Persistence;
using Raven.Client;

namespace Grasp.Raven
{
	public class RavenWorkContext : PersistentWorkContext
	{
		public static readonly Field<IDocumentSession> SessionField = Field.On<RavenWorkContext>.Backing(x => x.Session);

		public IDocumentSession Session { get { return GetValue(SessionField); } }
	}
}