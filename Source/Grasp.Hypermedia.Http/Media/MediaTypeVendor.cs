using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;

namespace Grasp.Hypermedia.Http.Media
{
	public sealed class MediaTypeVendor : MediaTypePart<MediaTypeVendor>
	{
		public static readonly MediaTypeVendor Unspecified = new MediaTypeVendor();

		public static MediaTypeVendor FromMediaType(string mediaType)
		{
			Contract.Requires(mediaType != null);

			var pattern = new Pattern(mediaType);

			var name = pattern.GetName();
			var path = pattern.GetPath();
			var format = pattern.GetFormat();

			var value = new StringBuilder();

			if(name != "")
			{
				value.Append("vnd.").Append(name);

				if(path != MediaTypePath.Unspecified)
				{
					value.Append(".").Append(path);
				}

				if(format != MediaTypeFormat.Unspecified)
				{
					value.Append("+").Append(format);
				}
			}

			return new MediaTypeVendor(value.ToString(), name, path, format);
		}

		private MediaTypeVendor() : base("")
		{
			Name = "";
			Path = MediaTypePath.Unspecified;
			Format = MediaTypeFormat.Unspecified;
		}

		private MediaTypeVendor(string value, string name, MediaTypePath path, MediaTypeFormat format) : base(value)
		{
			Name = name;
			Path = path;
			Format = format;
		}

		public string Name { get; private set; }

		public MediaTypePath Path { get; private set; }

		public MediaTypeFormat Format { get; private set; }

		private sealed class Pattern
		{
			private static readonly Regex _regex = new Regex(
				@"^\s*"														// Match the input start and any leading spaces
				+ @"application/vnd\."						// Match the prefix signaling a vendor-specific media type
				+ @"(?<name>[\w-]*)"							// Capture the vendor name
				+ @"(\.(?<path>[\w-]+))+"					// Capture at least one path element
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

			internal MediaTypePath GetPath()
			{
				return MatchFailed ? MediaTypePath.Unspecified : GetCapturedPath();
			}

			internal MediaTypeFormat GetFormat()
			{
				return MatchFailed ? MediaTypeFormat.Unspecified : GetCapturedFormat();
			}

			private bool MatchFailed
			{
				get { return !_match.Success; }
			}

			private string GetCapturedName()
			{
				return _match.Groups["name"].Captures[0].Value;
			}

			private MediaTypePath GetCapturedPath()
			{
				var captureValues = _match.Groups["path"].Captures.Cast<Capture>().Select(capture => capture.Value);

				return new MediaTypePath(String.Join(".", captureValues));
			}

			private MediaTypeFormat GetCapturedFormat()
			{
				return new MediaTypeFormat(_match.Groups["format"].Captures[0].Value);
			}
		}
	}
}