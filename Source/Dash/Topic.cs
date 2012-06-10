using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Dash
{
	public class Topic : Notion
	{
		public static readonly Field<string> TitleField = Field.On<Topic>.Backing(x => x.Title);
		public static readonly Field<TopicStatus> StatusField = Field.On<Topic>.Backing(x => x.Status);
		public static readonly Field<object> ContentField = Field.On<Topic>.Backing(x => x.Content);

		public Topic(string title, TopicStatus status, object content)
		{
			Contract.Requires(title != null);
			Contract.Requires(status != null);

			Title = title;
			Status = status;
			Content = content;
		}

		public string Title { get { return GetValue(TitleField); } private set { SetValue(TitleField, value); } }
		public TopicStatus Status { get { return GetValue(StatusField); } private set { SetValue(StatusField, value); } }
		public object Content { get { return GetValue(ContentField); } private set { SetValue(ContentField, value); } }
	}
}