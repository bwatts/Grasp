using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using Grasp.Knowledge;

namespace Grasp.Hypermedia
{
	public sealed class VendorPath : ComparableNotion<VendorPath>
	{
		public const string Prefix = "vnd";
		public const string Separator = ".";

		public static readonly Field<string> ValueField = Field.On<VendorPath>.Backing(x => x.Value);
		public static readonly Field<string> VendorField = Field.On<VendorPath>.Backing(x => x.Vendor);
		public static readonly Field<string> MediaField = Field.On<VendorPath>.Backing(x => x.Media);

		public static readonly VendorPath Unspecified = new VendorPath();

		public VendorPath(string mediaType)
		{
			Contract.Requires(mediaType != null);

			var pattern = new Pattern(mediaType);

			Vendor = pattern.GetVendor();
			Media = pattern.GetMedia();

			var value = new StringBuilder();

			if(!String.IsNullOrEmpty(Vendor))
			{
				value.Append(Prefix).Append(Separator).Append(Vendor);

				if(!String.IsNullOrEmpty(Media))
				{
					value.Append(Separator).Append(Media);
				}
			}

			Value = value.ToString();
		}

		private VendorPath()
		{
			Value = "";
			Vendor = "";
			Media = "";
		}

		public string Value { get { return GetValue(ValueField); } private set { SetValue(ValueField, value); } }
		public string Vendor { get { return GetValue(VendorField); } private set { SetValue(VendorField, value); } }
		public string Media { get { return GetValue(MediaField); } private set { SetValue(MediaField, value); } }

		public override int CompareTo(VendorPath other)
		{
			return other == null ? 1 : Value.CompareTo(other.Value);
		}

		public override bool Equals(VendorPath other)
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

		private sealed class Pattern
		{
			private static readonly Regex _regex = new Regex(
				@"^\s*"														// Match the input start and any leading spaces
				+ @"application/vnd\."						// Match the prefix signaling a vendor-specific media type
				+ @"(?<vendor>[\w-]*)"						// Capture the vendor name
				+ @"(\.(?<mediaPart>[\w-]+))*"		// Capture any number of parts of the referenced media
				+ @"\s*$",												// Match trailing spaces and the input end
				RegexOptions.Compiled);

			private readonly Match _match;

			internal Pattern(string mediaType)
			{
				// If any part of the value is formatted incorrectly, the pattern won't match and we use defaults. Any path with defaults for vendor and resource
				// indicates that it could not parse the media type.

				_match = _regex.Match(mediaType);
			}

			internal string GetVendor()
			{
				return MatchFailed ? "" : GetCapturedVendor();
			}

			internal string GetMedia()
			{
				return MatchFailed ? "" : GetCapturedMedia();
			}

			private bool MatchFailed
			{
				get { return !_match.Success; }
			}

			private string GetCapturedVendor()
			{
				return _match.Groups["vendor"].Captures[0].Value;
			}

			private string GetCapturedMedia()
			{
				var partCaptureValues = _match.Groups["mediaPart"].Captures.Cast<Capture>().Select(capture => capture.Value);

				return String.Join(".", partCaptureValues);
			}
		}
	}
}