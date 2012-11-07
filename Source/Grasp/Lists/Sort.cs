using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Grasp.Checks;

namespace Grasp.Lists
{
	public sealed class Sort : EquatableNotion<Sort>
	{
		public static readonly Field<ManyInOrder<SortField>> FieldsField = Field.On<Sort>.For(x => x.Fields);

		public static readonly Sort Empty = new Sort(Enumerable.Empty<SortField>());

		public static bool TryParse(string value, out Sort sort)
		{
			Contract.Requires(value != null);

			return new Parser(value).TryParse(out sort);
		}

		public static Sort Parse(string value)
		{
			Contract.Requires(value != null);

			Sort sort;

			if(!TryParse(value, out sort))
			{
				throw new FormatException(Resources.InvalidSortFormat.FormatCurrent(value));
			}

			return sort;
		}

		public Sort(IEnumerable<SortField> fields)
		{
			Contract.Requires(fields != null);

			Fields = new ManyInOrder<SortField>(fields);
		}

		public ManyInOrder<SortField> Fields { get { return GetValue(FieldsField); } private set { SetValue(FieldsField, value); } }

		public override string ToString()
		{
			return String.Join(", ", Fields);
		}

		public override bool Equals(Sort other)
		{
			return other != null && Fields.SequenceEqual(other.Fields);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine<object>(Fields);
		}

		private sealed class Parser
		{
			private readonly string _text;

			internal Parser(string text)
			{
				_text = text;
			}

			internal bool TryParse(out Sort sort)
			{
				if(_text == "")
				{
					sort = Sort.Empty;

					return true;
				}
				else
				{
					return TryParseCore(out sort);
				}
			}

			private bool TryParseCore(out Sort sort)
			{
				sort = null;

 				var textParts = _text.Split(',');

				return textParts.Any() && TryParseFields(textParts, out sort);
			}

			private static bool TryParseFields(IEnumerable<string> textParts, out Sort sort)
			{
				var parsedFields = new List<SortField>();

				sort = null;

 				foreach(var textPart in textParts)
				{
					SortField field;

					if(!SortField.TryParse(textPart, out field))
					{
						return false;
					}

					parsedFields.Add(field);
				}

				sort = new Sort(parsedFields);

				return true;
			}
		}
	}
}