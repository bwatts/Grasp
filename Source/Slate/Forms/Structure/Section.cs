using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;

namespace Slate.Forms.Structure
{
	public class Section : FormElement
	{
		public static readonly Field<string> TitleField = Field.On<Section>.For(x => x.Title);
		public static readonly Field<ManyInOrder<FormElement>> ElementsField = Field.On<Section>.For(x => x.Elements);

		public Section(string title, IEnumerable<FormElement> elements = null)
		{
			Elements = (elements ?? Enumerable.Empty<FormElement>()).ToManyInOrder();
		}

		public string Title { get { return GetValue(TitleField); } private set { SetValue(TitleField, value); } }
		public ManyInOrder<FormElement> Elements { get { return GetValue(ElementsField); } private set { SetValue(ElementsField, value); } }

		public Section FindSection(EntityId id)
		{
			Contract.Requires(id != EntityId.Unassigned);

			return id == Id ? this : GetSections().FirstOrDefault(section => section.Id == id);
		}

		public bool ContainsSection(EntityId id)
		{
			Contract.Requires(id != EntityId.Unassigned);

			return id == Id || GetSections().Any(section => section.ContainsSection(id));
		}

		public void AddSection(Section section)
		{
			Contract.Requires(section != null);

			Elements.AsWriteable().Add(section);
		}

		public IEnumerable<Section> GetSections()
		{
			return Elements.OfType<Section>();
		}

		public IEnumerable<Input> GetInputs()
		{
			return Elements.OfType<Input>();
		}
	}
}