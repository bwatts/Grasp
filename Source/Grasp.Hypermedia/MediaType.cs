using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using Grasp.Knowledge;

namespace Grasp.Hypermedia
{
	public sealed class MediaType : ComparableNotion<MediaType>
	{
		public static readonly Field<string> ValueField = Field.On<MediaType>.Backing(x => x.Value);
		public static readonly Field<string> NameField = Field.On<MediaType>.Backing(x => x.Name);
		public static readonly Field<string> CharSetField = Field.On<MediaType>.Backing(x => x.CharSet);
		public static readonly Field<Many<NameValueHeaderValue>> ParametersField = Field.On<MediaType>.Backing(x => x.Parameters);
		public static readonly Field<VendorPath> VendorField = Field.On<MediaType>.Backing(x => x.VendorPath);

		public static readonly Relationship PluralRelationship = new Relationship(Resources.MediaTypesRelationship);

		public static readonly MediaType Unspecified = new MediaType("");
		public static readonly MediaType ApplicationJson = new MediaType("application/json");
		public static readonly MediaType ApplicationXhtml = new MediaType("application/xhtml+xml");
		public static readonly MediaType ApplicationXml = new MediaType("application/xml");
		public static readonly MediaType ApplicationWwwFormUrlEncoded = new MediaType("application/x-www-form-urlencoded");
		public static readonly MediaType TextHtml = new MediaType("text/html");
		public static readonly MediaType TextJson = new MediaType("text/json");
		public static readonly MediaType TextXml = new MediaType("text/xml");

		// Each of these subsets is ordered from most to least specific. This ensures the most relevant match during content negotiation.

		public static IEnumerable<MediaType> JsonTypes
		{
			get
			{
				yield return ApplicationJson;
				yield return TextJson;
			}
		}

		public static IEnumerable<MediaType> XmlTypes
		{
			get
			{
				yield return ApplicationXhtml;
				yield return ApplicationXml;
				yield return TextXml;
			}
		}

		public static IEnumerable<MediaType> HtmlTypes
		{
			get
			{
				yield return ApplicationXhtml;
				yield return TextHtml;
			}
		}

		public MediaType(string value)
		{
			Contract.Requires(value != null);

			Value = value;

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
				VendorPath = new VendorPath(value);
			}
		}

		public string Value { get { return GetValue(ValueField); } private set { SetValue(ValueField, value); } }
		public string Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }
		public string CharSet { get { return GetValue(CharSetField); } private set { SetValue(CharSetField, value); } }
		public Many<NameValueHeaderValue> Parameters { get { return GetValue(ParametersField); } private set { SetValue(ParametersField, value); } }
		public VendorPath VendorPath { get { return GetValue(VendorField); } private set { SetValue(VendorField, value); } }

		public MediaTypeHeaderValue ToHeaderValue()
		{
			return new MediaTypeHeaderValue(Value);
		}

		public override int CompareTo(MediaType other)
		{
			return other == null ? 1 : Value.CompareTo(other.Value);
		}

		public override bool Equals(MediaType other)
		{
			return other != null && Value.Equals(other.Value);
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public override string ToString()
		{
			return Value;
		}
	}
}