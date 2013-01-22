using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak.Reflection;
using Grasp.Checks;
using Grasp.Persistence;
using Grasp.Semantics;
using Grasp.Work;
using Newtonsoft.Json.Linq;

namespace Grasp.Json
{
	public sealed class JsonState : Notion, INotionState
	{
		public const string TypeKey = "$type";

		public static readonly Field<JToken> _jsonField = Field.On<JsonState>.For(x => x._json);
		public static readonly Field<IFieldValueConverter> _fieldValueConverterField = Field.On<JsonState>.For(x => x._fieldValueConverter);

		private JToken _json { get { return GetValue(_jsonField); } set { SetValue(_jsonField, value); } }
		private IFieldValueConverter _fieldValueConverter { get { return GetValue(_fieldValueConverterField); } set { SetValue(_fieldValueConverterField, value); } }

		public JsonState(JToken json, IFieldValueConverter fieldValueConverter)
		{
			Contract.Requires(json != null);
			Contract.Requires(fieldValueConverter != null);

			_json = json;
			_fieldValueConverter = fieldValueConverter;
		}

		public Type GetEffectiveType(Type expectedType)
		{
			return LoadType() ?? expectedType;
		}

		public IEnumerable<FieldBinding> GetBindings(DomainModel domainModel, NotionModel model, INotionActivator activator)
		{
			return new FieldBindingReader(_json, _fieldValueConverter, domainModel, model, activator).ReadBindings();
		}

		private Type LoadType()
		{
			var typeName = _json.Value<string>(TypeKey);

			return typeName == null ? null : LoadType(typeName);
		}

		private static Type LoadType(string name)
		{
			return Type.GetType(name, throwOnError: true);
		}

		private sealed class FieldBindingReader
		{
			private readonly JToken _json;
			private readonly IFieldValueConverter _fieldValueConverter;
			private readonly DomainModel _domainModel;
			private readonly NotionModel _notionModel;
			private readonly INotionActivator _activator;
			private List<FieldBinding> _bindings;

			internal FieldBindingReader(JToken json, IFieldValueConverter fieldValueConverter, DomainModel domainModel, NotionModel notionModel, INotionActivator activator)
			{
				_json = json;
				_fieldValueConverter = fieldValueConverter;
				_domainModel = domainModel;
				_notionModel = notionModel;
				_activator = activator;
			}

			internal IEnumerable<FieldBinding> ReadBindings()
			{
				_bindings = new List<FieldBinding>();

				BindId();

				BindInherentFields();

				BindAttachedFields();

				return _bindings;
			}

			private void Bind(Field field, object value)
			{
				_bindings.Add(field.Bind(value));
			}

			private void BindId()
			{
				var id = GetJsonValue("Id");

				if(Check.That(id).IsNotNullOrEmpty())
				{
					var persistentNotionType = _notionModel.Type
						.GetInheritanceChain()
						.FirstOrDefault(type => type.IsAssignableToGenericDefinition(typeof(PersistentNotion<>)));

					if(persistentNotionType != null)
					{
						var idType = persistentNotionType.GetGenericArguments().Single();

						Bind(PersistentId.ValueField, ConvertValueToFieldType(idType, id));
					}
				}
			}

			private void BindInherentFields()
			{
				BindFields(_notionModel.Fields, field => field.Name);
			}

			private void BindAttachedFields()
			{
				BindFields(_notionModel.GetAttachableFields(_domainModel), attachedField => attachedField.FullName);
			}

			private void BindFields(IEnumerable<Field> fields, Func<Field, string> propertyNameSelector)
			{
				foreach(var field in fields)
				{
					var propertyName = propertyNameSelector(field);

					if(field.AsMany.IsMany)
					{
						BindManyField(propertyName, field);
					}
					else if(field.AsNonMany.IsNonMany)
					{
						BindNonManyField(propertyName, field);
					}
					else
					{
						BindValue(propertyName, field);
					}
				}
			}

			private string GetJsonValue(string name)
			{
				return _json.Value<string>(name);
			}

			private object ConvertValueToFieldType(Type fieldType, object value)
			{
				return _fieldValueConverter.ConvertValueToFieldType(fieldType, value);
			}

			private void BindManyField(string propertyName, Field field)
			{
				var items = GetItems(propertyName, field.AsMany.ElementType);

				Bind(field, items == null ? field.AsMany.CreateEmptyValue() : field.AsMany.CreateValue(items));
			}

			private void BindNonManyField(string propertyName, Field field)
			{
				var items = GetItems(propertyName, field.AsNonMany.ElementType);

				Bind(field, items == null ? field.AsNonMany.CreateEmptyValue() : field.AsNonMany.CreateValue(items));
			}

			private void BindValue(string propertyName, Field field)
			{
				var value = GetJsonValue(propertyName);

				if(value != null)
				{
					Bind(field, ConvertValueToFieldType(field.ValueType, value));
				}
			}

			private IEnumerable GetItems(string propertyName, Type itemType)
			{
				var items = _json[propertyName] as JArray;

				return items == null ? null : items.Select(item => GetItem(itemType, item));
			}

			private object GetItem(Type itemType, JToken json)
			{
				return _activator.Activate(itemType, new JsonState(json, _fieldValueConverter));
			}
		}
	}
}