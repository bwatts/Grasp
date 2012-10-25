using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Grasp.Checks;

namespace Grasp.Hypermedia
{
	public sealed class HrefPart : ComparableValue<HrefPart, string>
	{
		public static readonly HrefPart Separator = new HrefPart("/");

		public static string ParameterText(string name)
		{
			Contract.Requires(Check.That(name).IsNotNullOrEmpty());

			return "{" + name + "}";
		}

		public static HrefPart Parameter(string name)
		{
			Contract.Requires(Check.That(name).IsNotNullOrEmpty());

			return new HrefPart("{" + name + "}");
		}

		public static IEnumerable<HrefPart> Split(string path)
		{
			Contract.Requires(path != null);

			return
				from part in path.Split(Params.Of(HrefPart.Separator.Value), StringSplitOptions.None)
				select new HrefPart(part);
		}

		public static IEnumerable<HrefPart> Split(Uri uri)
		{
			Contract.Requires(uri != null);

			return Split(uri.AbsolutePath);
		}

		public HrefPart(string value) : base(value)
		{}

		public bool IsSeparator
		{
			get { return this == Separator; }
		}

		public bool IsParameter
		{
			get { return Value.StartsWith("{") && Value.EndsWith("}"); }
		}

		public string ParameterName
		{
			get { return IsParameter ? Value.Substring(1, Value.Length - 2) : ""; }
		}

		public string Escape()
		{
			return Href.Escape(Value);
		}

		public HrefPart BindParameter(string parameterName, string value)
		{
			return !IsParameter || ParameterName != parameterName ? this : new HrefPart(value);
		}
	}
}