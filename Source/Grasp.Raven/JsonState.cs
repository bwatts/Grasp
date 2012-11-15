using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak.Reflection;
using Grasp.Checks;
using Grasp.Semantics;
using Grasp.Work;
using Grasp.Work.Persistence;
using Raven.Imports.Newtonsoft.Json.Linq;

namespace Grasp.Raven
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
			var typeName = _json.Value<string>(TypeKey);

			return typeName != null ? LoadType(typeName) : expectedType;
		}

		public IEnumerable<FieldBinding> GetBindings(DomainModel domainModel, NotionModel model)
		{
			return new FieldBindingReader(_json, _fieldValueConverter, domainModel, model).ReadBindings();
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
			private List<FieldBinding> _bindings;

			internal FieldBindingReader(JToken json, IFieldValueConverter fieldValueConverter, DomainModel domainModel, NotionModel notionModel)
			{
				_json = json;
				_fieldValueConverter = fieldValueConverter;
				_domainModel = domainModel;
				_notionModel = notionModel;
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
						.FirstOrDefault(type => type.IsAssignableFromGenericDefinition(typeof(PersistentNotion<>)));

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

					if(field.IsMany)
					{
						var array = _json[propertyName] as JArray;

						if(array != null)
						{
							// TODO: Bind many field
						}
					}
					else
					{
						var value = GetJsonValue(propertyName);

						if(value != null)
						{
							Bind(field, ConvertValueToFieldType(field.ValueType, value));
						}
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
		}
	}
}