using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using Grasp.Knowledge;

namespace Grasp.Hypermedia.Http.Media
{
	public sealed class MediaType : MediaTypePart<MediaType>
	{
		public static readonly Relationship PluralRelationship = new Relationship(Resources.MediaTypesRelationship);

		public static readonly MediaType Unspecified = new MediaType("");

		public static readonly Field<string> NameField = Field.On<MediaType>.Backing(x => x.Name);
		public static readonly Field<string> CharSetField = Field.On<MediaType>.Backing(x => x.CharSet);
		public static readonly Field<Many<NameValueHeaderValue>> ParametersField = Field.On<MediaType>.Backing(x => x.Parameters);
		public static readonly Field<MediaTypeVendor> VendorField = Field.On<MediaType>.Backing(x => x.Vendor);

		public MediaType(string value) : base(value)
		{
			var systemMediaType = new MediaTypeHeaderValue(value);

			Name = systemMediaType.MediaType;
			CharSet = systemMediaType.CharSet;
			Parameters = new Many<NameValueHeaderValue>(systemMediaType.Parameters);

			Vendor = MediaTypeVendor.FromMediaType(Name);
		}

		public string Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }

		public string CharSet { get { return GetValue(CharSetField); } private set { SetValue(CharSetField, value); } }

		public Many<NameValueHeaderValue> Parameters { get { return GetValue(ParametersField); } private set { SetValue(ParametersField, value); } }

		public MediaTypeVendor Vendor { get { return GetValue(VendorField); } private set { SetValue(VendorField, value); } }

		public MediaTypeHeaderValue ToHeaderValue()
		{
			return new MediaTypeHeaderValue(Value);
		}
	}
}