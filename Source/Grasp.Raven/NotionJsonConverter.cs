using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Cloak.Reflection;
using Cloak.Time;
using Grasp.Checks;
using Grasp.Semantics;
using Grasp.Work;
using Raven.Imports.Newtonsoft.Json;
using Raven.Imports.Newtonsoft.Json.Linq;

namespace Grasp.Raven
{
	public class NotionJsonConverter : JsonConverter
	{
		private readonly ITimeContext _timeContext;
		private readonly DomainModel _domainModel;
		private readonly INotionActivator _activator;
		private readonly IFieldValueConverter _fieldValueConverter;

		public NotionJsonConverter(ITimeContext timeContext, DomainModel domainModel, INotionActivator activator, IFieldValueConverter fieldValueConverter)
		{
			Contract.Requires(timeContext != null);
			Contract.Requires(domainModel != null);
			Contract.Requires(activator != null);
			Contract.Requires(fieldValueConverter != null);

			_timeContext = timeContext;
			_domainModel = domainModel;
			_activator = activator;
			_fieldValueConverter = fieldValueConverter;
		}

		public override bool CanConvert(Type objectType)
		{
			return _domainModel.GetTypeModel(objectType) is NotionModel;
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return ReconstituteNotion(GetModel(objectType), JObject.ReadFrom(reader));
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var notion = value as IFieldContext;

			if(notion != null)
			{
				GetBindingsJson(notion).WriteTo(writer, serializer.Converters.ToArray());
			}
		}

		private NotionModel GetModel(Type notionType)
		{
			return (NotionModel) _domainModel.GetTypeModel(notionType);
		}

		private Notion ReconstituteNotion(NotionModel model, JToken json)
		{
			var notion = _activator.ActivateUninitializedNotion(model.Type);

			new ReconstitutionContext(_timeContext, _domainModel, model, notion, json, _fieldValueConverter).Reconstitute();

			return notion;
		}

		private static JToken GetBindingsJson(IFieldContext notion)
		{
			return new JObject(
				from binding in notion.GetBindings()
				where binding.Field != EntityLifetime.WhenReconstitutedField
				where binding.Field != Aggregate._unobservedEventsField
				let propertyName = binding.Field.IsAttached ? binding.Field.FullName : binding.Field.Name
				select new JProperty(propertyName, binding.Value));
		}

		private sealed class ReconstitutionContext
		{
			private readonly ITimeContext _timeContext;
			private readonly DomainModel _domainModel;
			private readonly NotionModel _notionModel;
			private readonly IFieldContext _notion;
			private readonly JToken _json;
			private readonly IFieldValueConverter _fieldValueConverter;

			internal ReconstitutionContext(
				ITimeContext timeContext,
				DomainModel domainModel,
				NotionModel notionModel,
				IFieldContext notion,
				JToken json,
				IFieldValueConverter fieldValueConverter)
			{
				_timeContext = timeContext;
				_domainModel = domainModel;
				_notionModel = notionModel;
				_notion = notion;
				_json = json;
				_fieldValueConverter = fieldValueConverter;
			}

			internal void Reconstitute()
			{
				SetId();

				SetInherentFields();

				SetAttachedFields();

				SetWhenReconstituted();
			}

			private void SetId()
			{
				var id = GetJsonPropertyValue("Id");

				if(Check.That(id).IsNotNullOrEmpty())
				{
					var persistentNotionType = _notionModel.Type
						.GetInheritanceChain()
						.FirstOrDefault(type => type.IsAssignableFromGenericDefinition(typeof(PersistentNotion<>)));

					if(persistentNotionType != null)
					{
						var idType = persistentNotionType.GetGenericArguments().FirstOrDefault();

						if(idType != null)
						{
							_notion.SetValue(PersistentId.ValueField, ConvertValueToFieldType(idType, id));
						}
					}
				}
			}

			private void SetInherentFields()
			{
				SetFields(_notionModel.Fields, field => field.Name);
			}

			private void SetAttachedFields()
			{
				SetFields(_notionModel.GetAttachableFields(_domainModel), attachedField => attachedField.FullName);
			}

			private void SetFields(IEnumerable<Field> fields, Func<Field, string> propertyNameSelector)
			{
				foreach(var field in fields)
				{
					if(field.IsMany)
					{
						_notion.SetValue(field, field.GetManyDefault());
					}
					else
					{
						var value = GetJsonPropertyValue(propertyNameSelector(field));

						if(value != null)
						{
							_notion.SetValue(field, ConvertValueToFieldType(field.ValueType, value));
						}
					}
				}
			}

			private void SetWhenReconstituted()
			{
				_notion.SetValue(EntityLifetime.WhenReconstitutedField, _timeContext.Now);
			}

			private string GetJsonPropertyValue(string name)
			{
				return _json.Value<string>(name);
			}

			private object ConvertValueToFieldType(Type fieldType, object value)
			{
				return _fieldValueConverter.ConvertValueToFieldType(fieldType, value);
			}
		}
	}
}