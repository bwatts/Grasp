using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak;
using Grasp.Knowledge.Work;
using Grasp.Semantics;

namespace Grasp.Knowledge.Persistence
{
	public class PersistentWorkContext : Notion, IWorkContext
	{
		public static readonly Field<DomainModel> ModelField = Field.On<PersistentWorkContext>.Backing(x => x.Model);
		public static readonly Field<ChangeSet> ChangeSetField = Field.On<PersistentWorkContext>.Backing(x => x.ChangeSet);
		public static readonly Field<PersistenceMedium> PersistenceMediumField = Field.On<PersistentWorkContext>.Backing(x => x.PersistenceMedium);

		private readonly Func<Type, IEntitySet> _getEntities;

		private DomainModel Model { get { return GetValue(ModelField); } }
		private ChangeSet ChangeSet { get { return GetValue(ChangeSetField); } }
		private PersistenceMedium PersistenceMedium { get { return GetValue(PersistenceMediumField); } }

		public PersistentWorkContext()
		{
			// TODO: This, and logic like it, won't work with FormatterServices.GetUninitializedObject

			// TODO: Caching options

			_getEntities = type => PersistenceMedium.GetEntities(type, Model);

			_getEntities = _getEntities.Cached();
		}

		public IEntitySet GetEntities(Type type)
		{
			return _getEntities(type);
		}

		public IEntitySet<T> GetEntities<T>() where T : Notion
		{
			return (IEntitySet<T>) GetEntities(typeof(T));
		}

		public void CommitChanges()
		{
			PersistenceMedium.CommitChanges(Model, ChangeSet);
		}
	}
}