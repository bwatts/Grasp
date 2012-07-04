using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using Grasp.Knowledge;

namespace Grasp.Hypermedia.Http
{
	public sealed class MediaType : VendorPart<MediaType>
	{
		public static readonly Field<string> NameField = Field.On<MediaType>.Backing(x => x.Name);
		public static readonly Field<string> CharSetField = Field.On<MediaType>.Backing(x => x.CharSet);
		public static readonly Field<Many<NameValueHeaderValue>> ParametersField = Field.On<MediaType>.Backing(x => x.Parameters);
		public static readonly Field<VendorPath> VendorField = Field.On<MediaType>.Backing(x => x.VendorPath);

		public static readonly Relationship PluralRelationship = new Relationship(Resources.MediaTypesRelationship);

		public static readonly MediaType Unspecified = new MediaType("");

		public MediaType(string value) : base(value)
		{
			if(value == "")
			{
				Name = "";
				CharSet = "";
				Parameters = new Many<NameValueHeaderValue>();
				VendorPath = VendorPath.Unspecified;
			}
			else
			{
				var systemMediaType = new MediaTypeHeaderValue(value);

				Name = systemMediaType.MediaType ?? "";
				CharSet = systemMediaType.CharSet ?? "";
				Parameters = new Many<NameValueHeaderValue>(systemMediaType.Parameters);
				VendorPath = VendorPath.Parse(Name);
			}
		}

		public string Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }
		public string CharSet { get { return GetValue(CharSetField); } private set { SetValue(CharSetField, value); } }
		public Many<NameValueHeaderValue> Parameters { get { return GetValue(ParametersField); } private set { SetValue(ParametersField, value); } }
		public VendorPath VendorPath { get { return GetValue(VendorField); } private set { SetValue(VendorField, value); } }

		public MediaTypeHeaderValue ToHeaderValue()
		{
			return new MediaTypeHeaderValue(Value);
		}
	}
}