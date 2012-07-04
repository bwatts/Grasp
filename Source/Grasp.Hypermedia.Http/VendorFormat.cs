using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Grasp.Hypermedia.Http
{
	public sealed class VendorFormat : VendorPart<VendorFormat>
	{
		public static readonly Field<string> DescriptionField = Field.On<VendorFormat>.Backing(x => x.Description);
		public static readonly Field<Uri> UrlField = Field.On<VendorFormat>.Backing(x => x.Url);

		public static readonly VendorFormat Unspecified = new VendorFormat(Resources.FormatUnspecified, Resources.FormatUnspecifiedDescription);
		public static readonly VendorFormat Json = new VendorFormat(Resources.FormatJson, Resources.FormatJsonDescription, new Uri(Resources.FormatJsonUrl));
		public static readonly VendorFormat Xml = new VendorFormat(Resources.FormatXml, Resources.FormatXmlDescription, new Uri(Resources.FormatXmlUrl));
		public static readonly VendorFormat Xhtml = new VendorFormat(Resources.FormatXhtml, Resources.FormatXhtmlDescription, new Uri(Resources.FormatXhtmlUrl));

		public static VendorFormat TryGetKnownFormat(string knownValue)
		{
			Contract.Requires(knownValue != null);

			return new[] { Unspecified, Json, Xml, Xhtml }.FirstOrDefault(format => StringComparer.OrdinalIgnoreCase.Equals(format.Value, knownValue));
		}

		public VendorFormat(string value) : base(value)
		{}

		public VendorFormat(string value, string description) : base(value)
		{
			Contract.Requires(description != null);

			Description = description;
		}

		public VendorFormat(string value, Uri url) : base(value)
		{
			Contract.Requires(url != null);

			Url = url;
		}

		public VendorFormat(string value, string description, Uri url) : base(value)
		{
			Contract.Requires(description != null);
			Contract.Requires(url != null);

			Description = description;
			Url = url;
		}

		public string Description { get { return GetValue(DescriptionField); } private set { SetValue(DescriptionField, value); } }
		public Uri Url { get { return GetValue(UrlField); } private set { SetValue(UrlField, value); } }

		public bool HasDescription
		{
			get { return Description != null; }
		}

		public bool HasUrl
		{
			get { return Url != null; }
		}
	}
}