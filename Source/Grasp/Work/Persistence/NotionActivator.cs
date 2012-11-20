using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Cloak.Time;
using Grasp.Semantics;

namespace Grasp.Work.Persistence
{
	public sealed class NotionActivator : INotionActivator
	{
		public static Notion GetUninitialized(Type type)
		{
			Contract.Requires(type != null);

			var notion = (Notion) FormatterServices.GetUninitializedObject(type);

			notion.SetIsolatedContext();

			return notion;
		}

		public static T GetUninitialized<T>() where T : Notion
		{
			return (T) GetUninitialized(typeof(T));
		}

		private readonly ITimeContext _timeContext;
		private readonly DomainModel _domainModel;
		private readonly Func<INotionContext> _contextFactory;

		public NotionActivator(ITimeContext timeContext, DomainModel domainModel, Func<INotionContext> contextFactory)
		{
			Contract.Requires(timeContext != null);
			Contract.Requires(domainModel != null);
			Contract.Requires(contextFactory != null);

			_timeContext = timeContext;
			_domainModel = domainModel;
			_contextFactory = contextFactory;
		}

		public bool CanActivate(Type type)
		{
			return GetNotionModel(type) != null;
		}

		public Notion Activate(Type type, INotionState state)
		{
			var notion = ActivateNotion(type, state);

			Lifetime.WhenReconstitutedField.Set(notion, _timeContext.Now);

			return notion;
		}

		public T Activate<T>(INotionState state) where T : Notion
		{
			return (T) Activate(typeof(T), state);
		}

		private Notion ActivateNotion(Type type, INotionState state)
		{
			if(state != null)
			{
				type = state.GetEffectiveType(type);
			}

			var notion = ActivateInstance(type);

			BindState(type, notion, state);

			return notion;
		}

		private Notion ActivateInstance(Type type)
		{
			var notion = GetUninitialized(type);

			notion.Context = _contextFactory();

			return notion;
		}

		private void BindState(Type type, Notion notion, INotionState state)
		{
			var notionModel = GetNotionModel(type);

			var boundManyFields = new List<Field>();
			var boundNonManyFields = new List<Field>();

			if(state != null)
			{
				foreach(var binding in GetBindings(notionModel, state))
				{
					binding.Field.Set(notion, binding.Value);

					if(binding.Field.AsMany.IsMany)
					{
						boundManyFields.Add(binding.Field);
					}
					else
					{
						if(binding.Field.AsNonMany.IsNonMany)
						{
							boundNonManyFields.Add(binding.Field);
						}
					}
				}
			}

			foreach(var unboundManyField in notionModel.GetManyFields(_domainModel).Except(boundManyFields))
			{
				unboundManyField.Set(notion, unboundManyField.AsMany.CreateEmptyValue());
			}

			foreach(var unboundNonManyField in notionModel.GetNonManyFields(_domainModel).Except(boundNonManyFields))
			{
				unboundNonManyField.Set(notion, unboundNonManyField.AsNonMany.CreateEmptyValue());
			}
		}

		private NotionModel GetNotionModel(Type type)
		{
			return (NotionModel) _domainModel.GetTypeModel(type);
		}

		private IEnumerable<FieldBinding> GetBindings(NotionModel notionModel, INotionState state)
		{
			return state.GetBindings(_domainModel, notionModel, this);
		}
	}
}