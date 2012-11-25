using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Autofac;
using Autofac.Core;
using Cloak.Autofac;
using Cloak.Linq;
using Grasp.Hypermedia.Server;
using Grasp.Messaging;
using Grasp.Work;
using Grasp.Work.Items;
using Slate.Forms;
using Slate.Forms.Structure;
using Slate.Services;

namespace Slate.Http.Server.Composition.Api
{
	public class MessagingModule : BuilderModule
	{
		public MessagingModule()
		{
			Register(c => new TypeDispatchChannelFactory(c.Resolve<ILifetimeScope>()).Create())
			.As<IMessageChannel>()
			.SingleInstance();

			RegisterModule<AmbientMessageChannelModule>();

			Register(c => new CreateHandler<StartFormCommand, Form>(c.Resolve<IRepository<Form>>(), command => new Form(command.WorkItemId, command.FormId, command.Name)))
			.As<IHandler<StartFormCommand>>()
			.InstancePerDependency();

			Register(c => new WorkHandler<ReportProgressCommand, WorkItem>(c.Resolve<IRepository<WorkItem>>(), command => command.WorkItemId))
			.As<IHandler<ReportProgressCommand>>()
			.InstancePerDependency();

			Register(c => new WorkHandler<AddSectionCommand, Form>(c.Resolve<IRepository<Form>>(), command => command.FormId))
			.As<IHandler<AddSectionCommand>>()
			.InstancePerDependency();

			RegisterType<FormStartedSubscriber>().As<ISubscriber<FormStartedEvent>>().InstancePerDependency();
		}

		private sealed class TypeDispatchChannelFactory
		{
			private readonly ILifetimeScope _rootScope;

			internal TypeDispatchChannelFactory(ILifetimeScope rootScope)
			{
				_rootScope = rootScope;
			}

			internal TypeDispatchChannel Create()
			{
				return new TypeDispatchChannel(CreateChannels().ToReadOnlyDictionary());
			}

			private IEnumerable<KeyValuePair<Type, IMessageChannel>> CreateChannels()
			{
				// Grasp
				yield return Handle<ReportProgressCommand>();

				// Slate
				yield return Handle<StartFormCommand>();
				yield return Handle<StartTestingCommand>();
				yield return Handle<ResumeDraftCommand>();
				yield return Handle<GoLiveCommand>();
				yield return Handle<AddSectionCommand>();

				yield return Subscribe<FormStartedEvent>();
			}

			private KeyValuePair<Type, IMessageChannel> Subscribe<TEvent>() where TEvent : Event
			{
				return new KeyValuePair<Type, IMessageChannel>(typeof(TEvent), new SubscriberChannel<TEvent>(_rootScope));
			}

			private KeyValuePair<Type, IMessageChannel> Handle<TCommand>() where TCommand : Command
			{
				return new KeyValuePair<Type, IMessageChannel>(typeof(TCommand), new HandlerChannel<TCommand>(_rootScope));
			}
		}

		private sealed class SubscriberChannel<TEvent> : IMessageChannel where TEvent : Event
		{
			private readonly ILifetimeScope _rootScope;

			internal SubscriberChannel(ILifetimeScope rootScope)
			{
				_rootScope = rootScope;
			}

			public async Task PublishAsync(Message message)
			{
				var @event = (TEvent) message;

				using(var publishScope = _rootScope.BeginLifetimeScope())
				{
					var subscribers = publishScope.Resolve<IEnumerable<ISubscriber<TEvent>>>();

					await Task.WhenAll(subscribers.Select(subscriber => subscriber.ObserveAsync(@event)));
				}
			}
		}

		private sealed class HandlerChannel<TCommand> : IMessageChannel where TCommand : Command
		{
			private readonly ILifetimeScope _rootScope;

			internal HandlerChannel(ILifetimeScope rootScope)
			{
				_rootScope = rootScope;
			}

			public async Task PublishAsync(Message message)
			{
				var command = (TCommand) message;

				using(var handleScope = _rootScope.BeginLifetimeScope())
				{
					var handler = handleScope.Resolve<IHandler<TCommand>>();

					await handler.HandleAsync(command);
				}
			}
		}

		private sealed class AmbientMessageChannelModule : Module
		{
			private static readonly string _itemKey = typeof(AmbientMessageChannelModule).FullName + ".Channel";

			protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
			{
				var isController = registration.Services.OfType<TypedService>().Any(typedService => typeof(ApiController).IsAssignableFrom(typedService.ServiceType));

				if(isController)
				{
					registration.Activated += OnApiControllerActivated;
				}

				base.AttachToComponentRegistration(componentRegistry, registration);
			}

			private void OnApiControllerActivated(object sender, ActivatedEventArgs<object> e)
			{
				// This relies on IMessageChannel being SingleInstance. If it had any more specific lifetime, we would have to more
				// carefully manage the instances we use to configure the ambient message channel.

				if(!AmbientMessageChannel.IsConfigured)
				{
					var channel = e.Context.Resolve<IMessageChannel>();

					AmbientMessageChannel.Configure(() => channel);
				}

				// No matter what, there is no need to keep calling this handler after activating the first controller

				e.Component.Activated -= OnApiControllerActivated;
			}
		}
	}
}