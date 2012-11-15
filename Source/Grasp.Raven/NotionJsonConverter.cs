using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Cloak.Reflection;
using Cloak.Time;
using Grasp.Checks;
using Grasp.Messaging;
using Grasp.Semantics;
using Grasp.Work;
using Grasp.Work.Items;
using Grasp.Work.Persistence;
using Raven.Imports.Newtonsoft.Json;
using Raven.Imports.Newtonsoft.Json.Linq;

namespace Grasp.Raven
{
	public class NotionJsonConverter : JsonConverter
	{
		private readonly Func<JToken, INotionState> _stateFactory;
		private readonly INotionActivator _activator;

		public NotionJsonConverter(Func<JToken, INotionState> stateFactory, INotionActivator activator)
		{
			Contract.Requires(stateFactory != null);
			Contract.Requires(activator != null);

			_stateFactory = stateFactory;
			_activator = activator;
		}

		public override bool CanConvert(Type objectType)
		{
			return _activator.CanActivate(objectType);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var state = _stateFactory(JObject.ReadFrom(reader));

			return _activator.Activate(objectType, state);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var notion = value as Notion;

			if(notion != null)
			{
				GetJson(notion).WriteTo(writer, serializer.Converters.ToArray());
			}
		}

		private static JToken GetJson(IFieldContext notion)
		{
			return new JObject(GetJsonProperties(notion));
		}

		private static IEnumerable<JProperty> GetJsonProperties(IFieldContext notion)
		{
			return
				from binding in notion.GetBindings()
				where !ExcludeField(binding.Field)
				let propertyName = binding.Field.IsAttached ? binding.Field.FullName : binding.Field.Name
				orderby binding.Field.IsAttached, binding.Field.IsMany, propertyName
				select new JProperty(propertyName, GetJsonValue(binding.Value));
		}

		private static bool ExcludeField(Field field)
		{
			// TODO: Externalize

			return field == PersistentId.ValueField
				|| field == Lifetime.WhenReconstitutedField
				|| field == Aggregate._unobservedEventsField
				|| field == Message.ChannelField;
		}

		private static object GetJsonValue(object value)
		{
			if(value == null)
			{
				return null;
			}

			// TODO: Allow extensibility and externalize these

			if(value is Notion)
			{
				return GetNotionObject((Notion) value);
			}
			else if(!(value is string) && value is IEnumerable)
			{
				return GetSequenceArray((IEnumerable) value);
			}
			else if(value is Guid)
			{
				return ((Guid) value).ToString("N").ToUpper();
			}
			else if(value is Progress)
			{
				return ((Progress) value).Value;
			}
			else
			{
				return value;
			}
		}

		private static JObject GetNotionObject(Notion notion)
		{
			var type = notion.GetType();

			return new JObject(
				new JProperty(JsonState.TypeKey, type.FullName + ", " + type.Assembly.GetName().Name),
				GetJsonProperties(notion));
		}

		private static JArray GetSequenceArray(IEnumerable value)
		{
			return new JArray(value.Cast<object>().Select(item => GetJsonValue(item)));
		}
	}
}