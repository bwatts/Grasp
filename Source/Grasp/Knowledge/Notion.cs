using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grasp.Knowledge
{
	/// <summary>
	/// Base implementation of an object which externalizes its state to a context
	/// </summary>
	public abstract class Notion : IFieldContext
	{
		/// <summary>
		/// Initializes a notion with an isolated context
		/// </summary>
		protected Notion()
		{
			Context = new InitialNotionContext();
		}

		internal INotionContext Context { get; set; }

		// TODO: Eventually replace this with the explicit implementation

		public object GetValue(Field field)
		{
			return Context.GetValue(this, field);
		}

		public T GetValue<T>(Field<T> field)
		{
			return (T) Context.GetValue(this, field);
		}

		public void SetValue(Field field, object value)
		{
			Context.SetValue(this, field, value);
		}

		public void SetValue<T>(Field<T> field, T value)
		{
			Context.SetValue(this, field, value);
		}

		// Keep these off the public interface by unconditionally passing through to the context

		//object IFieldContext.GetValue(Field field)
		//{
		//  return Context.GetValue(this, field);
		//}

		//T IFieldContext.GetValue<T>(Field<T> field)
		//{
		//  return (T) Context.GetValue(this, field);
		//}

		//void IFieldContext.SetValue(Field field, object value)
		//{
		//  Context.SetValue(this, field, value);
		//}

		//void IFieldContext.SetValue<T>(Field<T> field, T value)
		//{
		//  Context.SetValue(this, field, value);
		//}
	}
}