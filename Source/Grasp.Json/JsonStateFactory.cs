using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Persistence;
using Newtonsoft.Json.Linq;

namespace Grasp.Json
{
	public sealed class JsonStateFactory : Notion, IJsonStateFactory
	{
		public static readonly Field<IFieldValueConverter> _fieldValueConverterField = Field.On<JsonStateFactory>.For(x => x._fieldValueConverter);

		private IFieldValueConverter _fieldValueConverter { get { return GetValue(_fieldValueConverterField); } set { SetValue(_fieldValueConverterField, value); } }

		public JsonStateFactory(IFieldValueConverter fieldValueConverter)
		{
			Contract.Requires(fieldValueConverter != null);

			_fieldValueConverter = fieldValueConverter;
		}

		public JsonState CreateJsonState(JToken json)
		{
			return new JsonState(json, _fieldValueConverter);
		}
	}
}