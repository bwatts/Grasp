using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Xml.Linq;
using Cloak;
using Cloak.Http.Media;
using Grasp.Knowledge;

namespace Grasp.Hypermedia.Linq.Forms
{
	public sealed class MForm : MContent
	{
		public static readonly Field<Uri> ActionField = Field.On<MForm>.For(x => x.Action);
		public static readonly Field<HttpMethod> MethodField = Field.On<MForm>.For(x => x.Method);
		public static readonly Field<MediaType> MediaTypeField = Field.On<MForm>.For(x => x.MediaType);
		public static readonly Field<ManyInOrder<MediaType>> AcceptedMediaTypesField = Field.On<MForm>.For(x => x.AcceptedMediaTypes);
		public static readonly Field<ManyInOrder<MContent>> ItemsField = Field.On<MForm>.For(x => x.Items);

		public MForm(
			MClass @class,
			Uri action,
			HttpMethod method = null,
			MediaType mediaType = null,
			IEnumerable<MediaType> acceptedMediaTypes = null,
			IEnumerable<MContent> items = null,
			FullName name = null)
			: base(@class)
		{
			Contract.Requires(action != null);

			Action = action;
			Method = method ?? HttpMethod.Post;
			MediaType = mediaType ?? MediaType.ApplicationXWwwFormUrlEncoded;
			AcceptedMediaTypes = (acceptedMediaTypes ?? Enumerable.Empty<MediaType>()).ToManyInOrder();
			Items = (items ?? Enumerable.Empty<MContent>()).ToManyInOrder();
			Name = name ?? FullName.Anonymous;
		}

		public Uri Action { get { return GetValue(ActionField); } private set { SetValue(ActionField, value); } }
		public HttpMethod Method { get { return GetValue(MethodField); } private set { SetValue(MethodField, value); } }
		public MediaType MediaType { get { return GetValue(MediaTypeField); } private set { SetValue(MediaTypeField, value); } }
		public ManyInOrder<MediaType> AcceptedMediaTypes { get { return GetValue(AcceptedMediaTypesField); } private set { SetValue(AcceptedMediaTypesField, value); } }
		public ManyInOrder<MContent> Items { get { return GetValue(ItemsField); } private set { SetValue(ItemsField, value); } }
		public FullName Name { get { return GetValue(FullName.NameField); } private set { SetValue(FullName.NameField, value); } }

		protected override object GetHtmlWithoutClass()
		{
			return GetFormElement();
		}

		protected override object GetHtmlWithClass(string classStack)
		{
			var formElement = GetFormElement();

			formElement.Add(new XAttribute("class", classStack));

			return formElement;
		}

		private XElement GetFormElement()
		{
			return new XElement("form", GetAttributes(), GetItemElements());
		}

		private IEnumerable<XAttribute> GetAttributes()
		{
			if(Name != FullName.Anonymous)
			{
				yield return new XAttribute("name", Name);
			}

			yield return new XAttribute("action", Action);
			yield return new XAttribute("method", Method);
			yield return new XAttribute("enctype", MediaType);
			
			if(AcceptedMediaTypes.Any())
			{
				yield return new XAttribute("accept", AcceptedMediaTypes.SeparateWithCommas());
			}
		}

		private IEnumerable<XElement> GetItemElements()
		{
			// TODO

			yield break;
		}
	}
}