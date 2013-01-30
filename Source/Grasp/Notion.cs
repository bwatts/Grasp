using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.Reflection;
using Grasp.Knowledge;
using Grasp.Messaging;
using Grasp.Persistence;

namespace Grasp
{
	/// <summary>
	/// A object which externalizes its state
	/// </summary>
	public abstract class Notion : IFieldContext, IPersistent
	{
		/// <summary>
		/// Initializes a notion with an isolated context
		/// </summary>
		protected Notion()
		{
			SetIsolatedContext();
		}

		internal INotionContext Context { get; set; }

		internal void SetIsolatedContext()
		{
			Context = new IsolatedContext();
		}

		IEnumerable<FieldBinding> IFieldContext.GetBindings()
		{
			return GetBindings();
		}

		object IFieldContext.GetValue(Field field)
		{
			return GetValue(field);
		}

		T IFieldContext.GetValue<T>(Field<T> field)
		{
			return GetValue(field);
		}

		bool IFieldContext.TryGetValue(Field field, out object value)
		{
			return TryGetValue(field, out value);
		}

		bool IFieldContext.TryGetValue<T>(Field<T> field, out T value)
		{
			return TryGetValue(field, out value);
		}

		void IFieldContext.SetValue(Field field, object value)
		{
			SetValue(field, value);
		}

		void IFieldContext.SetValue<T>(Field<T> field, T value)
		{
			SetValue(field, value);
		}

		object IPersistent.Id
		{
			get { return Id; }
		}

		Event IPersistent.CreatedEvent
		{
			get { return CreatedEvent; }
		}

		Event IPersistent.ModifiedEvent
		{
			get { return ModifiedEvent; }
		}

		Event IPersistent.ReconstitutedEvent
		{
			get { return ReconstitutedEvent; }
		}

		protected IEnumerable<FieldBinding> GetBindings()
		{
			return Context.GetBindings(this);
		}

		protected bool TryGetValue(Field field, out object value)
		{
			return Context.TryGetValue(this, field, out value);
		}

		protected bool TryGetValue<T>(Field<T> field, out T value)
		{
			object untypedValue;

			var hasValue = Context.TryGetValue(this, field, out untypedValue);

			value = hasValue ? (T) untypedValue : default(T);

			return hasValue;
		}

		protected object GetValue(Field field)
		{
			var value = Context.GetValue(this, field);

			return value != null ? value : field.ValueType.GetDefaultValue();
		}

		protected T GetValue<T>(Field<T> field)
		{
			var value = Context.GetValue(this, field);

			return value != null ? (T) value : default(T);
		}

		protected void SetValue(Field field, object value)
		{
			Context.SetValue(this, field, value);
		}

		protected void SetValue<T>(Field<T> field, T value)
		{
			Context.SetValue(this, field, value);
		}

		/// <summary>
		/// Gets the value of <see cref="Grasp.Persistence.PersistentId.ValueField"/> associated with this notion
		/// </summary>
		protected internal object Id
		{
			get { return GetValue(PersistentId.ValueField) ?? PersistentId.Local; }
		}

		/// <summary>
		/// Gets the value of <see cref="Grasp.Lifetime.CreatedEventField"/> associated with this notion
		/// </summary>
		protected internal Event CreatedEvent
		{
			get { return GetValue(Lifetime.CreatedEventField) ?? Event.Unoccurred; }
		}

		/// <summary>
		/// Gets the value of <see cref="Grasp.Lifetime.ModifiedEventField"/> associated with this notion
		/// </summary>
		protected internal Event ModifiedEvent
		{
			get { return GetValue(Lifetime.ModifiedEventField) ?? Event.Unoccurred; }
		}

		/// <summary>
		/// Gets the value of <see cref="Grasp.Lifetime.ReconstitutedEventField"/> associated with this notion
		/// </summary>
		protected internal Event ReconstitutedEvent
		{
			get { return GetValue(Lifetime.ReconstitutedEventField) ?? Event.Unoccurred; }
		}

		/// <summary>
		/// Gets the amount of time passed since this notion was created
		/// </summary>
		/// <returns>The amount of time passed since this notion was created</returns>
		protected TimeSpan GetAge()
		{
			return this.NowRelativeTo(CreatedEvent.When).Length;
		}

		private sealed class IsolatedContext : INotionContext
		{
			private readonly IDictionary<Field, object> _values = new Dictionary<Field, object>();

			public IEnumerable<FieldBinding> GetBindings(Notion notion)
			{
				return _values.Select(pair => pair.Key.Bind(pair.Value));
			}

			public object GetValue(Notion notion, Field field)
			{
				object value;

				_values.TryGetValue(field, out value);

				return value;
			}

			public bool TryGetValue(Notion notion, Field field, out object value)
			{
				return _values.TryGetValue(field, out value);
			}

			public void SetValue(Notion notion, Field field, object value)
			{
				_values[field] = value;
			}
		}
	}
}