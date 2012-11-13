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
using Grasp.Messaging;
using Grasp.Semantics;
using Grasp.Work;
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
				GetBindingsJson(notion).WriteTo(writer, serializer.Converters.ToArray());
			}
		}

		private static JToken GetBindingsJson(IFieldContext notion)
		{
			// TODO: Externalize field selection

			return new JObject(
				from binding in notion.GetBindings()
				where binding.Field != Lifetime.WhenReconstitutedField
				where binding.Field != Aggregate._unobservedEventsField
				where binding.Field != Message.ChannelField
				let propertyName = binding.Field.IsAttached ? binding.Field.FullName : binding.Field.Name
				select new JProperty(propertyName, binding.Value));
		}
	}
}