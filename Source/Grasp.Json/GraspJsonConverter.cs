using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.Serialization;
using Cloak;
using Grasp.Persistence;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Grasp.Json
{
	public class GraspJsonConverter : JsonConverter
	{
		private readonly IExcludedFieldSet _excludedFieldSet;
		private readonly IJsonStateFactory _stateFactory;
		private readonly INotionActivator _activator;

		public GraspJsonConverter(IExcludedFieldSet excludedFieldSet, IJsonStateFactory stateFactory, INotionActivator activator)
		{
			Contract.Requires(excludedFieldSet != null);
			Contract.Requires(stateFactory != null);
			Contract.Requires(activator != null);

			_excludedFieldSet = excludedFieldSet;
			_stateFactory = stateFactory;
			_activator = activator;
		}

		public override bool CanConvert(Type objectType)
		{
			return _activator.CanActivate(objectType);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var state = _stateFactory.CreateJsonState(JToken.ReadFrom(reader));

			return _activator.Activate(state.GetEffectiveType(objectType), state);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var notion = value as Notion;

			if(notion != null)
			{
				GetNotionObject(notion).WriteTo(writer, serializer.Converters.ToArray());
			}
		}

		private JObject GetNotionObject(Notion notion)
		{
			var type = notion.GetType();

			return new JObject(
				new JProperty(JsonState.TypeKey, type.FullName + ", " + type.Assembly.GetName().Name),
				GetJsonProperties(notion));
		}

		private IEnumerable<JProperty> GetJsonProperties(IFieldContext notion)
		{
			return
				from binding in notion.GetBindings()
				let propertyName = GetPropertyName(binding)
				where propertyName != null
				orderby binding.Field.Trait != null, binding.Field.IsPlural, propertyName
				select new JProperty(propertyName, GetJsonValue(binding.Value));
		}

		private object GetJsonValue(object value)
		{
			if(value == null)
			{
				return null;
			}

			// TODO: Externalize

			string text;

			if(value is FullName || value is Namespace)
			{
				return value.ToString();
			}
			else if(value is Notion)
			{
				return GetNotionObject((Notion) value);
			}
			else if(value is string)
			{
				return value;
			}
			else if(value is DateTime)
			{
				return ((DateTime) value).ToUniversalTime().ToString("o");
			}
			else if(value is IEnumerable)
			{
				return GetSequenceArray((IEnumerable) value);
			}
			else if(ChangeType.TryTo<string>(value, out text))
			{
				return text;
			}
			else
			{
				return value;
			}
		}

		private string GetPropertyName(FieldBinding binding)
		{
			if(_excludedFieldSet.IsExcluded(binding.Field))
			{
				return null;
			}
			else if(binding.Field.ValueType == typeof(FullName) && binding.Value == FullName.Anonymous)
			{
				return null;
			}
			else if(binding.Field.Trait != null)
			{
				return binding.Field.FullName;
			}
			else
			{
				return binding.Field.Name;
			}
		}

		private JArray GetSequenceArray(IEnumerable value)
		{
			return new JArray(value.Cast<object>().Select(item => GetJsonValue(item)));
		}
	}
}