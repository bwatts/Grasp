using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Grasp.Knowledge.Work;
using Grasp.Semantics;

namespace Grasp.Knowledge.Persistence
{
	[ContractClass(typeof(PersistenceMediumContract))]
	public abstract class PersistenceMedium : Notion
	{
		public IEntitySet GetEntities(Type type, DomainModel model)
		{
			Contract.Requires(typeof(Notion).IsAssignableFrom(type));
			Contract.Requires(model != null);

			var getEntitiesCall = Expression.Call(Expression.Constant(this), "GetEntities", new[] { type }, Expression.Constant(model));

			var getEntitiesLambda = Expression.Lambda<Func<IEntitySet>>(Expression.Convert(getEntitiesCall, typeof(IEntitySet)));

			var getEntitiesFunction = getEntitiesLambda.Compile();

			return getEntitiesFunction();
		}

		public abstract IEntitySet<T> GetEntities<T>(DomainModel model) where T : Notion;

		public abstract void CommitChanges(DomainModel model, ChangeSet changeSet);
	}

	[ContractClassFor(typeof(PersistenceMedium))]
	internal abstract class PersistenceMediumContract : PersistenceMedium
	{
		public override IEntitySet<T> GetEntities<T>(DomainModel model)
		{
			Contract.Requires(model != null);
			Contract.Ensures(Contract.Result<IEntitySet<T>>() != null);

			return null;
		}

		public override void CommitChanges(DomainModel model, ChangeSet changeSet)
		{
			Contract.Requires(model != null);
			Contract.Requires(changeSet != null);
		}
	}
}