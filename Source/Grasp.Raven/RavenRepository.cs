using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Text;
using Cloak;
using Grasp.Knowledge;
using Grasp.Knowledge.Work;
using Raven.Client;

namespace Grasp.Raven
{
	public abstract class RavenRepository<TEntity> : Repository<TEntity> where TEntity : Notion
	{
		protected RavenRepository(RavenWorkContext ravenWorkContext) : base(ravenWorkContext)
		{}

		protected new RavenWorkContext Context
		{
			get { return (RavenWorkContext) base.Context; }
		}

		protected IDocumentSession Session
		{
			get { return Context.Session; }
		}

		protected virtual IFormatProvider IdFormatProvider
		{
			get { return CultureInfo.InvariantCulture; }
		}

		protected TEntity GetById(ValueType id)
		{
			return Session.Load<TEntity>(id);
		}

		protected TEntity	GetById(string id)
		{
			return Session.Load<TEntity>(id);
		}

		protected IEnumerable<TEntity> GetByIds(IEnumerable<string> ids)
		{
			return Session.Load<TEntity>(ids);
		}

		protected IEnumerable<TEntity> GetByIds(params string[] ids)
		{
			return Session.Load<TEntity>(ids);
		}

		protected virtual TEntity GetById(object id)
		{
			return Session.Load<TEntity>(GetIdText(id));
		}

		protected virtual string GetIdText(object id)
		{
			return "{0}".Format(IdFormatProvider, id);
		}

		protected virtual IEnumerable<TEntity> GetByIds(IEnumerable<object> ids)
		{
			return Session.Load<TEntity>(GetIdsText(ids));
		}

		protected virtual IEnumerable<string> GetIdsText(IEnumerable<object> ids)
		{
			return ids.Select(GetIdText);
		}

		protected virtual IEnumerable<string> GetIdsText(params object[] ids)
		{
			return ids.Select(GetIdText);
		}
	}
}