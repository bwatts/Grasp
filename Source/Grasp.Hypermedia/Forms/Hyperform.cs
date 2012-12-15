using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http;
using System.Text;
using Cloak.Http.Media;
using Grasp.Hypermedia.Linq;
using Grasp.Knowledge;

namespace Grasp.Hypermedia.Forms
{
	public class Hyperform : HttpResource
	{
		public static readonly Field<FullName> NameField = Field.On<Hyperform>.For(x => x.Name);
		public static readonly Field<Uri> ActionField = Field.On<Hyperform>.For(x => x.Action);
		public static readonly Field<HttpMethod> MethodField = Field.On<Hyperform>.For(x => x.Method);
		public static readonly Field<MediaType> MediaTypeField = Field.On<Hyperform>.For(x => x.MediaType);
		public static readonly Field<ManyInOrder<MediaType>> AcceptedMediaTypesField = Field.On<Hyperform>.For(x => x.AcceptedMediaTypes);
		public static readonly Field<ManyInOrder<HyperformInput>> InputsField = Field.On<Hyperform>.For(x => x.Inputs);

		public Hyperform(
			MHeader header,
			Uri action,
			FullName name = null,
			HttpMethod method = null,
			MediaType mediaType = null,
			IEnumerable<MediaType> acceptedMediaTypes = null,
			IEnumerable<HyperformInput> inputs = null)
			: base(header)
		{
			Contract.Requires(action != null);

			Action = action;
			Name = name ?? FullName.Anonymous;
			Method = method ?? HttpMethod.Post;
			MediaType = mediaType ?? MediaType.ApplicationXWwwFormUrlEncoded;
			AcceptedMediaTypes = (acceptedMediaTypes ?? Enumerable.Empty<MediaType>()).ToManyInOrder();
			Inputs = (inputs ?? Enumerable.Empty<HyperformInput>()).ToManyInOrder();
		}

		public Uri Action { get { return GetValue(ActionField); } private set { SetValue(ActionField, value); } }
		public FullName Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }
		public HttpMethod Method { get { return GetValue(MethodField); } private set { SetValue(MethodField, value); } }
		public MediaType MediaType { get { return GetValue(MediaTypeField); } private set { SetValue(MediaTypeField, value); } }
		public ManyInOrder<MediaType> AcceptedMediaTypes { get { return GetValue(AcceptedMediaTypesField); } private set { SetValue(AcceptedMediaTypesField, value); } }
		public ManyInOrder<HyperformInput> Inputs { get { return GetValue(InputsField); } private set { SetValue(InputsField, value); } }
	}
}