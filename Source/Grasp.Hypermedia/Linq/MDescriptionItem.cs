using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Grasp.Hypermedia.Linq
{
	public sealed class MDescriptionItem : MContent
	{
		public static readonly Field<Many<MValue>> TermsField = Field.On<MDescriptionItem>.For(x => x.Terms);
		public static readonly Field<Many<MContent>> DescriptionsField = Field.On<MDescriptionItem>.For(x => x.Descriptions);

		public MDescriptionItem(IEnumerable<MValue> terms, IEnumerable<MContent> descriptions)
		{
			Contract.Requires(terms != null);
			Contract.Requires(descriptions != null);

			Terms = terms.ToMany();
			Descriptions = descriptions.ToMany();

			if(!Terms.Any() || !Descriptions.Any())
			{
				throw new HypermediaException(Resources.DescriptionListsRequireTermAndDescription);
			}
		}

		public MDescriptionItem(MValue term, MContent description)
		{
			Contract.Requires(term != null);
			Contract.Requires(description != null);

			Terms = new Many<MValue>(term);
			Descriptions = new Many<MContent>(description);
		}

		public Many<MValue> Terms { get { return GetValue(TermsField); } private set { SetValue(TermsField, value); } }
		public Many<MContent> Descriptions { get { return GetValue(DescriptionsField); } private set { SetValue(DescriptionsField, value); } }

		public MValue Term
		{
			get { return Terms.First(); }
		}

		public MContent Description
		{
			get { return Descriptions.First(); }
		}

		protected override object GetHtmlWithoutClass()
		{
			return GetTermElements().Concat(GetDescriptionElements());
		}

		protected override object GetHtmlWithClass(string classStack)
		{
			// Description items do not directly correspond to an HTML element - term and description classes take effect instead

			return GetHtmlWithoutClass();
		}

		private IEnumerable<XElement> GetTermElements()
		{
			foreach(var term in Terms)
			{
				var html = term.GetHtml();

				var element = html as XElement;

				if(element != null)
				{
					element.Name = "dt";
				}
				else
				{
					element = new XElement("dt", html);
				}

				yield return element;
			}
		}

		private IEnumerable<XElement> GetDescriptionElements()
		{
			return Descriptions.Select(description => new XElement("dd", description.GetHtml()));
		}
	}
}