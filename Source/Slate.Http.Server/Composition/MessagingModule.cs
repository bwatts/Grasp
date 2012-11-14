using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak.Autofac;
using Grasp;
using Grasp.Messaging;

namespace Slate.Http.Server.Composition
{
	public class MessagingModule : BuilderModule
	{
		public MessagingModule()
		{
			RegisterType<FakeMessageChannel>().As<IMessageChannel>().InstancePerDependency();
		}


		private sealed class FakeMessageChannel : IMessageChannel
		{
			public Task PublishAsync(Message message)
			{
				return Task.Run(() => Debug.WriteLine(GetText(message)));
			}

			private static string GetText(Message message)
			{
				var text = new StringBuilder("A message was sent on this channel");

				text.AppendLine().AppendLine().Append("Data for message type ").Append(message.GetType().FullName).AppendLine(":");

				foreach(var binding in ((IFieldContext) message).GetBindings())
				{
					text.AppendLine().Append(binding.Field.IsAttached ? binding.Field.FullName : binding.Field.Name).Append(" = ").Append(binding.Value);
				}

				return text.ToString();
			}
		}




	}
}