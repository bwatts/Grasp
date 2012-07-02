using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using Grasp.Knowledge;

namespace Grasp.Hypermedia.Http.Media
{
	public sealed class VendorPath : VendorPart<VendorPath>
	{
		public const string Prefix = "vnd";
		public const string Separator = ".";
		public const string FormatSpecifier = "+";

		public static readonly Field<string> NameField = Field.On<VendorPath>.Backing(x => x.Name);
		public static readonly Field<VendorResource> ResourceField = Field.On<VendorPath>.Backing(x => x.Resource);
		public static readonly Field<VendorFormat> FormatField = Field.On<VendorPath>.Backing(x => x.Format);

		public static readonly VendorPath Unspecified = new VendorPath();

		public static VendorPath Parse(string mediaType)
		{
			Contract.Requires(mediaType != null);

			var pattern = new Pattern(mediaType);

			var name = pattern.GetName();
			var path = pattern.GetPath();
			var format = pattern.GetFormat();

			var value = new StringBuilder();

			if(name != "")
			{
				value.Append(Prefix).Append(Separator).Append(name);

				if(path != VendorResource.Unspecified)
				{
					value.Append(Separator).Append(path);
				}

				if(format != VendorFormat.Unspecified)
				{
					value.Append(FormatSpecifier).Append(format);
				}
			}

			return new VendorPath(value.ToString(), name, path, format);
		}

		private VendorPath() : base("")
		{
			Name = "";
			Resource = VendorResource.Unspecified;
			Format = VendorFormat.Unspecified;
		}

		private VendorPath(string value, string name, VendorResource path, VendorFormat format) : base(value)
		{
			Name = name;
			Resource = path;
			Format = format;
		}

		public string Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }
		public VendorResource Resource { get { return GetValue(ResourceField); } private set { SetValue(ResourceField, value); } }
		public VendorFormat Format { get { return GetValue(FormatField); } private set { SetValue(FormatField, value); } }

		private sealed class Pattern
		{
			private static readonly Regex _regex = new Regex(
				@"^\s*"														// Match the input start and any leading spaces
				+ @"application/vnd\."						// Match the prefix signaling a vendor-specific media type
				+ @"(?<name>[\w-]*)"							// Capture the vendor name
				+ @"(\.(?<path>[\w-]+))*"					// Capture any number of path elements
				+ @"\+(?<format>[\w-]*)"					// Match the "+" format specifier and capture the value
				+ @"\s*$",												// Match trailing spaces and the input end
				RegexOptions.Compiled);

			private readonly Match _match;

			internal Pattern(string mediaType)
			{
				// If any part of the value is formatted incorrectly, the pattern won't match and we use defaults. Any media type with defaults for vendor, namespace, and format
				// indicates that it could not parse the origin string.

				_match = _regex.Match(mediaType);
			}

			internal string GetName()
			{
				return MatchFailed ? "" : GetCapturedName();
			}

			internal VendorResource GetPath()
			{
				return MatchFailed ? VendorResource.Unspecified : GetCapturedPath();
			}

			internal VendorFormat GetFormat()
			{
				return MatchFailed ? VendorFormat.Unspecified : GetCapturedFormat();
			}

			private bool MatchFailed
			{
				get { return !_match.Success; }
			}

			private string GetCapturedName()
			{
				return _match.Groups["name"].Captures[0].Value;
			}

			private VendorResource GetCapturedPath()
			{
				var captureValues = _match.Groups["path"].Captures.Cast<Capture>().Select(capture => capture.Value);

				return new VendorResource(String.Join(".", captureValues));
			}

			private VendorFormat GetCapturedFormat()
			{
				var format = _match.Groups["format"].Captures[0].Value;

				return VendorFormat.TryGetKnownFormat(format) ?? new VendorFormat(format);
			}
		}
	}
}