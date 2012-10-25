using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Grasp.Checks;

namespace Grasp.Hypermedia.Lists
{
	public sealed class SortField : EquatableNotion<SortField>
	{
		public const string AscendingSuffix = "asc";
		public const string DescendingSuffix = "desc";

		public static readonly Field<string> NameField = Field.On<SortField>.For(x => x.Name);
		public static readonly Field<SortDirection> DirectionField = Field.On<SortField>.For(x => x.Direction);

		public static bool TryParse(string value, out SortField sortField)
		{
			Contract.Requires(value != null);

			return new Parser(value).TryParse(out sortField);
		}

		public static SortField Parse(string value)
		{
			Contract.Requires(value != null);

			SortField sortField;

			if(!TryParse(value, out sortField))
			{
				throw new FormatException(Resources.InvalidSortFieldFormat.FormatCurrent(value));
			}

			return sortField;
		}

		public SortField(string field, SortDirection direction)
		{
			Contract.Requires(!String.IsNullOrEmpty(field));
			Contract.Requires(field != null);

			Name = field;
			Direction = direction;
		}

		public SortField(string field) : this(field, default(SortDirection))
		{}

		public string Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }
		public SortDirection Direction { get { return GetValue(DirectionField); } private set { SetValue(DirectionField, value); } }

		public override string ToString()
		{
			return Direction == SortDirection.Ascending ? Name : Resources.SortDescendingField.FormatInvariant(Name, DescendingSuffix);
		}

		public override bool Equals(SortField other)
		{
			return other != null && Name == other.Name && Direction == other.Direction;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Name, Direction);
		}

		private sealed class Parser
		{
			private readonly string _text;

			internal Parser(string text)
			{
				_text = text;
			}

			internal bool TryParse(out SortField field)
			{
				var fieldParts = _text.Split(' ');

				if(fieldParts.Length == 1)
				{
					field = new SortField(fieldParts[0].Trim());

					return true;
				}
				else if(fieldParts.Length == 2)
				{
					var leftPart = fieldParts[0].Trim();
					var rightPart = fieldParts[1].Trim();

					var direction = default(SortDirection);

					var parsed = Check.That(leftPart).IsNotNullOrEmpty()
						&& Check.That(rightPart).IsNotNullOrEmpty()
						&& TryParseDirection(rightPart, out direction);

					field = !parsed ? null : new SortField(leftPart, direction);

					return parsed;
				}
				else
				{
					field = null;

					return false;
				}
			}

			private static bool TryParseDirection(string value, out SortDirection direction)
			{
				if(AscendingSuffix.Equals(value, StringComparison.CurrentCultureIgnoreCase))
				{
					direction = SortDirection.Ascending;

					return true;
				}
				else if(DescendingSuffix.Equals(value, StringComparison.CurrentCultureIgnoreCase))
				{
					direction = SortDirection.Descending;

					return true;
				}
				else
				{
					direction = default(SortDirection);

					return false;
				}
			}
		}
	}
}