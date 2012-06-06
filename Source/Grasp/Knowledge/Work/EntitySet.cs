using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.Globalization;
using Cloak.Linq;

namespace Grasp.Knowledge.Work
{
	public abstract class EntitySet<TEntity> : Notion, IEntitySet<TEntity> where TEntity : Notion
	{
		public static Field<IQueryProvider> QueryProviderField = Field.On<EntitySet<TEntity>>.Backing(x => x.QueryProvider);

		protected IQueryProvider QueryProvider { get { return GetValue(QueryProviderField); } }

		#region IEnumerable

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerator<TEntity> GetEnumerator()
		{
			return ((IEnumerable<TEntity>) QueryProvider.Execute(Expression)).GetEnumerator();
		}
		#endregion

		#region IQueryable

		Type IQueryable.ElementType
		{
			get { return ElementType; }
		}

		Expression IQueryable.Expression
		{
			get { return Expression; }
		}

		IQueryProvider IQueryable.Provider
		{
			get { return QueryProvider; }
		}

		protected Type ElementType
		{
			get { return typeof(TEntity); }
		}

		protected Expression Expression
		{
			get { return Expression.Constant(this); }
		}
		#endregion

		#region IEntitySet

		void IEntitySet.AddOnCommit(Notion entity)
		{
			AddOnCommit(entity);
		}

		void IEntitySet.RemoveOnCommit(Notion entity)
		{
			RemoveOnCommit(entity);
		}

		public abstract void AddOnCommit(TEntity entity);

		public abstract void RemoveOnCommit(TEntity entity);

		protected virtual void AddOnCommit(Notion entity)
		{
			AddOnCommit((TEntity) entity);
		}

		protected virtual void RemoveOnCommit(Notion entity)
		{
			RemoveOnCommit((TEntity) entity);
		}
		#endregion

		public override string ToString()
		{
			var knownQueryProvider = QueryProvider as QueryProvider;

			return knownQueryProvider != null ? knownQueryProvider.GetQueryText(Expression) : Expression.ToString();
		}
	}
}