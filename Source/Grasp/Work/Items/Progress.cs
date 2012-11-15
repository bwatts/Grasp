using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Work.Items
{
	/// <summary>
	/// A floating-point number in the range 0-1 representing progress on a work item
	/// </summary>
	[Serializable]
	[TypeConverter(typeof(ProgressConverter))]
	public struct Progress : IEquatable<Progress>, IComparable<Progress>
	{
		#region Operators

		public static bool operator ==(Progress x, Progress y)
		{
			return x.Value == y.Value;
		}

		public static bool operator !=(Progress x, Progress y)
		{
			return x.Value != y.Value;
		}

		public static bool operator >(Progress x, Progress y)
		{
			return x.Value > y.Value;
		}

		public static bool operator <(Progress x, Progress y)
		{
			return x.Value < y.Value;
		}

		public static bool operator >=(Progress x, Progress y)
		{
			return x.Value >= y.Value;
		}

		public static bool operator <=(Progress x, Progress y)
		{
			return x.Value <= y.Value;
		}

		public static Progress operator +(Progress x, Progress y)
		{
			return new Progress(x.Value + y.Value);
		}

		public static Progress operator -(Progress x, Progress y)
		{
			return new Progress(x.Value - y.Value);
		}

		public static Progress operator +(Progress x, int y)
		{
			return new Progress(x.Value + y);
		}

		public static Progress operator -(Progress x, int y)
		{
			return new Progress(x.Value - y);
		}
		#endregion

		public static readonly Progress Accepted = new Progress(0);
		public static readonly Progress Complete = new Progress(1);

		public Progress(double value) : this()
		{
			Contract.Requires(value >= 0 && value <= 1);

			Value = value;
		}

		public double Value { get; private set; }

		public bool HasStarted
		{
			get { return Value > 0; }
		}

		public override bool Equals(object obj)
		{
			return obj is Progress && Equals((Progress) obj);
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public override string ToString()
		{
			return ToString(CultureInfo.InvariantCulture);
		}

		public bool Equals(Progress other)
		{
			return Value == other.Value;
		}

		public int CompareTo(Progress other)
		{
			return Value.CompareTo(other.Value);
		}

		public string ToString(IFormatProvider formatProvider)
		{
			return Value.ToString("P", formatProvider);
		}
	}
}